using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Emgu.CV.Structure;
using Newtonsoft.Json.Linq;
namespace ModuloVisualizacionDatos
{
    public partial class FiguresDP : PictureBox
    {
        JObject figures= new JObject();
        private System.ComponentModel.IContainer components = null;
        private Font fnt = new Font("Arial", 10);
        //private PictureBox pictureBox6 = new PictureBox();
        public FiguresDP(JObject figuresIn)
        {
            
            Size = new Size(400, 450);
            //new Point(285, 65);
            Location = new Point(295, 70);
            BackColor = Color.FromArgb(255, 0, 0);
           
            BringToFront();
            figures = figuresIn;
            //Paint += new System.Windows.Forms.PaintEventHandler();
            InitializeComponent();
            //base.Dock = DockStyle.None;
            //base.BackColor = Color.Red;
            // Connect the Paint event of the PictureBox to the event handler method.
            
            /*
            base.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox18_Paint);
            base.Location = new Point(285, 65);
            base.Width = 520;
            base.Height = 400;
            Refresh();*/
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
        }

        /*private void pictureBox18_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            g.DrawString("This is a diagonal line drawn on the control",
                fnt, System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            g.DrawLine(System.Drawing.Pens.Red, base.Left, base.Top,
                base.Right, base.Bottom);
        }*/

        public void RuntimeResize(int x, int y, int width, int height)
        {
            Location = new Point(x, y);
            Size = new Size(width, height);
            Refresh();
        }

       

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Console.WriteLine("entro a dibujar ");
            //Si hay figuras cargadas dibujar
            if (figures != null)
            {
                //e.Graphics.DrawRectangle(Pens.Red, GetRectangle());
                //pe.Graphics.FillRectangle(Brushes.Yellow, GetRectangle(highlightHeight));
                //PaintHighlight(pe);
                int k = 0;
                
                Console.WriteLine("esto es figures " + figures.ToString());
               
                foreach (var item in figures["figuras"]["rectangulos"])
                {
                    Console.WriteLine("item en rect "+ item["puntoX"]);
                    int puntoX = (int)item["puntoX"];
                    int puntoY = (int)item["puntoY"];
                    int puntoW = (int)item["width"];
                    int puntoH = (int)item["height"];
                    //Rectangle rect_aux2 = new Rectangle(100, 100, 100, 100);
                    //pe.Graphics.DrawRectangle(System.Drawing.Pens.Yellow, rect_aux2);
                    Rectangle rect_aux = new Rectangle(puntoX,puntoY,puntoW,puntoH);
                    pe.Graphics.DrawRectangle(System.Drawing.Pens.Black, rect_aux);
                    k = k + 1;
                }
                int z = 0;
                foreach (var item2 in figures["figuras"]["circulos"])
                {
                    Console.WriteLine("item en circ " + item2["puntoX"]);
                   
                    int puntoX = (int)item2["puntoX"];
                    int puntoY = (int)item2["puntoY"];
                    int puntoW = (int)item2["width"];
                    int puntoH = (int)item2["height"];
                    Ellipse elip = new Ellipse(new PointF(puntoX, puntoY), new Size(puntoW, puntoH), 90);
                    RectangleF aux_elip = new RectangleF(new PointF(puntoX, puntoY), new Size(puntoW, puntoH));
                    //Rectangle rect_aux = new Rectangle(puntoX, puntoY, puntoW, puntoH);
                    //pe.Graphics.DrawRectangle(System.Drawing.Pens.Black, rect_aux);
                    pe.Graphics.DrawEllipse(System.Drawing.Pens.Black, aux_elip);
                    z = z + 1;
                }
                
                /*Console.WriteLine("item en circ " + figures["figuras"]["circulos"]["circ1"]["puntoX"]);
                int puntoX1 = (int)figures["figuras"]["circulos"]["circ1"]["puntoX"];
                int puntoY1 = (int)figures["figuras"]["circulos"]["circ1"]["puntoY"];
                int puntoW1 = (int)figures["figuras"]["circulos"]["circ1"]["width"];
                int puntoH1 = (int)figures["figuras"]["circulos"]["circ1"]["height"];
                Ellipse elip1 = new Ellipse(new PointF(puntoX1, puntoY1), new Size(puntoW1, puntoH1), 90);
                RectangleF aux_elip1 = new RectangleF(new PointF(puntoX1, puntoY1), new Size(puntoW1, puntoH1));
                //Rectangle rect_aux = new Rectangle(puntoX, puntoY, puntoW, puntoH);
                //pe.Graphics.DrawRectangle(System.Drawing.Pens.Black, rect_aux);
                pe.Graphics.DrawEllipse(System.Drawing.Pens.Black, aux_elip1);*/

                //pe.Graphics.DrawLine(System.Drawing.Pens.Red, 0, 0, 400, 400);

                //pe.Graphics.DrawString("Correccion parte dosss",
                //fnt, System.Drawing.Brushes.Blue, new Point(30, 30));

            }


        }


    }
}
