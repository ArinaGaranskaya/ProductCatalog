using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductCatalog.Models
{
    public class SearchM
    {
        public IEnumerable<Products> Product { get; set; }
        public SelectList Categor { get; set; }
    }

    public class AddOREdit
    {
        public Products Product { get; set; }
        public SelectList Categor { get; set; }
    }




}
