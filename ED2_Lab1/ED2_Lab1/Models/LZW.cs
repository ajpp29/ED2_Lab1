using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ED2_Lab1.Models
{
    public class LZW
    {
        public  List<int> Compresion(string Descomprimido)
        {
            // Crando Diccionario
            Dictionary<string, int> Diccionario = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
                Diccionario.Add(((char)i).ToString(), i);

            string w = string.Empty;
            List<int> ArchivoComp = new List<int>();

            foreach (char c in Descomprimido)
            {
                string wc = w + c;
                if (Diccionario.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    ArchivoComp.Add(Diccionario[w]);
                    Diccionario.Add(wc, Diccionario.Count);
                    w = c.ToString();
                }
            }
            if (!string.IsNullOrEmpty(w))
                ArchivoComp.Add(Diccionario[w]);

            return ArchivoComp;
        }

        public string Descompresion(List<int> Comprimido)
        {
            // Crando Diccionario
            Dictionary<int, string> Diccionario = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
                Diccionario.Add(i, ((char)i).ToString());

            string w = Diccionario[Comprimido[0]];
            Comprimido.RemoveAt(0);
            StringBuilder Descomprimido = new StringBuilder(w);

            foreach (int k in Comprimido)
            {
                string entry = null;
                if (Diccionario.ContainsKey(k))
                    entry = Diccionario[k];
                else if (k == Diccionario.Count)
                    entry = w + w[0];

                Descomprimido.Append(entry);

                // Agregando al diccionario la entrada
                Diccionario.Add(Diccionario.Count, w + entry[0]);
                w = entry;
            }

            return Descomprimido.ToString();
        }
    }
}