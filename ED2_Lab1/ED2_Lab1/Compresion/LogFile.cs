using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ED2_Lab1.Models;

namespace ED2_Lab1.Compresion
{
    public class LogFile
    {
        public List<Archivo> ListaHistorial;

        public LogFile()
        {
            ListaHistorial = new List<Archivo>();
        }

        public void GenerarLog(string nombre_original,long tamanio_original, long tamanio_compreso)
        {
            int razon_compresion = default(int);
            int factor_compresion = default(int);
            int porcentaje_reduccion = default(int);
            DateTime dateTime = DateTime.Today;

            long tamanio_aux = tamanio_compreso / tamanio_original;
            long reduccion_aux = (Math.Abs(tamanio_original - tamanio_compreso) / tamanio_original);

            razon_compresion = unchecked((int)tamanio_aux);
            factor_compresion = 1 / razon_compresion;
            porcentaje_reduccion = unchecked((int)reduccion_aux);

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filepath = filepath + @"\Log.txt";

            StreamWriter escribir = File.AppendText(filepath);
            escribir.WriteLine("Fecha Compresion," + dateTime.ToString("dd/MM/yyyy HH:mm:ss") + ";Nombre," + nombre_original + ";Razon," + razon_compresion + ";Factor," + factor_compresion + ";Porcentaje de reduccion," + porcentaje_reduccion);
            escribir.Close();

        }

        public List<Archivo> ObtenerHistorial()
        {
            ListaHistorial = new List<Archivo>();

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filepath = filepath + @"\Log.txt";

            StreamReader leer = new StreamReader(filepath);

            string cadena = leer.ReadLine();
            string[] splitCadena = new string[5];

            while (cadena != null)
            {
                Archivo aAux = new Archivo();
                splitCadena = cadena.Split(';');
                aAux.fechaCompresion = DateTime.ParseExact(splitCadena[0].Split(',')[1].Trim(), "dd/MM/yyyy HH:mm:ss", null);
                aAux.nombreArchivo = splitCadena[1].Split(',')[1].Trim();
                aAux.razon_compresion = int.Parse(splitCadena[2].Split(',')[1].Trim());
                aAux.factor_compresion = int.Parse(splitCadena[3].Split(',')[1].Trim());
                aAux.porcentaje_reduccion = int.Parse(splitCadena[4].Split(',')[1].Trim());

                ListaHistorial.Add(aAux);

                cadena = leer.ReadLine();
            }

            ListaHistorial.Sort();

            return ListaHistorial;
        }
    }
}