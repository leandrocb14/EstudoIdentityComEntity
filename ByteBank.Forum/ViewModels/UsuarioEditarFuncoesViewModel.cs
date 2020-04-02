using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ByteBank.Forum.ViewModels
{
    public class UsuarioEditarFuncoesViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<UsuarioFuncaoViewModel> Funcoes { get; set; }

        public UsuarioEditarFuncoesViewModel()
        {

        }

        public UsuarioEditarFuncoesViewModel(UsuarioAplicacao usuarioAplicacao, RoleManager<IdentityRole> roleManager)
        {
            Id = usuarioAplicacao.Id;
            Nome = usuarioAplicacao.NomeCompleto;
            Email = usuarioAplicacao.Email;
            UserName = usuarioAplicacao.UserName;

            Funcoes = roleManager.Roles.ToList().Select(role => new UsuarioFuncaoViewModel()
            {
                Nome = role.Name,
                Id = role.Id
            }).ToList();

            foreach (var item in Funcoes)
            {
                var funcaoUsuario = usuarioAplicacao.Roles.FirstOrDefault(u => u.RoleId == item.Id);
                item.Selecionado = funcaoUsuario != null;
            }
        }
    }
}