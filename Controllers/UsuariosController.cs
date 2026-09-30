using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        // Constructor para inicializar el controlador con el servicio de usuarios
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var usuarios = _usuarioService.ObtenerTodos();
            return Ok(usuarios);
        }

        // GET: api/usuarios/1
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado." });
            }
            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public IActionResult Crear([FromBody] Usuario nuevoUsuario)
        {
            if (string.IsNullOrWhiteSpace(nuevoUsuario.Nombre))
            {
                return BadRequest(new { mensaje = "El nombre es obligatorio." });
            }

            var usuarioCreado = _usuarioService.Crear(nuevoUsuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = usuarioCreado.Id }, usuarioCreado);
        }
    }
}
