using Juegoteca.Biblioteca.Interfaces;

namespace Juegoteca.Biblioteca.Modelos;

public class Videojuego : IVideojuego
{
    public string Titulo { get; set; } = string.Empty;
    public string Estudio { get; set; } = string.Empty;
    public int HorasJuego { get; set; }
}
