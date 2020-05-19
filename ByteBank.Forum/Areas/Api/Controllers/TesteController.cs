using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ByteBank.Forum.Areas.Api.Controllers
{
    [Authorize]
    public class TesteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Obter()
        {
            return Ok(new { Teste = "Tudo BLZ" });
        }
    }
}
