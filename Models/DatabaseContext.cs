using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KeysOnboarding2.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=KeysEntities")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProductSold> ProductSold { get; set; }
    }
}