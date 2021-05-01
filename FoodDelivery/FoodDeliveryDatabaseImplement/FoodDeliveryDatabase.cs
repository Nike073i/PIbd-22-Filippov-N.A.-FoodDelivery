using FoodDeliveryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDatabaseImplement
{
    public class FoodDeliveryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FoodDeliveryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Dish> Dishes { set; get; }
        public virtual DbSet<Set> Sets { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
        public virtual DbSet<SetDish> SetDishes { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Store> Stores { set; get; }
        public virtual DbSet<StoreDish> StoreDishes { set; get; }
    }
}
