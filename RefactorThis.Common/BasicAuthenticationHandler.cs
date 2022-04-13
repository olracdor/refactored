using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RefactorThis.Common.Configuration;

namespace RefactorThis.Common
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BasicAuthConfiguration _configuration;

        /// <summary>
        /// Instantiate a new instance of the handler
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="configuration"></param>
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            BasicAuthConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Check the requests
        /// </summary>
        /// <returns></returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>();
            if (_configuration.IsEnabled)
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

                try
                {
                    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                    var username = credentials[0];
                    var password = credentials[1];

                    if (!(username.Equals(_configuration.Username) &&
                           password.Equals(_configuration.Password)))
                    {
                        return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization"));
                    }
                }
                catch
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
                }

                claims.Add(new Claim(ClaimTypes.NameIdentifier, _configuration.Username));
                claims.Add(new Claim(ClaimTypes.Name, _configuration.Username));
            }

            var identity = new ClaimsIdentity(claims.ToArray(), Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
