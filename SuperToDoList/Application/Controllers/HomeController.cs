using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Models;
using Repository.Interfaces;
using Entities;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public HomeController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public IActionResult Index()
        {
            return View(_todoRepository.GetAll());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Todo todo)
        {
            _todoRepository.Add(todo);
            return View("Index", _todoRepository.GetAll());
        }

        public IActionResult Edit(int id)
        {            
            return View(_todoRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
            _todoRepository.Edit(todo);
            return View("Index", _todoRepository.GetAll());
        }

        public IActionResult Remove(int id)
        {
            return View(_todoRepository.Get(id));
        }

        public IActionResult RemoveConfirm(int id)
        {
            _todoRepository.Remove(id);
            return RedirectToAction("Index");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
