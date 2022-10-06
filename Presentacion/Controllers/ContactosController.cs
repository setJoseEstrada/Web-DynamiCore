using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class ContactosController : Controller
    {
        // GET: Contactos

        NContactos _metodos = new NContactos();
        Contactos _contactos = new Contactos();

        public ActionResult Index()
        {
            List<Contactos> listcon = _metodos.ConsultarTodos();
            return View(listcon);
        }

        // GET: Contactos/Details/5
        public ActionResult Details(int id)
        {
            _contactos = _metodos.Consultaruno(id);
            return View(_contactos);
        }

        // GET: Contactos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contactos/Create
        [HttpPost]
        public ActionResult Create(Contactos objcon)
        {
            try
            {
                // TODO: Add insert logic here
                _metodos.Agregar(objcon);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(int id)
        {
            _contactos = _metodos.Consultaruno(id);
            return View(_contactos);
        }

        // POST: Contactos/Edit/5
        [HttpPost]
        public ActionResult Edit(Contactos contacto)
        {
            try
            {
                // TODO: Add update logic here
                _metodos.Modificar(contacto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contactos/Delete/5
        public ActionResult Delete(int id)
        {
            _contactos=_metodos.Consultaruno(id);
            return View(_contactos);
        }

        // POST: Contactos/Delete/5
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
