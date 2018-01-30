using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using TrainingDotNetCoreMVC.Models;
using TrainingDotNetCoreMVC.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainingDotNetCoreMVC.Controllers
{
    public class MovieController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var movieRepo = new MovieRepository();
            var items = movieRepo.getMovieList();
            ViewBag.movieList = items;
            return View();
        }
    }
}
