using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public CheeseType Type { get; set; }
        public CheeseCategory Category { get; set; }
        //CategoryID corresponds to Category
        public int CategoryID { get; set; }
        public int ID { get; set; }
        public IList<CheeseMenu> CheeseMenus { get; set; }
    }
}
