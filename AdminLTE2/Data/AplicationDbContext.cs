using AdminLTE2.Models;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;
using System.Drawing;

namespace AdminLTE2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Customers> customers { get; set; }
        //public object Customers { get; internal set; }
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<Countries> countries { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<Inventories> inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventories>().HasKey(x => new { x.product_id, x.warehouse_id });
            modelBuilder.Entity<Order_Items>().HasKey(x => new { x.order_id, x.item_id });
        }
        public DbSet<Locations> locations { get; set; }
        public DbSet<Order_Items> order_items { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Product_Categories> product_categories { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Regions> regions { get; set; }
        public DbSet<Warehouses> warehouses { get; set; }

        /*public DbSet<SqlProductosCategoria> sqlproductoscategoria { get; set; }*/

        //[DbFunction(Schema = "dbo")]
        //public static int fn_PorductCategory_count(int pCategoryId)
        //{
        //    throw new Exception();
        //}

    }
}
