using ByteBank.Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ByteBank.Forum.ViewModels
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }

        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(UsuarioAplicacao usuarioAplicacao)
        {
            Id = usuarioAplicacao.Id;
            Email = usuarioAplicacao.Email;
            Nome = usuarioAplicacao.NomeCompleto;
            UserName = usuarioAplicacao.UserName;
        }
    }
}