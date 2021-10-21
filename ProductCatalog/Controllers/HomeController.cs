using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc.Rendering;


using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;



namespace ProductCatalog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public ActionResult ViewProducts(int? categoryid, string prodname)
        {
            List<Categorys> cat = db.SprCat.ToList();
            IQueryable<Products> prod = db.SprProd.Include(u => u.Categorys);
            if (categoryid != null && categoryid != 0) {
                prod = prod.Where(u => u.Categorys.id == categoryid);
            }
            if (!String.IsNullOrEmpty(prodname)) {
                prod = prod.Where(u => u.product.Contains(prodname));

            }
            SearchM mod = new SearchM
            {
                Categor = new SelectList(cat, "id", "Category"),
                Product = prod.ToList()
            };

            return View(mod);
        }

        public async Task<IActionResult> ViewCategory() {
            return View(await db.SprCat.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.SprProd.Include(u => u.Categorys).ToListAsync());
        }

        public IActionResult CreateCat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCat(Categorys cat)
        {
            db.SprCat.Add(cat);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewCategory");
        }

        public async Task<IActionResult> EditCat(int? id)
        {
            if (id != null)
            {
                Categorys cat = await db.SprCat.FirstOrDefaultAsync(p => p.id == id);
                if (cat != null)
                    return View(cat);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditCat(Categorys cat)
        {
            db.SprCat.Update(cat);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewCategory");
        }




        [HttpGet]
        [ActionName("DeleteCat")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Categorys cat = await db.SprCat.FirstOrDefaultAsync(p => p.id == id);
                if (cat != null)
                    return View(cat);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCat(int? id)
        {
            if (id != null)
            {
                Categorys cat = await db.SprCat.FirstOrDefaultAsync(p => p.id == id);
                if (cat != null)
                {
                    db.SprCat.Remove(cat);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ViewCategory");
                }
            }
            return NotFound();
        }


        [HttpGet]
        [ActionName("DeleteProd")]
        public async Task<IActionResult> ConfirmDeleteP(int? id)
        {
            if (id != null)
            {
                Products prod = await db.SprProd.FirstOrDefaultAsync(p => p.id == id);
                if (prod != null)
                    return View(prod);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProd(int? id)
        {
            if (id != null)
            {
                Products prod = await db.SprProd.FirstOrDefaultAsync(p => p.id == id);
                if (prod != null)
                {
                    db.SprProd.Remove(prod);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ViewProducts");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> EditProd(int? id)
        {
            List<Categorys> cat = db.SprCat.ToList();

            if (id != null)
            {
                Products prod = await db.SprProd.FirstOrDefaultAsync(p => p.id == id);
                if (prod != null)
                {
                    AddOREdit mod1 = new AddOREdit
                    {
                        Categor = new SelectList(cat, "id", "Category"),
                        Product = prod
                    };
                    return View(mod1);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditProd(AddOREdit mod)
        {
            db.SprProd.Update(mod.Product);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewProducts");
        }



        public IActionResult AddProd()
        {
            List<Categorys> cat = db.SprCat.ToList();
            AddOREdit mod1 = new AddOREdit
            {
                Categor = new SelectList(cat, "id", "Category"),
                Product = new Products()
            };
            return View(mod1);
        }

        [HttpPost]
        public async Task<IActionResult> AddProd(AddOREdit mod)
        {
            db.SprProd.Add(mod.Product);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewProducts");
        }



        public async Task<IActionResult> ViewDetails(int? id)
        {
            if (id != null)
            {
                Products prod = await db.SprProd.FirstOrDefaultAsync(p => p.id == id);
                if (prod != null) { return View(prod); } 
            }
            return NotFound();

        }

    }





}
