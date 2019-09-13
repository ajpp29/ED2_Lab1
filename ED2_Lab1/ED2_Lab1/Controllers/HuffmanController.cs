﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ED2_Lab1.Models;
using ED2_Lab1.DBContext;
using ED2_Lab1.Compresion;

namespace ED2_Lab1.Controllers
{
    public class HuffmanController : Controller
    {
        public DefaultConnection db = DefaultConnection.getInstance;
        // GET: Huffman
        public ActionResult Index()
        {
            List<HuffmanNode> ListaNodos = new List<HuffmanNode>();
            ListaNodos = ObtenerLista();

            Huffman compresionHuffman = new Huffman();
            //compresionHuffman.GenerarArbol(ListaNodos);

            return View(db.historialCompresiones.ToList());
        }
        public List<HuffmanNode> ObtenerLista()
        {
            List<HuffmanNode> nodeList = new List<HuffmanNode>(); 
            try
            {
               
                FileStream stream = new FileStream("C://Users//carlo//Desktop//cerote.txt", FileMode.Open, FileAccess.Read);
                for (int i = 0; i < stream.Length; i++)
                {
                    string read = Convert.ToChar(stream.ReadByte()).ToString();
                    if (nodeList.Exists(x => x.caracter == read)) 
                        nodeList[nodeList.FindIndex(y => y.caracter == read)].Frecuencia(); 
                    else
                        nodeList.Add(new HuffmanNode(read));   
                }
                nodeList.Sort();   
                return nodeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(HttpPostedFileBase File)
        {
            string filePath = string.Empty;
            if (File != null)
            {
                string path = Server.MapPath("~/UploadedFiles/");
                filePath = path + Path.GetFileName(File.FileName);
                string extension = Path.GetExtension(File.FileName);
                File.SaveAs(filePath);
                ViewBag.Message = "Archivo Cargado";

                return RedirectToAction("Index");
            }

            return View();
        }
    }


}




//        public List<HuffmanNode> ObtenerListaNodos()
//        {
//            var buffer = new byte[bufferLength];
//            using (var file = new FileStream(@"C:\hola.txt", FileMode.Open))
//            {
//                using (var reader = new BinaryReader(file))
//                {
//                    while (reader.BaseStream.Position != reader.BaseStream.Length)
//                    {
//                        buffer = reader.ReadBytes(bufferLength);
//                        if (ListaNodos.Exists(x => x.caracter == buffer.ToString()))
//                            ListaNodos[ListaNodos.FindIndex(y => y.caracter == buffer.ToString())].Frecuencia() ;
//                        else
//                            ListaNodos.Add(new HuffmanNode(buffer.ToString()));
//                    }
//                    ListaNodos.Sort();
//                    return ListaNodos;
//                }

//            }
//        }