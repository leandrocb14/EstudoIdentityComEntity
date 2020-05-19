using ByteBank.Forum.Infraestrutura;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ByteBank.Forum.App_Start.Identity
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var teste = await context.Request.ReadFormAsync();

            var user = await IdentityUtil.UserManager.FindByEmailAsync(context.UserName);
            var rolesUser = IdentityUtil.UserManager.GetRoles(user.Id);
            var userNotConfirm = await IdentityUtil.UserManager.FindAsync(user.UserName, context.Password) == null;
            

            if (user == null || userNotConfirm)
            {
                context.SetError("invalid_grant", "Usuário ou senha está incorreto.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, rolesUser.FirstOrDefault()));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            var props = new AuthenticationProperties();

            var ticket = new AuthenticationTicket(identity, props);

            context.Validated(ticket);
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
        }

        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                if (property.Key != ".issued" && property.Key != ".expires" && !string.IsNullOrEmpty(property.Value))
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }
            }
        }
    }
}