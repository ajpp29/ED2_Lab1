using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ED2_Lab1.Compresion
{
    public class LogFile
    {
        public LogFile()
        {

        }

        public void GenerarLog(string nombre_original,int razon_compresion, int factor_compresion,int porcentaje_reduccion)
        {
            //int razon_compresion;
            //int factor_compresion;
            //int porcentaje_reduccion;

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filepath = filepath + @"\Log.txt";

            StreamWriter escribir = File.AppendText(filepath);
            escribir.WriteLine("Nombre:" + nombre_original + ";Razon:" + razon_compresion + ";Factor:" + factor_compresion + ";Porcentaje de reduccion:" + porcentaje_reduccion);
            escribir.Close();

        }
    }
}