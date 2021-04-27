using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ModuloRastreoOcular
{
    public class ReticleDrawing
    {
        //  Attributes for managing reticle drawing: The image to be used, the transparent form where it will be drawn, and it's coordinates
        public Image reticle;
        private Form transparentForm;
        private int x, y;

        //  Class initialization
        public ReticleDrawing(string reticleRoute)
        {
            reticle = Image.FromFile(reticleRoute);
            transparentForm = new Form
            {
                BackColor = Color.White,
                TransparencyKey = Color.White,
                FormBorderStyle = FormBorderStyle.None,
                Bounds = Screen.PrimaryScreen.Bounds,
                TopMost = true
            };
            transparentForm.Paint += TransparentForm_Paint;
            transparentForm.Show();
        }

        /// <summary>
        /// Method in charge of updating the reticle's position when new coordinates are received
        /// </summary>
        /// <param name="xCoord">X coordinate where the reticle will be drawn</param>
        /// <param name="yCoord">Y coordinate where the reticle will be drawn</param>
        public void UpdateData(string xCoord, string yCoord)
        {
            x = Int32.Parse(xCoord) - reticle.Width / 2;
            y = Int32.Parse(yCoord) - reticle.Height / 2;
            //  In order to avoid a cross-thread exception, the Invoke method is used to call a method that invalidates and updates the
            //  transparent form.
            if (transparentForm.InvokeRequired)
            {
                transparentForm.Invoke(new MethodInvoker(UpdateGUI));
            }
        }

        public void UpdateGUI() 
        {
            transparentForm.Invalidate();
            transparentForm.Update();
        }

        public void ClearUp() 
        {
            transparentForm.Close();
        }

        /// <summary>
        /// Method that gets called whenever the transparent form has to be redrawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransparentForm_Paint(object sender, PaintEventArgs e)
        {
            if(reticle != null)
            {
                Point ulCorner = new Point(x, y);
                Size imgSize = new Size(reticle.Width, reticle.Height);
                Rectangle destRec = new Rectangle(ulCorner, imgSize);
                

                e.Graphics.DrawImage(reticle, destRec);
            }
        }
    }

}
