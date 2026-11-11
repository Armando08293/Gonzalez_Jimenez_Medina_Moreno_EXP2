using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;

// 1. Inicialización
var gestor = new GestorVideojuegos();
var exportador = new ExportadorTxt();
var servicio = new ServicioVideojuegos(gestor, exportador);

gestor.AgregarJuego(new Videojuego("Celeste", "Maddy Makes Games", 8));
gestor.AgregarJuego(new Videojuego("Hollow Knight", "Team Cherry", 40));
gestor.AgregarJuego(new Videojuego("Minecraft", "Mojang", 100));
gestor.AgregarJuego(new Videojuego("Tetris", "Alexey Pajitnov", 5));
gestor.AgregarJuego(new Videojuego("Portal 2", "Valve", 10));
gestor.AgregarJuego(new Videojuego("Stardew Valley", "ConcernedApe", 60));
gestor.AgregarJuego(new Videojuego("Hades", "Supergiant", 25));
gestor.AgregarJuego(new Videojuego("Zelda: BOTW", "Nintendo", 120));

Console.WriteLine("Bienvenido al Sistema de Gestión de Videojuegos");

// 2. Registro de usuario
Console.WriteLine("\n--- REGISTRO DE USUARIO ---");
Console.Write("Ingrese su nombre de usuario: ");
string nombre = Console.ReadLine() ?? "";
Usuario usuario = servicio.RegistrarUsuario(nombre);
Console.WriteLine($"Usuario '{usuario.Nombre}' registrado. ¡Bienvenido/a, {usuario.Nombre}!");

// 3. Creación de colección inicial
Console.Write("Nombre de su primera colección: ");
string coleccionActual = Console.ReadLine() ?? "";
usuario.CrearColeccion(coleccionActual);
Console.WriteLine($"Colección '{coleccionActual}' creada exitosamente.");

// 4. Menú principal
bool salir = false;
while (!salir) {
    Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
    Console.WriteLine($"Colección actual: '{coleccionActual}' ({usuario.ObtenerColeccion(coleccionActual).Count} juegos)");
    Console.WriteLine("1. Mostrar juegos disponibles");
    Console.WriteLine("2. Ordenar colección (QuickSort o MergeSort)");
    Console.WriteLine("3. Búsqueda binaria en catálogo");
    Console.WriteLine("4. Crear nueva colección");
    Console.WriteLine("5. Cambiar de colección actual");
    Console.WriteLine("6. Ver mis colecciones");
    Console.WriteLine("7. Exportar mis colecciones a .txt");
    Console.WriteLine("8. Salir");
    Console.Write("Seleccione una opción: ");

    if (!int.TryParse(Console.ReadLine(), out int opcion)) {
        Console.WriteLine("Entrada inválida. Escriba un número del 1 al 8.");
        continue;
    }

    switch (opcion) {
        case 1:
            // Juegos del catálogo que aún no están en la colección actual
            var actual = usuario.ObtenerColeccion(coleccionActual);
            var disponibles = gestor.JuegosDisponibles.Where(j => !actual.Contains(j)).ToList();
            Console.WriteLine("\nJuegos que puede agregar:");
            foreach (var j in disponibles)
                Console.WriteLine($"  {j}");
            break;

        case 2:
            Console.Write("Algoritmo (1 = QuickSort, 2 = MergeSort): ");
            if (!int.TryParse(Console.ReadLine(), out int alg) || (alg != 1 && alg != 2)) {
                Console.WriteLine("Opción inválida.");
                break;
            }
            try {
                servicio.OrdenarCatalogo(alg == 1 ? "quick" : "merge");
                int total = servicio.OrdenarColeccionYCalcularHoras(usuario, coleccionActual);
                Console.WriteLine($"Colección ordenada. Total de horas: {total} h");
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;

        case 3:
            Console.Write("Título a buscar: ");
            string titulo = Console.ReadLine() ?? "";
            if (gestor.BusquedaBinaria(titulo) == -1) {
                Console.WriteLine("No se encontraron coincidencias.");
                break;
            }
            var coincidencias = gestor.BuscarPorTitulo(titulo);
            for (int i = 0; i < coincidencias.Count; i++)
                Console.WriteLine($"  {i + 1}. {coincidencias[i]}");
            Console.Write("Número de juego a agregar (0 para cancelar): ");
            if (int.TryParse(Console.ReadLine(), out int elegido) && elegido >= 1 && elegido <= coincidencias.Count) {
                usuario.AgregarJuegoAColeccion(coleccionActual, coincidencias[elegido - 1]);
                Console.WriteLine("Juego agregado.");
            } else {
                Console.WriteLine("Cancelado.");
            }
            break;

        case 4:
            Console.Write("Nombre de la nueva colección: ");
            string nueva = Console.ReadLine() ?? "";
            try {
                usuario.CrearColeccion(nueva);
                Console.WriteLine($"Colección '{nueva}' creada exitosamente.");
            } catch (InvalidOperationException) {
                Console.WriteLine("Esa colección ya existe.");
            }
            break;

        case 5:
            Console.WriteLine("Sus colecciones:");
            foreach (var nombreCol in usuario.Colecciones.Keys)
                Console.WriteLine($"  - {nombreCol}");
            Console.Write("Nombre de la colección a usar: ");
            string elegida = Console.ReadLine() ?? "";
            if (usuario.Colecciones.ContainsKey(elegida)) {
                coleccionActual = elegida;
                Console.WriteLine($"Colección actual: '{coleccionActual}'.");
            } else {
                Console.WriteLine("Esa colección no existe.");
            }
            break;

        case 6:
            usuario.MostrarColecciones();
            break;

        case 7:
            string ruta = $"{usuario.Nombre}_colecciones.txt";
            servicio.ExportarColecciones(usuario, ruta);
            Console.WriteLine($"Archivo creado: {ruta}");
            break;

        case 8:
            salir = true;
            Console.WriteLine("¡Hasta luego!");
            break;

        default:
            Console.WriteLine("Opción fuera de rango. Elija del 1 al 8.");
            break;
    }
}