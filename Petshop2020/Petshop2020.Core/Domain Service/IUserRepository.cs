using Petshop2020.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();

        public User GetUser(long id);

        public void AddUser(User entity);

        public void EditUser(User entity);

        public void RemoveUser(long id);


    }
}
