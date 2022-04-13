
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyModel;

namespace RefactorThis.Common
{
    public static class AutofacConfig
    {
        /// <summary>
        /// Sets up the default Dependancy Resolver
        /// </summary>
        /// <param name="builder"></param>
        public static void SetupDependencyResolver(this ContainerBuilder builder)
        {
            // get the list of assemblies which may contain something to resolve
            var assemblies = GetAssemblies();
            builder.RegisterAssemblyModules(assemblies);
        }

        /// <summary>
        /// Gets List of Assemblies being used
        /// </summary>
        /// <returns> List of Assemblies being used in the API</returns>
        private static Assembly[] GetAssemblies()
        {
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            return dependencies
                .Where(IsCandidate)
                .Select(library => Assembly.Load(new AssemblyName(library.Name)))
                .ToArray();
        }
        /// <summary>
        /// Determines if this project contains all the dependent assemblies
        /// </summary>
        /// <param name="compilationLibrary">RuntimeLibrary to check against our current Service</param>
        /// <returns>Boolean of whether the assembly is used in this service</returns>
        private static bool IsCandidate(this Library compilationLibrary)
        {
            return compilationLibrary.Name.Contains("Domain.Product") ||
                   compilationLibrary.Name.Contains("RefactorThis") ||
                   compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("RefactorThis"));
        }
    }
}
