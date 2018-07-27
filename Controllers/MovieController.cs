using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingDotNetCoreMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainingDotNetCoreMVC.Controllers
{
    public class MovieController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var db = new MyProjectContext();
            var movie = db.Movie.ToList();
            return View(movie);
        }

        public string Welcome(string name, string id)
        {
            return $"Hello, Your name's {name}. Your ID is {id}.";
        }
    }
}
