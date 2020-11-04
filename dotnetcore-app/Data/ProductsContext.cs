﻿using Microsoft.EntityFrameworkCore;

namespace MvcApp.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext (DbContextOptions<ProductsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<MvcApp.Models.Product> Product { get; set; }
    }
}
