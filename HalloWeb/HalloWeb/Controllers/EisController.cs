using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalloWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalloWeb.Controllers
{
    public class EisController : Controller
    {
        DataManager data = new DataManager();

        // GET: Eis
        public ActionResult Index()
        {
            return View(data.GetAll());
        }

        // GET: Eis/Details/5
        public ActionResult Details(int id)
        {
            return View(data.GetById(id));
        }

        // GET: Eis/Create
        public ActionResult Create()
        {
            return View(new Eis() { Name = "NEU", Herstelldatum = DateTime.Now });
        }

        // POST: Eis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Eis eis)
        {
            try
            {
                data.Add(eis);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Eis/Edit/5
        public ActionResult Edit(int id)
        {
            return View(data.GetById(id));
        }

        // POST: Eis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Eis eis)
        {
            try
            {
                data.Update(eis);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Eis/Delete/5
        public ActionResult Delete(int id)
        {
            return View(data.GetById(id));
        }

        // POST: Eis/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Eis eis)
        {
            try
            {
                data.Delete(eis);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}