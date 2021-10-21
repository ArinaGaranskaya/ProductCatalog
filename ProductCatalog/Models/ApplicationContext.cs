using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ProductCatalog.Models
{
    
    public class ApplicationContext : DbContext
    {
  //      public DbSet<User> Users { get; set; }


        public DbSet<Categorys> SprCat { get; set; }
        public DbSet<Products> SprProd { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
