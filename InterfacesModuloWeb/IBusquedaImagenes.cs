using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesModuloWeb
{
    public interface IBusquedaImagenes
    {
        List<string> buscarImagen(byte[] bytefile);
        string getName();
        string getVersion();
    }
}
