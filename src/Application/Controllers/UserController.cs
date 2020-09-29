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

namespace Application.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_userRepository.GetAll());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(User user)
        {           
            if(ModelState.IsValid) 
            {
                _userRepository.Add(new User {
                    Registration = DateTime.UtcNow,
                    Name = user.Name,
                    Email = user.Email,
                    VerifiedEmail = false,
                    Password = Common.ComputeSha256Hash(user.Password),
                    Role = "User",
                    ChangePassword = false
                });

                Notification.Set(TempData, new Message() {
                    Text = "Tarefa cadastrada com sucesso!",
                    Type = NotificationType.success
                });

                return View("Index", _userRepository.GetAll());
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

        [Authorize(Roles = "Admin")]
        public IActionResult Update(Guid id)
        {            
            return View(_userRepository.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]        
        public IActionResult Update(User user)
        {
            _userRepository.Update(user);
            return View("Index", _userRepository.GetAll());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Remove(Guid id)
        {
            return View(_userRepository.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveConfirm(Guid id)
        {
            _userRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
