using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesModuloConsistencia
{
    public interface ICommentsDP
    {
        string GetName();
        string GetVersion();

        List<List<string>> GetComments(string namePDF);


    }
}
