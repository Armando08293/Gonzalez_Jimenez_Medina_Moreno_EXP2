using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Interfaces;

public interface IExportador
{
    void ExportarATxt(Usuario usuario, string rutaArchivo);
}
