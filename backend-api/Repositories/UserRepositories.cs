using Data;
using Domain.DefinitionObjects;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class UserRepositories : IUserOperations
    {
        private GetDB database = new GetDB();
        private IMongoCollection<UserData> Users;
        public List<User> list;

        public UserRepositories()
        {
            Users = database.Connect();
            var filter = Builders<UserData>.Filter.Empty;
            
            foreach (UserData user in Users.Find(filter).ToListAsync().Result)
            {
                if (list == null)
                    list = new List<User>()
                    {
                        ToDomain(user)
                    };
                else
                    list.Add(ToDomain(user));
            }

            Console.WriteLine(list);
        }


        public User ToDomain(UserData userData)
        {
            return new User
            {
                Id = userData.Id,
                FamilyId = userData.FamilyId,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Role = userData.Role,
                Phone = userData.Phone,
                Password = userData.Password
            };
        }

        public UserData FromDomain(User userData)
        {
            return new UserData
            {
                Id = userData.Id,
                FamilyId = userData.FamilyId,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Role = userData.Role,
                Phone = userData.Phone,
                Password = userData.Password
            };
        }

        public void DeleteUser(User useDetails)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(User userDetails)
        {
            //add a method to check if a user doesn't already exist
            Users.InsertOne(FromDomain(userDetails));
        }

        public void UpdateUser(User useDetails)
        {
            throw new NotImplementedException();
        }

        public void UserLogin(User userDetails)
        {
            throw new NotImplementedException();
        }
    }
}
