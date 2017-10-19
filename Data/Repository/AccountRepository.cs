using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class AccountRepository : IAccount
    {
        private Context _db;

        public AccountRepository(Context context)
        {
            _db = context;
        }

        public List<Role> GetRoles() => _db.Roles.ToList();

        // если выборка пуста, FirstOrDefault вернет null.
        public User GetUser(string name) => _db.Users.FirstOrDefault(user => user.Name == name);

        public User CheckUser(string name, string password) => _db.Users.FirstOrDefault(u => u.Name == name && u.Password == password);

        public User Add(string name, string password, int roleId)
        {
            _db.Users.Add(new User { Name = name, Password = password, RoleId = roleId });
            _db.SaveChanges();

            return _db.Users.Where(u => u.Name == name && u.Password == password).FirstOrDefault();
        }
    }
}
