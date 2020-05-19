using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ByteBank.Forum.Infraestrutura
{
    public class IdentityUtil
    {
        public static UserManager<UsuarioAplicacao> UserManager
        {
            get => HttpContext.Current.GetOwinContext().GetUserManager<UserManager<UsuarioAplicacao>>();
        }

        public static SignInManager<UsuarioAplicacao, string> SignInManager
        {
            get => HttpContext.Current.GetOwinContext().GetUserManager<SignInManager<UsuarioAplicacao, string>>();
        }

        public static IAuthenticationManager AuthenticationManager
        {
            get => HttpContext.Current.Request.GetOwinContext().Authentication;
        }

    }
}