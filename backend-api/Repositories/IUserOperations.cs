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
        void UpdateUser(User userDetails, string id);
        void DeleteUser(string id);
        User GetUserById(string id);
        User GetUserByPhone(string phone);
    }
}
