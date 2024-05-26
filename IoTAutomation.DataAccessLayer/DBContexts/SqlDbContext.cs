using IoTAutomation.EntityLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IoTAutomation.DataAccessLayer.DBContexts
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<SmartLightApp> SmartLightApps { get; set; }
        public DbSet<PreOrder> PreOrders { get; set; }
        public DbSet<PreOrderDetail> PreOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("IoTAutomation.EntityLayer"));
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=IoTAutomation;Trusted_Connection=True; Trust Server Certificate=true; MultipleActiveResultSets=True");
        }

        // Soft delete olarak calistigimiz veritabanimizda delete islemi gerceklestiginde changetracker kayıt olan deleted islemlerini
        // savechange metodunu ezerek durumun bir delete degil update oldugunu belirtip update olacak propları tanimliyoruz.
        // artik butun delete islemleri bir update olarak algilanacak ve sadece isdelete ile updatedate fieldleri degisecek. 
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            var changes = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();

            foreach (var item in changes)
            {
                item.State = EntityState.Modified;
                BaseEntity<int> baseEntity = item.Entity as BaseEntity<int>;
                baseEntity.IsDelete = true;
                baseEntity.UpdateDate = DateTime.UtcNow.AddHours(3);
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
