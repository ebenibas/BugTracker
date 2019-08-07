using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    public class UserManagement
    {

        ApplicationDbContext db;
            UserManager<ApplicationUser> usersManager;
            public UserManagement(ApplicationDbContext db)
            {
                this.db = db;
                usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            }
        public bool IsUserInRole(string userId, string roleName)
        {
            return usersManager.IsInRole(userId, roleName);
        }
        public ICollection<string>GetUserRoles(string userId)
        {
            return usersManager.GetRoles(userId);
        }
        public bool AddUserToRole(string userId, string roleName)
        {
            var result = usersManager.AddToRole(userId, roleName);
                return result.Succeeded;
        }
        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = usersManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }
        public ICollection<ApplicationUser>UsersInRole(string roleName)
        {
            var result = new List<ApplicationUser>();
            var allUsers = db.Users;
            foreach(var user in allUsers)
            {
                //another way to solve the linq
                //usersManager.GetRoles(user.Id).Any(r => r == roleName);
                if (IsUserInRole(user.Id, roleName))
                {
                    result.Add(user);
                }
                //another way
                //return db.Users.Where(u => IsUserInRole(u.Id, roleName)).ToList();
           
            }
            return result;
        }
        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var result = new List<ApplicationUser>();
            var allUsers = db.Users;
            foreach (var user in allUsers)
            {
                //another way to solve the linq
                //usersManager.GetRoles(user.Id).Any(r => r == roleName);
                if (IsUserInRole(user.Id, roleName))
                {
                    result.Add(user);
                }
                //another way
                //return db.Users.Where(u => IsUserInRole(u.Id, roleName)).ToList();
                return result;
            }
        }
    }
    
}