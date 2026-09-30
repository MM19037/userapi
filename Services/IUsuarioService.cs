using UserApi.Models;

namespace UserApi.Services
{
    public interface IUsuarioService
    {
        // Definición de funciones para trabajar con los usuarios
        IEnumerable<Usuario> ObtenerTodos();
        Usuario? ObtenerPorId(int id);
        Usuario Crear(Usuario usuario);
    }
}