using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using InterfacesModuloConsistencia;
namespace ConsistencyLibraryCommentsDP
{
    public class CommentsDP : ICommentsDP
    {
        
        public List<List<string>> GetComments(string namePDF)
        {
            List<List<string>> comments = new List<List<string>>();
            string oldFile = namePDF;
            //string newFile = "C:\\Users\\Denisse\\Desktop\\EDDIE-Augmented-Reading-master\\AugmentedReadingApp\\bin\\x86\\Debug\\temporal.pdf"
            PdfReader reader = new PdfReader(oldFile);
            PdfDictionary pageDict = reader.GetPageN(1);




            iTextSharp.text.Rectangle pagesize = reader.GetPageSize(1);

            double anchoPDF = pagesize.Width - 30;
            double altoPDF = pagesize.Height - 30;
            // MessageBox.Show("Tamano pagina pdf --> ancho :" + anchoPDF + " y alto : " + altoPDF);
            float anchoImagen = 400;
            float altoImagen = 450;
            //MessageBox.Show("Tamano imagen --> ancho :" + anchoImagen+ " y alto : " + altoImagen);

            PdfArray annotArray = pageDict.GetAsArray(PdfName.ANNOTS);

            for (int i = 0; i < annotArray.Size; ++i)
            {
                PdfDictionary curAnnot = annotArray.GetAsDict(i);
                var subtype = curAnnot.Get(PdfName.SUBTYPE);
                var rect = curAnnot.Get(PdfName.RECT);
                var contents = curAnnot.Get(PdfName.CONTENTS);
                var page = curAnnot.Get(PdfName.P);
                List<string> aux_commnents = new List<string>();
                //MessageBox.Show("Subtipo "+subtype + " coordenadas: "+rect + " y contenido "+contents);
                if (subtype.ToString() == "/Text")
                {
                    //MessageBox.Show("Comentarios Post it detectado coor "+rect);
                    //listBox2.Items.Add(contents + " " + page);
                    var aux_rect = rect.ToString().Split(',');
                    aux_rect[0] = aux_rect[0].Remove(0, 1);
                    aux_rect[0] = aux_rect[0].Replace(".", ",");
                    aux_rect[1] = aux_rect[1].Replace(".", ",");
                    int puntox1 = Convert.ToInt32(Convert.ToSingle(aux_rect[0]));
                    int puntoy1 = Convert.ToInt32(Convert.ToSingle(aux_rect[1]));
                    int nuevoX1 = (Convert.ToInt32(anchoImagen) * (puntox1 * 100 / Convert.ToInt32(anchoPDF))) / 100;
                    int nuevoY1 = Convert.ToInt32(altoImagen) - ((Convert.ToInt32(altoImagen) * (puntoy1 * 100 / Convert.ToInt32(altoPDF))) / 100);
                    aux_commnents.Add(contents.ToString());
                    aux_commnents.Add(page.ToString());
                    aux_commnents.Add(nuevoX1.ToString());
                    aux_commnents.Add(nuevoY1.ToString());
                    comments.Add(aux_commnents);
                    //MessageBox.Show("Esto es punto x1 "+nuevoX1+ " y y1 "+nuevoY1);
                    //ROi debe ser del mismo tamaño que la imagen a pegar
                    //Rectangle brect = new Rectangle(0, nuevoY1, icono.Width, icono.Height);
                    //fondo.ROI = brect;
                    //final = auxImge.Copy();
                    //CvInvoke.cvCopy(icono, fondo, IntPtr.Zero);
                    //pictureBox2.Image = fondo.Bitmap;


                    //CvInvoke.cvResetImageROI(fondo);
                }



            }
            return comments;
        }

        public string GetName()
        {
            string name = "CommentsDP";
            return name;
        }

        public string GetVersion()
        {
            string version = "1.0";
            return version;
        }

       



    }
}
