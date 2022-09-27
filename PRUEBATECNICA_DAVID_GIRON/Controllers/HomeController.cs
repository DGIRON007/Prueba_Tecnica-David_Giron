using PRUEBATECNICA_DAVID_GIRON.Models;
using PRUEBATECNICA_DAVID_GIRON.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRUEBATECNICA_DAVID_GIRON.Permisos;

namespace PRUEBATECNICA_DAVID_GIRON.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Formulario David Giron.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Mi contacto personal.";

            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}