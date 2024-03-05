using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebToDoApp.Models;
using WebToDoApp.Services;

namespace WebToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoItemService _toDoItemService;

        public HomeController(ToDoItemService service)
        {
            _toDoItemService = service;
        }

        public IActionResult Index() 
        {
            var todoList = _toDoItemService.GetAllToDoItems();
            return View(todoList);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var todoItem = _toDoItemService.GetToDoItemById(id);

            if(todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        [HttpPost]
        public IActionResult AddItem(ToDoItem newItem)
        {
            if(ModelState.IsValid)
            {
                _toDoItemService.AddToDoItem(newItem);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult UpdateItem(ToDoItem updatingItem)
        {
            if (ModelState.IsValid)
            {
                _toDoItemService.UpdateToDoItem(updatingItem);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult DeleteItem(Guid id)
        {
            _toDoItemService.DeleteToDoItem(id);
            return RedirectToAction("Index");
        }


    }
}
