using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class DirectoriosController : Controller
    {
        // GET: Directorios

        Directorio _Directorio = new Directorio();
        NDirectorio _Metodos =new  NDirectorio();
        NUsuarios _nUsuarios = new NUsuarios();
        NContactos _nContactos = new NContactos();
        public ActionResult Index()
        {
          
            string token = (string)Session["token"];
            List<Directorio> directorios = _Metodos.ConsultarTodos(token);
            return View(directorios);
        }

        // GET: Directorios/Details/5
        public ActionResult Details(int id)
        {
            _Directorio = _Metodos.Consultaruno(id);
            return View(_Directorio);
        }

        // GET: Directorios/Create
        public ActionResult Create()
        {

    

            
            List<Contactos> _listcontactos = _nContactos.ConsultarTodos();

            ViewBag.ListU = _nUsuarios.ConsultarTodos();
            ViewBag.ListC = _listcontactos;


            return View();
        }

        // POST: Directorios/Create
        [HttpPost]
        public ActionResult Create(Directorio objDirectorio)
        {
            try
            {
                // TODO: Add insert logic here



                _Metodos.Agregar(objDirectorio);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Directorios/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListU = _nUsuarios.ConsultarTodos();
            ViewBag.ListC = _nContactos.ConsultarTodos();
            Directorio direc=_Metodos.Consultaruno(id);
            return View(direc);
        }

        // POST: Directorios/Edit/5
        [HttpPost]
        public ActionResult Edit(Directorio objdirec)
        {
            try
            {

                ViewBag.ListU = _nUsuarios.ConsultarTodos();
                ViewBag.ListC = _nContactos.ConsultarTodos();
                // TODO: Add update logic here

                _Metodos.Modificar(objdirec);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Directorios/Delete/5
        public ActionResult Delete(int id)
        {
            Directorio directorio = _Metodos.Consultaruno(id);
            return View(directorio);
        }

        // POST: Directorios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _Metodos.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
