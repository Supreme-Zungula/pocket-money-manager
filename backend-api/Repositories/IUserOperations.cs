using Data;
using Domain.DefinitionObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    interface IUserOperations
    {
        void RegisterUser(User userDetails);
        void UserLogin(User userDetails);
        void UpdateUser(User userDetails, ObjectId id);
        void DeleteUser(ObjectId id);
        User GetUserById(ObjectId id);
        User GetUserByPhone(string phone);
    }
}
