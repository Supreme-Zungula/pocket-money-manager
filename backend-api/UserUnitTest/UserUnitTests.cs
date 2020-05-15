using Domain.DefinitionObjects;
using Domain.Services;
using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;

namespace UserUnitTest
{
    public class UserUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void GetAllUsers()
        {
            UserCrudService _userService = new UserCrudService();
            List<User> _usersList = _userService.GetAllUsers();
            Assert.IsNotNull(_usersList);
        }
        
        [Test, Order(2)]
        public void RegisterUser()
        {
            UserCrudService _userService = new UserCrudService();

            User _user = new User { FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Lorem", LastName = "Ipsum", Role = "Mother", Password = "tobehashed", Phone = "0123456788" };
            _userService.Register(_user);

            User _getCreatedUser = _userService.GetUserByPhone("0123456788");
            Assert.AreEqual(_user.Phone, _getCreatedUser.Phone);
        }

        [Test, Order(3)]
        public void GetUserByPhone()
        {
            UserCrudService _userService = new UserCrudService();
            User user = _userService.GetUserByPhone("0123456788");
            Assert.IsNotNull(user);
        }

        [Test, Order(4)]
        public void GetUserByPhone_FailCase()
        {
            UserCrudService _userService = new UserCrudService();
            User user = _userService.GetUserByPhone("00123456788");
            Assert.IsNull(user);
        }

        [Test, Order(5)]
        public void GetUserById()
        {
            UserCrudService _userService = new UserCrudService();
            User user = _userService.GetUserByPhone("0123456788");
            User idResult = _userService.GetUserById(user.Id);
            Assert.IsNotNull(idResult);
        }
        
        [Test, Order(6)]
        public void UpdateUser()
        {
            UserCrudService _userService = new UserCrudService();
            User user = _userService.GetUserByPhone("0123456788");
            User updateInfo = new User { Id=user.Id, FamilyId = user.FamilyId, FirstName = "Foo", LastName = "Ipsum", Role = "Mother", Password = "tobehashed", Phone = "0123456788" };
            _userService.UpdateUser(updateInfo, user.Id);

            User getUpdatedUser = _userService.GetUserById(user.Id);
            Assert.AreNotEqual(user.FirstName, getUpdatedUser.FirstName);
        }

        [Test, Order(7)]
        public void DeleteUser()
        {
            UserCrudService _userService = new UserCrudService();
            User user = _userService.GetUserByPhone("0123456788");
            _userService.DeleteUser(user.Id);

            User deletedUser = _userService.GetUserByPhone("0123456788");
            Assert.IsNull(deletedUser);
        }

    }
}