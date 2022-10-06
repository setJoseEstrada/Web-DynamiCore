using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        NUsuarios _metodos = new NUsuarios();
        Usuarios _usuarios = new Usuarios();

        public ActionResult Index()
        {
            List<Usuarios> lista = _metodos.ConsultarTodos();
            return View(lista);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            _usuarios = _metodos.Consultaruno(id);
            return View(_usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(Usuarios objusuarios)
        {
            try
            {
                // TODO: Add insert logic here
                _metodos.Agregar(objusuarios);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            _usuarios = _metodos.Consultaruno(id);
            return View(_usuarios);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuarios usuarios)
        {
            try
            {
                // TODO: Add update logic here

                _metodos.Modificar(usuarios);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            _usuarios = _metodos.Consultaruno(id);
            return View(_usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _metodos.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
