using Data;
using Domain.DefinitionObjects;
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
    }
}
