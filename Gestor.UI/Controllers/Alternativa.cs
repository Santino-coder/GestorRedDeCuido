﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor.UI.Controllers
{
    public class Alternativa : Controller
    {
        // GET: Alternativa
        public ActionResult Index()
        {
            return View();
        }

        // GET: Alternativa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alternativa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alternativa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Alternativa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Alternativa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Alternativa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alternativa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
