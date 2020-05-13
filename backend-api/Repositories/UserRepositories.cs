using Data;
using Domain.DefinitionObjects;
using MongoDB.Bson;
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

        //for testing update
        public static List<User> list2;

        public UserRepositories()
        {
            Users = database.GetUserCollection();
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
            list2 = list;
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

        public void DeleteUser(ObjectId id)
        {
            var filter = Builders<UserData>.Filter.Eq("_id", id);
            Users.DeleteOne(filter);
        }

        public void RegisterUser(User userDetails)
        {
            //add a method to check if a user doesn't already exist
            Users.InsertOne(FromDomain(userDetails));
        }

        public void UpdateUser(User userDetails, ObjectId id)
        {
            var filter = Builders<UserData>.Filter.Eq("_id", id);
            Users.ReplaceOne(filter, FromDomain(userDetails));
        }

        public void UserLogin(User userDetails)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(ObjectId id)
        {
            var filter = Builders<UserData>.Filter.Eq("_id", id);
            User result = null;
            foreach (UserData user in Users.Find(filter).ToListAsync().Result)
            {
                result = ToDomain(user);
            }
            return result;
        }

        public User GetUserByPhone(string phone)
        {
            var filter = Builders<UserData>.Filter.Eq("Phone", phone);
            User result = null;
            foreach (UserData user in Users.Find(filter).ToListAsync().Result)
            {
                result = ToDomain(user);
            }
            return result;
        }
    }
}
