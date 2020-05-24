using Data;
using Domain.DefinitionObjects;
using MongoDB.Bson;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class UserCrudService
    {
        private readonly UserRepositories repositories = new UserRepositories();

        public List<User> GetAllUsers()
        {
            return repositories.GetAllUsers();
        }

        public void Register(User userDetails)
        {
            repositories.RegisterUser(userDetails);
        }

        public User GetUserById(string id)
        {
            User result = repositories.GetUserById(id);
            return result;
        }

        public User GetUserByPhone(string phone)
        {
            User result = repositories.GetUserByPhone(phone);
            return result;
        }

        public void UpdateUser(User updatedDetails, string id)
        {
            repositories.UpdateUser(updatedDetails, id);
        }

        public void DeleteUser(string id)
        {
            repositories.DeleteUser(id);
        }
    }
}
