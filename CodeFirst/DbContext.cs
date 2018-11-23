using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirst
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public MyDbContext() : base("name=TheAuctionDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = true;
        }

        //static DbContext()
        //{
        //    Database.SetInitializer<DbContext>(new IdentityDbInit());
        //}
        
        public static MyDbContext Create()
        {
            return new MyDbContext();
        }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<LotGroup> LotGroups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Check> Checks { get; set; }
        //public DbSet<Trade> Trades { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentOption> ShipmentOptions { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<DeliveringCondition> DeliveringConditions { get; set; }
        //public DbSet<Dispute> Disputes { get; set; }
        //public DbSet<Refund> Refunds { get; set; }
        //public DbSet<Return> Returns { get; set; }
        //public DbSet<LotPicture> LotPictures { get; set; }
        //public DbSet<FigurePicture> FigurePictures { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }

    //public class IdentityDbInit : DropCreateDatabaseIfModelChanges<DbContext>
    //{
    //    protected override void Seed(DbContext context)
    //    {
    //        PerformInitialSetup(context);
    //        base.Seed(context);
    //    }
    //    public void PerformInitialSetup(DbContext context)
    //    {
    //        // настройки конфигурации контекста будут указываться здесь
    //    }
    //}
}
