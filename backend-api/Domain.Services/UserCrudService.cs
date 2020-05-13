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
        public void Register(User userDetails)
        {
            repositories.RegisterUser(userDetails);
        }

        public User GetUserById(ObjectId id)
        {
            User result = repositories.GetUserById(id);
            return result;
        }

        public User GetUserByPhone(string phone)
        {
            User result = repositories.GetUserByPhone(phone);
            return result;
        }

        public void UpdateUser(User updatedDetails, ObjectId id)
        {
            repositories.UpdateUser(updatedDetails, id);
        }

        public void DeleteUser(ObjectId id)
        {
            repositories.DeleteUser(id);
        }

        //for testing update
        public List<User> getl2()
        {
            var l = UserRepositories.list2;
            return l;
        }
    }
}
