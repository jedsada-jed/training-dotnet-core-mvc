using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingDotNetCoreMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainingDotNetCoreMVC.Controllers
{
    public class MovieController : Controller
    {
        MyProjectContext db = new MyProjectContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = db.Movie.ToList();
            return View(model);
        }

        public string Welcome(string name, string id)
        {
            return $"Hello, Your name's {name}. Your ID is {id}.";
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieModel model, IFormFile fileUpload)
        {
            if (model.duration < 1)
            {
                ModelState.AddModelError("errDuration", "The duration field is required.");
                return View();
            }

            if (fileUpload == null)
            {
                ModelState.AddModelError("errFileUpload", "The file upload field is required.");
                return View();
            }

            if (ModelState.IsValid)
            {
                string pathImgMovie = "/images/movie/";
                string pathSave = $"wwwroot{pathImgMovie}";
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string extFile = Path.GetExtension(fileUpload.FileName);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }

                DateTime dateNow = DateTime.Now;
                model.coverImg = pathImgMovie + fileName;
                model.createDate = dateNow;
                model.modifyDate = dateNow;
                db.Movie.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            MovieModel movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            MovieModel movie = db.Movie.Find(id);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieModel model, IFormFile fileUpload)
        {
            if (model.duration < 1)
            {
                ModelState.AddModelError("errDuration", "The duration field is required.");
                return View();
            }

            // set old data
            db.Movie.Attach(model);
            MovieModel oldMovie = new MyProjectContext().Movie.Find(model.id);
            model.coverImg = oldMovie.coverImg;
            model.createDate = oldMovie.createDate;
            oldMovie = null;

            if (ModelState.IsValid)
            {
                if (fileUpload != null) 
                {
                    string pathImgMovie = "/images/movie/";
                    string pathSave = $"wwwroot{pathImgMovie}";
                    if (!Directory.Exists(pathSave))
                    {
                        Directory.CreateDirectory(pathSave);
                    }
                    string extFile = Path.GetExtension(fileUpload.FileName);
                    string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(stream);
                    }
                    model.coverImg = pathImgMovie + fileName;
                }

                model.modifyDate = DateTime.Now;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
