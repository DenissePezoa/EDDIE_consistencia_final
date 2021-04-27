using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;

namespace InterfacesModuloConsistencia
{
    public interface ICommentsPD
    {
        string GetName();
        string GetVersion();

        List<List<string>> GetComments(string namePDF, Mat _frame, int page);

        void SaveComments(string namePDF, int page);
    }
}
