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
        public int razon_compresion;
        public int factor_compresion;
        public int porcentaje_reduccion;

        public Archivo()
        {
            fechaCompresion = new DateTime();
            nombreArchivo = default(string);
            razon_compresion = default(int);
            factor_compresion = default(int);
            porcentaje_reduccion = default(int);

        }

        public int CompareTo(Archivo other)
        {
            return this.fechaCompresion.CompareTo(other.fechaCompresion);
        }
    }
}