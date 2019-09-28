using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED2_Lab1.Models
{
    public class Archivo : IComparable<Archivo>
    {
        public DateTime fechaCompresion;
        public string nombreArchivo;
        public double razon_compresion;
        public double factor_compresion;
        public double porcentaje_reduccion;

        public Archivo()
        {
            fechaCompresion = new DateTime();
            nombreArchivo = default(string);
            razon_compresion = default(double);
            factor_compresion = default(double);
            porcentaje_reduccion = default(double);

        }

        public int CompareTo(Archivo other)
        {
            return this.fechaCompresion.CompareTo(other.fechaCompresion);
        }
    }
}