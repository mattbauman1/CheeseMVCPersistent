using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class EditCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        //public CheeseType Type { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        //public List<SelectListItem> CheeseTypes { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public Cheese TheCheese { get; set; }

        public EditCheeseViewModel() { }

        public EditCheeseViewModel(IEnumerable<CheeseCategory> categories, Cheese theCheese)
        {

            Categories = new List<SelectListItem>();

            TheCheese = theCheese;

            Name = TheCheese.Name;

            Description = TheCheese.Description;

            CategoryID = TheCheese.CategoryID;

            foreach (CheeseCategory category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = category.ID.ToString(),
                    Text = category.Name
                });
            }
        }
    }
}
