using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ED2_Lab1.DBContext
{
    public class DefaultConnectionLZW
    {
        private static volatile DefaultConnectionLZW Instance;
        private static object syncRoot = new Object();

        public static List<Models.Archivo> historialCompresiones = new List<Models.Archivo>();
        public static FileInfo fileInfo = default(FileInfo);

        public int IdActual { get; set; }

        public DefaultConnectionLZW()
        {
            IdActual = 0;
        }

        public void GuardarArchivo(FileInfo file)
        {
            fileInfo = file;
        }

        public List<Models.Archivo> ObtenerHistorial()
        {
            return historialCompresiones;
        }

        public FileInfo ObtenerArchivo()
        {
            return fileInfo;
        }

        public static DefaultConnectionLZW getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnectionLZW();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}