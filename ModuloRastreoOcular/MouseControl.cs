using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ModuloRastreoOcular
{
    class MouseControl
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);


        /// <summary>
        /// Method used to move the cursor to a new position based on a pair of coordinates
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        public void MoveCursor(string xCoordinate, string yCoordinate)
        {
            int x = Int32.Parse(xCoordinate);
            int y = Int32.Parse(yCoordinate);
            SetCursorPos(x, y);
        }
    }
}
