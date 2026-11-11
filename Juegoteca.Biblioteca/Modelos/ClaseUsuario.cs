public class Usuario {
    public string Nombre { get; set; }
    public Dictionary<string, List<Videojuego>> Colecciones { get; set; }
    public Usuario(string nombre) 
        { Nombre = nombre;
        Colecciones= new Dictionary<string, List<Videojuego>>();
    }
}
