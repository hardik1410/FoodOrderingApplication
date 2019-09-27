namespace FoodOrder.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FoodOrderModel1 : DbContext
    {
        // Your context has been configured to use a 'FoodOrderModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FoodOrder.Models.FoodOrderModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FoodOrderModel' 
        // connection string in the application configuration file.
        public FoodOrderModel1()
            : base("name=FoodOrderModel1")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<FoodItems> FoodItems { get; set; }
        public DbSet<FCategory> FCategory { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<ResCat> ResCat { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}