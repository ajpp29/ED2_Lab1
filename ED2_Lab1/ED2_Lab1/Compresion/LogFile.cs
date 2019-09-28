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
            double razon_compresion = default(int);
            double factor_compresion = default(int);
            double porcentaje_reduccion = default(int);
            DateTime dateTime = DateTime.Now;

            double tamanio_aux = (double)tamanio_compreso / (double)tamanio_original;
            double reduccion_aux = ((double)Math.Abs(tamanio_original - tamanio_compreso) / (double)tamanio_original);

            razon_compresion = tamanio_aux;
            factor_compresion = 1 / razon_compresion;
            porcentaje_reduccion = reduccion_aux;

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filepath = filepath + @"\Log.txt";

            Archivo archivo = new Archivo();
            archivo.fechaCompresion = dateTime;
            archivo.nombreArchivo = nombre_original;
            archivo.razon_compresion = razon_compresion;
            archivo.factor_compresion = factor_compresion;
            archivo.porcentaje_reduccion = porcentaje_reduccion;

            StreamWriter escribir = File.AppendText(filepath);
            escribir.WriteLine("Fecha Compresion," + dateTime.ToString("dd/MM/yyyy HH:mm:ss") + ";Nombre," + nombre_original + ";Razon," + razon_compresion + ";Factor," + factor_compresion + ";Porcentaje de reduccion," + porcentaje_reduccion);
            escribir.Close();

            ListaHistorial.Add(archivo);
            
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
                aAux.razon_compresion = double.Parse(splitCadena[2].Split(',')[1].Trim());
                aAux.factor_compresion = double.Parse(splitCadena[3].Split(',')[1].Trim());
                aAux.porcentaje_reduccion = double.Parse(splitCadena[4].Split(',')[1].Trim());

                ListaHistorial.Add(aAux);

                cadena = leer.ReadLine();
            }

            ListaHistorial.Sort();

            return ListaHistorial;
        }
    }
}