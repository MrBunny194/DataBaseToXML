using Data.Models;
using System.Data.Entity;

namespace Data
{
    public class Context : DbContext
    {
        public Context() : base("Context")
        { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            db.Years.Add(new Year { Date = 2017 });

            db.Cars.Add(new Car { Name = "FORD FIESTA", Model = "FORD" });
            db.Cars.Add(new Car { Name = "FORD FOCUS", Model = "FORD" });
            db.Cars.Add(new Car { Name = "FORD MONDEO", Model = "FORD" });


            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "user" });

            db.Users.Add(new User { Name = "admin", Password = "admin", RoleId = 1 });
            db.Users.Add(new User { Name = "user", Password = "user", RoleId = 2 });

            base.Seed(db);
        }
    }
}
