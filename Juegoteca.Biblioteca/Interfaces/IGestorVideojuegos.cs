using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Interfaces;

public interface IGestorVideojuegos
{
    List<Videojuego> JuegosDisponibles { get; }

    void AgregarJuego(Videojuego juego);
    bool EliminarJuego(Videojuego juego);
    bool EliminarPorIndice(int indice);
    List<Videojuego> BuscarPorTitulo(string texto);
    void LimpiarCatalogo();

    void QuickSort(List<Videojuego> lista);
    List<Videojuego> MergeSort(List<Videojuego> lista);
    int BusquedaBinaria(string titulo);
}
