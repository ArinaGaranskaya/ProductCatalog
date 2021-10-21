using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductCatalog.Models
{

    public class Categorys {
        public int id { get; set; }
        public string Category { get; set; }

        public List<Products> Product { get; set; }
    }

    public class Products {
        public int id { get; set; }
        public string product { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string note { get; set; }
        public string spnote { get; set; }
        public int categorysid { get; set; }
        public Categorys Categorys { get; set; }
    }


    public enum roles { 
    User,
    Admin,
    Superadmin
    }

    public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } 
            public string Pass { get; set; }
            public int role { get; set; }        }
}
