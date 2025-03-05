using System.Numerics;
using inventory_re.Model;
using inventory_re.Models;
using Microsoft.EntityFrameworkCore;

namespace inventory_re.DAO
{

    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options)
              : base(options)
        {

        }
      

        public DbSet<PurchaseMaster> PurchaseMaster { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Tax> Tax{ get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<InventoryItems> InventoryItems { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<SalesDetail> SalesDetail { get; set; }
        public DbSet<SalesMaster> SalesMaster { get; set; }



    }
}
