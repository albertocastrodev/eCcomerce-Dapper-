using eCcomerce_Dapper_.Models;
using eCcomerce_Dapper_.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCcomerce_Dapper_.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuario_Repository _usuario_repository;
        //public UsuariosController(IUsuario_Repository usuario_repository)
        //{
        //    _usuario_repository = usuario_repository;
        //}

        public UsuariosController()
        {
            _usuario_repository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()

        {
            var usuario = _usuario_repository.GetUsuarios();
            return Ok(usuario);
        }


   
        [HttpGet("{id}")]

        public IActionResult GetId(int id)
        {

            var usuario = _usuario_repository.GetUsuario(id);
            return Ok(usuario);

        }

        [HttpPost]

        public IActionResult Insert(Usuario usuario)
        {
            _usuario_repository.Insert(usuario);
            return Ok(usuario);

        }

        [HttpPut]

        public IActionResult Updade(Usuario usuario)
        {

            _usuario_repository.Update(usuario);
            return Ok(usuario);

        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            _usuario_repository.Delete(id);
            return Ok("Usuário deletado");
        }


    }


}
