using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesModuloConsistencia
{
    public interface IPageMarker
    {
        List<string> GetPageMarker(string nombrePDF);
        string GetName(); 
        string GetVersion();
    }
}
