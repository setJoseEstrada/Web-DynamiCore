using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        NLogin _metodos = new NLogin();
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string correo = Convert.ToString(form["correo"]);
            string contrasena = Convert.ToString(form["contrasena"]);
            Login login = new Login();
           
            login.contrasena = contrasena;
            login.correo = correo;

            Respuesta respuesta = _metodos.Login(login);    

            if(respuesta.Exito==1)
            {
                Session["token"] = respuesta.Data.token;
                Session["data"] = respuesta.Data;

                return RedirectToAction("Index","Directorios");
            }
            else
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta";
            }
            return RedirectToAction("Index", "Login");
        }

    }
}