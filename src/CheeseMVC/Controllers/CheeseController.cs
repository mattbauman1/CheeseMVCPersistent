using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            //List<Cheese> cheeses = context.Cheeses.ToList();
            if (id != 0)
            {
                IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).Where(c => c.CategoryID == id).ToList();
                if (cheeses.Count > 0)
                {
                    ViewBag.Title = "Cheese Category: " + cheeses[0].Category.Name;
                }
                else
                {
                    ViewBag.Title = "No Cheeses in the " +  context.Categories.Where(c => c.ID == id).ToList()[0].Name + " Category!";
                }
                return View(cheeses);
            }
            else
            {
                IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();
                ViewBag.Title = "All Cheeses";
                return View(cheeses);
            }
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        /*[HttpGet]
        public IActionResult Edit(int cheeseId)
        {
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(context.Categories.ToList(), context.Cheeses.Single(c => c.ID == cheeseId));
            return View(editCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory cheeseCategory = context.Categories.Single(c => c.ID == editCheeseViewModel.CategoryID);

                //context.Cheeses.Update(context.Cheeses.Single(c => c.ID == editCheeseViewModel.TheCheese.ID));

                context.Cheeses.Where(c => c.ID == editCheeseViewModel.TheCheese.ID).First().Name = editCheeseViewModel.Name;
                context.Cheeses.Where(c => c.ID == editCheeseViewModel.TheCheese.ID).First().Description = editCheeseViewModel.Description;
                context.Cheeses.Where(c => c.ID == editCheeseViewModel.TheCheese.ID).First().Category = cheeseCategory;
                //context.Cheeses.Update(context.Cheeses.Where(c => c.ID == editCheeseViewModel.TheCheese.ID).First());
                context.Entry(context.Cheeses.Where(c => c.ID == editCheeseViewModel.TheCheese.ID).First()).State = EntityState.Modified;
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(editCheeseViewModel);
        }*/
    }
}
