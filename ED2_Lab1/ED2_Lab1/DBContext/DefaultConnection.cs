using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED2_Lab1.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public List<Models.Archivo> historialCompresiones = new List<Models.Archivo>();
        public int IdActual { get; set; }

        public DefaultConnection()
        {
            IdActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}