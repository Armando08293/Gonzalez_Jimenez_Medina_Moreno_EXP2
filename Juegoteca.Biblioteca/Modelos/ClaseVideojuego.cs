
using Juegoteca.Biblioteca.Interfaces;

public class Videojuego: IVideojuego {
    public string Titulo { get; set; }
    public string Estudio { get; set; }
    public int HorasJuego { get; set; }

    public Videojuego(string titulo, string estudio, int horasJuego) {
        if (horasJuego <= 0) {
            throw new ArgumentException("Las horas no pueden ser cero");
        }
        Titulo = titulo;
        Estudio = estudio;
        HorasJuego = horasJuego;

    }

    public override string ToString() {
        return $" {Titulo}- {Estudio} ({HorasJuego}h)";
    }
}









