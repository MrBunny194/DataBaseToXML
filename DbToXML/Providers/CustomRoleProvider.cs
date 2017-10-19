using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;

using Data.Models;
using Data;

namespace DbToXML.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string email)
        {
            var role = new string[] { };

            using (var db = new Context())
            {
                // Получаем пользователя
                var user = db.Users.FirstOrDefault(u => u.Name == email);

                if (user != null)
                {
                    // получаем роль
                    var userRole = db.Roles.Find(user.RoleId);

                    if (userRole != null)
                    {
                        role = new string[] { userRole.Name };
                    }
                }
            }
            return role;
        }


        public override void CreateRole(string roleName)
        {
           var newRole = new Role() { Name = roleName };
           var db = new Context();
           db.Roles.Add(newRole);
           db.SaveChanges();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;

            // Находим пользователя
            using (Context db = new Context())
            {
                // Получаем пользователя
                var user = db.Users.FirstOrDefault(u => u.Name == username);

                if (user != null)
                {
                    // получаем роль
                    var userRole = db.Roles.Find(user.RoleId);
                    
                    //сравниваем
                    if (userRole != null && userRole.Name == roleName)
                    {
                        outputResult = true;
                    }
                }
            }
            return outputResult;
        }

        #region создано автоматически 
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}