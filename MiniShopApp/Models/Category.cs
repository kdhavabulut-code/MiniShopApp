
using System.Collections.Generic;

namespace MiniShopApp.Models
{

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category ParentCategory { get; set; }

        public List<Category> SubCategories { get; set; } = new List<Category>();
    }

}

