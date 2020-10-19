using Microsoft.EntityFrameworkCore;
using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Infrastructure.SQLite.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly PetshopContext _ctx;

        public UserRepository(PetshopContext ctx)
        {
            _ctx = ctx;
        }

        public void AddUser(User entity)
        {
            _ctx.Users.Add(entity);
            _ctx.SaveChanges();
        }

        public void EditUser(User entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return  _ctx.Users.ToList();
        }

        public User GetUser(long id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
        }

        public void RemoveUser(long id)
        {
            _ctx.Remove(GetUser(id));
            _ctx.SaveChanges();
        }
    }
}
