using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ByteBank.Forum.Controllers
{
    public class UsuarioController : Controller
    {
        private UserManager<UsuarioAplicacao> _userManager;
        public UserManager<UsuarioAplicacao> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var contextOwin = HttpContext.GetOwinContext();
                    _userManager = contextOwin.GetUserManager<UserManager<UsuarioAplicacao>>();
                }
                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }

        private RoleManager<IdentityRole> _roleManager;

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    var contextOwin = HttpContext.GetOwinContext();
                    _roleManager = contextOwin.GetUserManager<RoleManager<IdentityRole>>();
                }
                return _roleManager;
            }
            set { _roleManager = value; }
        }

        // GET: Usuario
        public ActionResult Index()
        {
            var model = UserManager.Users.ToList().Select(user => new UsuarioViewModel(user));
            return View(model);
        }

        public async Task<ActionResult> EditarFuncoes(string id)
        {
            var usuario = await UserManager.FindByIdAsync(id);
            var model = new UsuarioEditarFuncoesViewModel(usuario, RoleManager);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditarFuncoes(UsuarioEditarFuncoesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await UserManager.FindByIdAsync(model.Id);
                var rolesUser = UserManager.GetRoles(usuario.Id);

                var resultado = await UserManager.RemoveFromRolesAsync(usuario.Id, rolesUser.ToArray());
                if (resultado.Succeeded)
                {
                    var funcoesSelecionadas = model.Funcoes.Where(f => f.Selecionado).Select(f => f.Nome).ToArray();

                    var resultadoAdicao = await UserManager.AddToRolesAsync(usuario.Id, funcoesSelecionadas);

                    if (resultadoAdicao.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }

            return View();
        }
    }
}