using System;
using System.Net.Http;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using RefactorThis.Common.Configuration;

namespace RefactorThis.Common
{
    public static  class ContainerBuilderExtensions
    {
        public static string ConfigurationKey => "AppConfig";

        /// <summary>
        /// Builds an HttpClient using the provided setup Action and registers it against a class
        /// </summary>
        /// <typeparam name="T">The type to register against</typeparam>
        /// <param name="builder">The builder being used to registration</param>
        /// <param name="configureClient">Setup for the HttpClient</param>
        /// <returns>The registration for the HttpClient</returns>
        public static IRegistrationBuilder<HttpClient, SimpleActivatorData, SingleRegistrationStyle>
            RegisterHttpClient<T>(this ContainerBuilder builder, Action<HttpClient, IComponentContext> configureClient)
        {
            var client = new HttpClient();

            return builder.Register(ctx =>
            {
                configureClient(client, ctx);
                return client;
            }).Named<HttpClient>(typeof(T).Name).SingleInstance();
        }

        /// <summary>
        /// Convenience extension for singletons
        /// </summary>
        /// <typeparam name="T">The type to make a singleton</typeparam>
        /// <param name="builder">The builder to build the singleton with</param>
        /// <returns>The registration for the type</returns>
        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterSingleton<T>(this ContainerBuilder builder)
        {
            return builder.RegisterType<T>().SingleInstance();
        }

        /// <summary>
        /// Convenience extension for singletons
        /// </summary>
        /// <typeparam name="T">The type to make a singleton</typeparam>
        /// <param name="builder">The builder to build the singleton with</param>
        /// <param name="instance">The instance to define as a singleton</param>
        /// <returns>The registration for the type</returns>
        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle>
            RegisterSingleton<T>(this ContainerBuilder builder, T instance) where T : class
        {
            return builder.RegisterInstance(instance).SingleInstance();
        }

        /// <summary>
        /// Registers a subclass of BaseConfiguration with the global IConfiguration passed to its constructor
        ///
        /// This extension will break the program if IConfiguration was never registered
        ///   or the generic class does not have a constructor matching TConfiguration(IConfiguration)
        ///
        /// Both of these should never assert false but if they do it's an early way to catch
        ///   weird dependency resolution issues
        /// </summary>
        /// <typeparam name="TConfiguration">The class to map to</typeparam>
        /// <param name="builder">The builder to build the config with</param>
        /// <returns>A registered singleton of the specified configuration class</returns>
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static IRegistrationBuilder<TConfiguration, SimpleActivatorData, SingleRegistrationStyle>
            RegisterConfiguration<TConfiguration>(this ContainerBuilder builder) where TConfiguration : AppConfig
        {
            var registeredConfiguration = builder.Register(ctx =>
            {
                // Attempt to resolve an IConfiguration with a name
                // The reason why we use a named lookup is because TryResolve(out IConfiguration) would return the right configuration
                //   but there's no guarantee that will still be implicitly registered in a later version of Autofac.
                //   My assumption is that the registration is somehow done by UseServiceProviderFactory(new AutofacServiceProviderFactory())
                //   call in Program.cs
                // The IConfiguration is registered in Startup so this should always resolve
                ctx.TryResolveNamed(ConfigurationKey, typeof(IConfiguration), out var resolvedConfiguration);

                // Will break the startup of the program if someone has manually removed the configuration registration
                // Purely a safeguard as lots of services will fail if missing configuration
                // NOTE: If this fails, it's probably your fault
                if (resolvedConfiguration == null) throw new InvalidOperationException("An IConfiguration instance could not be resolved by Autofac. " +
                                                                             "Configurations can not be registered.");

                // Gets a constructor from TConfiguration that takes IConfiguration as a parameter, otherwise null
                var validConstructor = typeof(TConfiguration).GetConstructor(new[] { typeof(IConfiguration) });

                // Again, will break startup if the constructor isn't TConfiguration(IConfiguration)
                // NOTE: this could also be your fault if you didn't pass an IConfiguration in your configuration type
                if (validConstructor == null) throw new InvalidOperationException($"No suitable constructor was found for type \"{typeof(TConfiguration)}\". " +
                                                                        "Constructor must take an IConfiguration.");

                // Call the constructor of TConfiguration with the IConfiguration object as a parameter
                // Will not fail as BaseConfiguration's default constructor takes IConfiguration
                return (TConfiguration)validConstructor.Invoke(new[] { resolvedConfiguration });
            });

            // Creates a single instance of the configuration object
            // AutoActivate() runs the builder.Register Delegate above when dependencies are built, not when they are injected
            return registeredConfiguration.SingleInstance().AsSelf().AutoActivate();
        }

        /// <summary>
        /// Registers a pre-registered HttpClient instance based on type
        /// </summary>
        /// <typeparam name="T">The (inferred) type to apply client on</typeparam>
        /// <param name="regBuilder">The registration builder</param>
        /// <returns>The registration for the type</returns>
        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            WithHttpClient<T>(this IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> regBuilder)
        {
            // TODO: exception on failed resolve
            return regBuilder.WithParameter(new ResolvedParameter(
                    (info, context) => info.ParameterType == typeof(HttpClient),
                    (info, context) => context.ResolveNamed<HttpClient>(typeof(T).Name)
                ));
        }
    }
}
