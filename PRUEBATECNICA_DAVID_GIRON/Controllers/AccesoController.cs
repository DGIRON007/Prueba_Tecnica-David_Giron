using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using PRUEBATECNICA_DAVID_GIRON.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;

namespace PRUEBATECNICA_DAVID_GIRON.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=localhost;Initial Catalog=DB_ACCESO;Integrated Security=true";


        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario oUsario)
        {
            bool registrado;
            string mensaje;

            if(oUsario.Clave == oUsario.ConfirmarClave)
            {
                oUsario.Clave = (oUsario.Clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no son iguales";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsario.Clave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();





            }
            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }


        }
        [HttpPost]
        public ActionResult Login(Usuario oUsario)
        {
            oUsario.Clave = (oUsario.Clave);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsario.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsario.IdUsuario= Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if(oUsario.IdUsuario != 0)
            {
                Session["usuario"] = oUsario;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewData["Mensaje"] = "No se encontro el usuario";
                return View();
            }
            
        }



  

    }
}