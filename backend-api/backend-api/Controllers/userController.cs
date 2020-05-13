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

        [Obsolete]
        public ActionResult<List<UserModel>> Get()
        {
            //set.Setup();
            User use = new User { FirstName = "tess", LastName = "dfdfadf", Password = "ddfdfdf", Phone = "000099888", Role = "dhdhd" };
            //userService.Register(use);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post(UserModel userDetails)
        {
            userDetails.FamilyId = ObjectId.GenerateNewId().Increment;
            userService.Register(userDetails.ToDomain());
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(userDetails.FirstName));
            return Created(resourceUrl, userDetails);
        }
    }
}