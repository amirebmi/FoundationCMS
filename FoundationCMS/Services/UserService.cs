using FoundationCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public interface IUserService
    {
        List<User> GetUsers();

        User GetUser(int id);

        void AddUser(User u);

        void MakeAdmin(int id);

        void DeleteUser(User u);

        void SaveChanges();
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _db.Users.Find(id);
        }

        public void AddUser(User u)
        {
            _db.Users.Add(u);
            _db.SaveChanges();
        }

        public void MakeAdmin(int id)   // Is it a correct way to make a user as Admin?
        {
            User foundUser = _db.Users.Find(id);
            foundUser.IsAdmin = true;
            _db.SaveChanges();
        }

        public void DeleteUser(User u)
        {
            _db.Users.Remove(u);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
