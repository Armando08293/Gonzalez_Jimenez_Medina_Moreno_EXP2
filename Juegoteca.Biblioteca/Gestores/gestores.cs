using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;
//Correcto
namespace Juegoteca.Biblioteca.Gestores;

public class GestorVideojuegos : IGestorVideojuegos
{
    public List<Videojuego> JuegosDisponibles { get; }

    public GestorVideojuegos()
    {
        JuegosDisponibles = new List<Videojuego>();
    }

    public void AgregarJuego(Videojuego juego)
    {
        if (!JuegosDisponibles.Any(j => j.Titulo.Equals(juego.Titulo,StringComparison.OrdinalIgnoreCase)))
        {
            JuegosDisponibles.Add(juego);
        }
    }

    public bool EliminarJuego(Videojuego juego)
    {
        return JuegosDisponibles.Remove(juego);
    }

    public bool EliminarPorIndice(int indice)
    {
        if (indice >= 0 && indice < JuegosDisponibles.Count)
        {
            JuegosDisponibles.RemoveAt(indice);
            return true;
        }
        return false;
    }

    public List<Videojuego> BuscarPorTitulo(string texto)
    {
        return JuegosDisponibles
            .Where(j => j.Titulo.Contains(texto,StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void LimpiarCatalogo()
    {
        JuegosDisponibles.Clear();
    }

    //Algorimos 
    public void QuickSort(List<Videojuego> lista)
    {
        QuickSortRec(lista,0,lista.Count - 1);
    }

    private void QuickSortRec(List<Videojuego> lista,int izq,int der)
    {
        if (izq < der)
        {
            int pivote = Particionar(lista,izq,der);
            QuickSortRec(lista,izq,pivote - 1);
            QuickSortRec(lista,pivote + 1,der);
        }
    }

    private int Particionar(List<Videojuego> lista,int izq,int der)
    {
        int pivoteHoras = lista[der].HorasJuego;
        int i = izq - 1;

        for (int j = izq; j < der; j++)
        {
            if (lista[j].HorasJuego <= pivoteHoras)
            {
                i++;
                var temp = lista[i];
                lista[i] = lista[j];
                lista[j] = temp;
            }
        }
        var tempPivote = lista[i + 1];
        lista[i + 1] = lista[der];
        lista[der] = tempPivote;
        return i + 1;
    }

    public List<Videojuego> MergeSort(List<Videojuego> lista)
    {
        if (lista.Count <= 1) return new List<Videojuego>(lista);

        int medio = lista.Count / 2;
        var izq = MergeSort(lista.GetRange(0,medio));
        var der = MergeSort(lista.GetRange(medio,lista.Count - medio));

        return Unir(izq,der);
    }

    private List<Videojuego> Unir(List<Videojuego> izq,List<Videojuego> der)
    {
        var resultado = new List<Videojuego>();
        int i = 0, j = 0;

        while (i < izq.Count && j < der.Count)
        {
            if (izq[i].HorasJuego <= der[j].HorasJuego)
            {
                resultado.Add(izq[i]);
                i++;
            }
            else
            {
                resultado.Add(der[j]);
                j++;
            }
        }

        while (i < izq.Count) resultado.Add(izq[i++]);
        while (j < der.Count) resultado.Add(der[j++]);

        return resultado;
    }

    public int BusquedaBinaria(string titulo)
    {
        var copia = JuegosDisponibles.OrderBy(j => j.Titulo,StringComparer.OrdinalIgnoreCase).ToList();
        int izq = 0;
        int der = copia.Count - 1;

        while (izq <= der)
        {
            int medio = izq + (der - izq) / 2;
            int comparacion = string.Compare(copia[medio].Titulo,titulo,StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
            {
                return JuegosDisponibles.IndexOf(copia[medio]);
            }
            if (comparacion < 0)
            {
                izq = medio + 1;
            }
            else
            {
                der = medio - 1;
            }
        }
        return -1;
    }
}