using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios {
    internal class ServicioVideojuegos {
        // Propiedades del servicio
        public IGestorVideojuegos Gestor { get; set; }
        public IExportador Exportador { get; set; }
        public List<Usuario> Usuarios { get; set; }

        public ServicioVideojuegos(IGestorVideojuegos gestor, IExportador exportador)
        {
            Gestor = gestor ?? throw new ArgumentNullException(nameof(gestor)); 
            Exportador = exportador ?? throw new ArgumentNullException(nameof(exportador)); 
            Usuarios = new List<Usuario>();
        }

        // Registra un nuevo usuario y lo añade a la lista
        public Usuario RegistrarUsuario(string nombre)
        {
            var nuevoUsuario = new Usuario(nombre);
            Usuarios.Add(nuevoUsuario);
            return nuevoUsuario;
        }

        // Busca un usuario ignorando mayúsculas y minúsculas; retorna null si no existe
        public Usuario BuscarUsuario(string nombre)
        {
             return Usuarios.FirstOrDefault(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Elimina un usuario y retorna true si se encontró y eliminó, de lo contrario false
        public bool EliminarUsuario(string nombre)
        {
            var usuario = BuscarUsuario(nombre);
             if (usuario != null) {
                return Usuarios.Remove(usuario);
             }
        return false;
        }

        // Retorna el número de usuarios registrados
        public int ContarUsuarios()
        {
            return Usuarios.Count;
        }

        // Ordena el catálogo según el algoritmo especificado ("quick" o "merge")
        public void OrdenarCatalogo(string algoritmo)
        {
            if (algoritmo.Equals("quick", StringComparison.OrdinalIgnoreCase)) {
                Gestor.QuickSort(Gestor.JuegosDisponibles);
                Console.WriteLine("Catálogo ordenado correctamente con QuickSort.");
            } 
            else if (algoritmo.Equals("merge", StringComparison.OrdinalIgnoreCase)) {
                Gestor.JuegosDisponibles = Gestor.MergeSort(Gestor.JuegosDisponibles);
                Console.WriteLine("Catálogo ordenado correctamente con MergeSort."); 
            } else {
                throw new ArgumentException("Algoritmo de ordenamiento desconocido."); 
            }
        }

        // Ordena una colección específica por HorasJuego y retorna la suma total de horas
        public int OrdenarColeccionYCalcularHoras(Usuario usuario, string nombreColeccion)
        {
            var coleccion = usuario.ObtenerColeccion(nombreColeccion);

            // Ordenar los juegos de la colección por HorasJuego (ascendente)
            var listaOrdenada = coleccion.OrderBy(j => j.HorasJuego).ToList();
            usuario.Colecciones[nombreColeccion] = listaOrdenada;

            // Retorna la suma total de horas
            return listaOrdenada.Sum(j => j.HorasJuego);
        }

        public void ExportarColecciones(Usuario usuario, string ruta) { 
            Exportador.ExportarATxt(usuario, ruta);
        }
    }
}
