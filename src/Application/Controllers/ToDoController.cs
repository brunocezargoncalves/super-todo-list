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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading;

namespace Application.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_toDoRepository.GetAll());
        }
        
        [Authorize]        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]        
        public IActionResult Add(ToDo toDo)
        {    
            var userId = this.User.Identity.GetUserId();

            if(userId == null) {                
                ModelState.AddModelError("Erro", "Usuário não encontrado");
            }

            if(ModelState.IsValid) 
            {                
                var forecast = DateTime.MinValue;
                DateTime.TryParse(toDo.Forecast.ToString(), out forecast);                

                _toDoRepository.Add(new ToDo {
                    Id = Guid.NewGuid(),
                    UserId = (Guid)userId,
                    Start = DateTime.UtcNow,
                    Task = toDo.Task,
                    Description = toDo.Description,
                    Forecast =  forecast != DateTime.MinValue ? (DateTime?)forecast : null,
                    Finish = null
                });

                Notification.Set(TempData, new Message() {
                    Text = "Tarefa cadastrada com sucesso!",
                    Type = NotificationType.success
                });

                return View("Index", _toDoRepository.GetAll());
            }
            else 
            {
                Notification.Set(TempData, new Message() {
                    Text = "Não foi possível adicionar essa tarefa!",
                    Type = NotificationType.danger
                });

                return View();
            }
        }

        [Authorize]        
        public IActionResult Update(Guid id)
        {            
            return View(_toDoRepository.GetById(id));
        }

        [HttpPost]
        [Authorize]        
        public IActionResult Update(ToDo toDo)
        {
            _toDoRepository.Update(toDo);
            return View("Index", _toDoRepository.GetAll());
        }
    
        [Authorize]
        public IActionResult Remove(Guid id)
        {
            return View(_toDoRepository.GetById(id));
        }

        [Authorize]
        public IActionResult RemoveConfirm(Guid id)
        {
            _toDoRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
