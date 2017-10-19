using Data.Models;
using System.Collections.Generic;

namespace Data.Repository
{
    public interface IAccount
    {
        List<Role> GetRoles();
        User GetUser(string name);
        User CheckUser(string name, string password);
        User Add(string name, string password, int roleId);
    }
}
