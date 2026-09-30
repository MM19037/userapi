using System.Collections.Concurrent;
using UserApi.Models;


// Simula un servicio de almacenamiento de datos, en este caso utiliza un diccionario concurrente
// para almacenar la inforamción en memoria y atender todas las peticiones con diferentes hilos de procesos
// es necesario ya que el programa presenta la misma instancia para este servicio (singleton)
namespace UserApi.Services
{
    public class UsuarioSingletonService : IUsuarioService
    {
        // Diccionario concurrente para almacenar usuarios en memoria de forma segura (evitar condiciones de carrera)
        private readonly ConcurrentDictionary<int, Usuario> _usuarios = new();
        private int _ultimoId = 0;

        // Constructor para inicializar datos por defecto
        public UsuarioSingletonService()
        {
            // Carga datos iniciales de prueba
            Crear(new Usuario { Nombre = "Juan Pérez", Email = "juan@empresa.com" });
            Crear(new Usuario { Nombre = "María López", Email = "maria@empresa.com" });
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _usuarios.Values;
        }

        public Usuario? ObtenerPorId(int id)
        {
            _usuarios.TryGetValue(id, out var usuario);
            return usuario;
        }

        public Usuario Crear(Usuario usuario)
        {
            // Incrementa el ID de forma atómica y segura
            int nuevoId = Interlocked.Increment(ref _ultimoId);
            usuario.Id = nuevoId;

            _usuarios[nuevoId] = usuario;
            return usuario;
        }
    }
}
