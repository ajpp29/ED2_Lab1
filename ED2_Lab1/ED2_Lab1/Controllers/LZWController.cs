using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ED2_Lab1.Models;
using System.Web.Mvc;

namespace ED2_Lab1.Controllers
{
    public class LZWController : Controller
    {
        LZW algoritmo = new LZW();
        public ActionResult Index()
        {
            List<int> compreso = algoritmo.Compresion("HOLACOMOESTASMAJE");
            string descompreso = algoritmo.Descompresion(compreso);
            return View();
        }
    }
}