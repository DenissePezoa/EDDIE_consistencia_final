using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using InterfacesModuloConsistencia;
namespace ConsistencyLibraryPageMarkerDP
{
    public class PageMarkerDP : IPageMarker
    { 

        public List<string> GetPageMarker(string nombrePDF)
        {
            string oldFile = nombrePDF;
            string newFile = "temporal.pdf";
            PdfReader reader = new PdfReader(oldFile);
            IList<Dictionary<String, Object>> bmProperties = SimpleBookmark.GetBookmark(reader);
            PdfStamper stamp = new PdfStamper(reader, new FileStream(newFile, FileMode.Create));
            //List<List<string>> marcadores_final = new List<List<string>>();
            List<string> marcadores_final = new List<string>();
            if (bmProperties != null)
            {
                //listBox1.Items.Clear();
                //MessageBox.Show("Cantidad marcadores " + bmProperties.Count());
                List<string> aux_marcador = new List<string>();

                foreach (IDictionary<String, Object> bmProperty in bmProperties)
                {

                    aux_marcador.Clear();
                    // MessageBox.Show("pagina " + bmProperty["Page"] + " - Titulo " + bmProperty["Title"]);
                    var getPage = bmProperty["Page"];
                    var getTittle = bmProperty["Title"];

                    var aux_pagina = getPage.ToString().Split(' ');
                    var pagina = aux_pagina[0];
                    var titulo = getTittle;
                    aux_marcador.Add(pagina.ToString());
                    aux_marcador.Add(titulo.ToString());
                    //marcadores_final.Add(aux_marcador);
                    marcadores_final.Add(titulo + " Pag." + pagina);
                    //Console.WriteLine("esto tengo en la lista pagina :" + pagina);
                    
                   
                    
                    //listBox1.Items.Add(titulo + "  Pag. " + pagina);

                    //}
                }
                stamp.Close();
                reader.Close();
            }

            foreach (var item in marcadores_final)
            {
                Console.WriteLine("uno " + item);
            }
            return marcadores_final;

        }

        public int DeleteMarker(string marker, string namePDF)
        {

            string[] delimiters = new string[] { " Pag." };
            string[] items = marker.Split(delimiters, StringSplitOptions.None);
            string markerTitle = items[0];
            string markerPage = items[1];
            markerTitle = markerTitle.Trim();
            markerPage = markerPage.Trim();
            Console.WriteLine("Esto comparo :" + markerTitle + "___" + markerPage);
            string oldFile = namePDF;
            string newFile = "temporal.pdf";
            PdfReader reader = new PdfReader(oldFile);
            IList<Dictionary<String, Object>> bmProperties = SimpleBookmark.GetBookmark(reader);
            PdfStamper stamp = new PdfStamper(reader, new FileStream(newFile, FileMode.Create));
            int index = -1;
            int i = 0;
            foreach (IDictionary<String, Object> bmProperty in bmProperties)
            {
                var getPage = bmProperty["Page"];
                var getTittle = bmProperty["Title"];

                var aux_pagina = getPage.ToString().Split(' ');
                var pagina = aux_pagina[0];
                //Console.WriteLine("Esto saco :" + getTittle + "___" + pagina);
                //Console.WriteLine("elemento " + bmProperty);
                if (markerTitle == (string)getTittle && markerPage == (string)pagina)
                {
                    Console.WriteLine("Se elimina este marcador en pos " + i);
                    index = i;
                }
                i += 1;
            }
            if (index == -1)
            {
                stamp.Close();
                reader.Close();
                return 0;
            }
            else
            {
                bmProperties.RemoveAt(index);
                stamp.Outlines = bmProperties;
                stamp.Close();
                //System.Diagnostics.Process.Start(newFile);
                //Console.Read();
                reader.Close();
                File.Replace(newFile, oldFile, @"backup.pdf.bac");
                return 1;
            }

            
            //MessageBox.Show("Pdf modificado con exito, se ha guardado un backup de la versión anterior ");
            //listBox1.Items.Remove(marcador);
           
        }
        public string GetName()
        {
            string name = "PageMarkerDP";
            return name;
        }

        public string GetVersion()
        {
            string version = "1.0";
            return version;
        }
    }
}
