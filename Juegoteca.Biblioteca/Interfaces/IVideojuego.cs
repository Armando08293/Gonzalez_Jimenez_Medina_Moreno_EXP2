namespace Juegoteca.Biblioteca.Interfaces;

public interface IVideojuego
{
    string Titulo { get; }
    string Estudio { get; }
    int HorasJuego { get; }
    string ToString();
}
