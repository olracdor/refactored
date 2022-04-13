using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
namespace RefactorThis.Common.Configuration
{
    public static class AuthorisationConfig
    {
        /// <summary>
        /// Setup the Authorisation
        /// </summary>
        /// <param name="services"></param>
        public static void SetupAuthorisation(this IServiceCollection services)
        {
            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }
    }
}
