using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ED2_Lab1.Models;
using System.Web.Mvc;
using System.IO;
using ED2_Lab1.Models;
using ED2_Lab1.DBContext;
using System.Text;
using ED2_Lab1.Compresion;

namespace ED2_Lab1.Controllers
{
    public class LZWController : Controller
    {
        public DefaultConnectionLZW db = DefaultConnectionLZW.getInstance;
        LZW algoritmo = new LZW();
        static LogFile log = new LogFile();

        //HOME DE LZW
        public ActionResult Index()
        {
            //List<int> compreso = algoritmo.Compresion("HOLACOMOESTASMAJE");
            //string descompreso = algoritmo.Descompresion(compreso);
            return View(db.ObtenerHistorial());
        }

        //COMPRESION DE ARCHIVO 
        [HttpGet]
        public ActionResult CompressFile()
        {
            return View();
        }
        //COMPRESION DE ARCHIVO 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompressFile(HttpPostedFileBase File)
        {
            string filePath = string.Empty;
            if (File != null)
            {
                string path = Server.MapPath("~/UploadedFiles/");
                filePath = path + Path.GetFileName(File.FileName);
                string extension = Path.GetExtension(File.FileName);
                File.SaveAs(filePath);
                ViewBag.Message = "Archivo Cargado";

                FileInfo fileInfo = new FileInfo(filePath);

                string nombre_original = fileInfo.Name;
                long tamanio_original = fileInfo.Length;

                db.GuardarArchivo(fileInfo);
                GenerarArhivoComprimido();
                //db.GuardarArchivo(fileInfo);
                //DownloadFile(fileInfo);

                
                log.GenerarLog(nombre_original, tamanio_original, db.ObtenerArchivo().Length);
                db.GuardarHistorial(log.ListaHistorial);

                return RedirectToAction("DownloadFile");
            }

            return View();
        }

        public void GenerarArhivoComprimido()
        {
            byte[] filedata = System.IO.File.ReadAllBytes(db.ObtenerArchivo().FullName);
            List<int> compreso = algoritmo.Compresion(System.Text.Encoding.UTF8.GetString(filedata, 0, filedata.Length));

            List<char> bytecompress = new List<char>();

            foreach (int  numero in compreso)
            {
                bytecompress.Add((char)numero);
            }

            ////////
            var ruta = Server.MapPath("~/DownloadedFiles/") + db.ObtenerArchivo().Name.Split('.')[0]+".lzw";
            using (StreamWriter outputFile= new StreamWriter(ruta))
            {
                foreach (char caracter in bytecompress)
                {
                    outputFile.Write(caracter.ToString());
                }
            }

            db.GuardarArchivo(new FileInfo(ruta));
        }


        //DESCOMPRESION DE ARCHIVO
        [HttpGet]
        public ActionResult DecompressFile()
        {
            return View();
        }
        //DESCOMPRESION DE ARCHIVO 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DecompressFile(HttpPostedFileBase File)
        {
            string filePath = string.Empty;
            if (File != null)
            {
                string path = Server.MapPath("~/UploadedFiles/");
                filePath = path + Path.GetFileName(File.FileName);
                string extension = Path.GetExtension(File.FileName);
                File.SaveAs(filePath);
                ViewBag.Message = "Archivo Cargado";

                FileInfo fileInfo = new FileInfo(filePath);

                string nombre_original = fileInfo.Name;
                long tamanio_original = fileInfo.Length;

                db.GuardarArchivo(fileInfo);
                GenerarArchivoDescomprimido();
                //db.GuardarArchivo(fileInfo);
                //DownloadFile(fileInfo);

                return RedirectToAction("DownloadFile");
            }

            return View();
        }

        public void GenerarArchivoDescomprimido()
        {
            byte[] filedata = System.IO.File.ReadAllBytes(db.ObtenerArchivo().FullName);

            List<int> bytedecompress = new List<int>();

            foreach (byte bite in filedata)
            {
                bytedecompress.Add((int)Convert.ToChar(bite));
            }

            StringBuilder builder = new StringBuilder(algoritmo.Descompresion(bytedecompress));

            ////////
            var ruta = Server.MapPath("~/DownloadedFiles/") + db.ObtenerArchivo().Name.Split('.')[0]+".txt";
            using (StreamWriter outputFile= new StreamWriter(ruta))
            {
                outputFile.Write(builder);
            }

            db.GuardarArchivo(new FileInfo(ruta));
        }

        //METODO PARA DESCARGAR EL ARCHIVO
        public ActionResult DownloadFile()
        {
            //FileInfo fileInfo = default(FileInfo);
            //fileInfo = db.ObtenerArchivo();
            //var ruta = Server.MapPath("~/UploadedFiles/") + fileInfo.Name;
            //string filename = fileInfo.Name + ".lzw";
            ////string filepath = AppDomain.CurrentDomain.BaseDirectory + filename;
            //string filepath = fileInfo.FullName;
            //byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            //string contentType = MimeMapping.GetMimeMapping(filepath);

            //var cd = new System.Net.Mime.ContentDisposition
            //{
            //    FileName = filename,
            //    Inline = true,
            //};

            //Response.Headers.Add("Content-Disposition", cd.ToString());

            //return File(filedata, contentType, filename);
            return View();
        }

        //DESCARGAR EL ARCHIVO
        public FileResult Download()
        {
            
            var ruta = Server.MapPath("~/DownloadedFiles/")+ db.ObtenerArchivo().Name;
            return File(ruta, "text/plain", db.ObtenerArchivo().Name);
        }
    }
}