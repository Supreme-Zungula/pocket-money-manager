using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Models;
using Domain.DefinitionObjects;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserCrudService userService = new UserCrudService();
        private static readonly List<UserModel> user = new List<UserModel> { new UserModel { FirstName = "Test", LastName = "test", Role = "man" } };
        private readonly SetupDB set = new SetupDB();
        private readonly UserModel userModel = new UserModel();

        [Obsolete]
        public ActionResult SetUp()
        {
            set.Setup();
            return Ok("SetUp Successfully");
        }

        [HttpGet]
        [Route("allUsers")]
        public ActionResult<List<UserModel>> GetAll()
        {
            var Users = userService.GetAllUsers();
            var userModel = Users.Select(person => UserModel.FromDomain(person));

            return Ok(userModel.ToList());
        }

        [HttpGet]
        [Route("getbyphone/{Phone}")]
        public ActionResult<List<UserModel>> GetUserByPhone(string phone)
        {
            
            UserModel existingUser = UserModel.FromDomain(userService.GetUserByPhone(phone));
            //var userModel = Users.Select(person => UserModel.FromDomain(person));

            return Ok(existingUser);
        }

        [HttpPost]
        public ActionResult Post(UserModel userDetails)
        {
            userDetails.FamilyId = ObjectId.GenerateNewId().Increment;
            userService.Register(userDetails.ToDomain());
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(userDetails.FirstName));
            return Created(resourceUrl, userDetails);
        }

        [HttpPut]
        public ActionResult Put(UserModel userDetails)
        {
            //this is for testing update
            /*var lis = userService.getl2();
            foreach (var l in lis)
            {
                userDetails.Id = l.Id;
            }*/
            //the object id will come from the frontend code behind

            UserModel existingUser = UserModel.FromDomain(userService.GetUserById(userDetails.Id));
            
            //also for testing
            //userDetails.FamilyId = existingUser.FamilyId;

            if (existingUser == null)
            {
                return BadRequest("User not found!");
            }
            else
            {
                userService.UpdateUser(userDetails.ToDomain(), userDetails.Id);
                return Ok();
            }
        }
        
        [HttpDelete]
        [Route("{Phone}")]
        public ActionResult Delete(string phone)
        {
            UserModel existingUser = UserModel.FromDomain(userService.GetUserByPhone(phone));
            var id = existingUser.Id;

            if (existingUser == null)
            {
                return NotFound();
            }
            else
            {
                userService.DeleteUser(id);
                return NoContent();
            }
        }
    }
}