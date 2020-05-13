using Domain.DefinitionObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    interface IUserOperations
    {
        void RegisterUser(User userDetails);
        void UserLogin(User userDetails);
        void UpdateUser(User useDetails);
        void DeleteUser(User useDetails);
    }
}
