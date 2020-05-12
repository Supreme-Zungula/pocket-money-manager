using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly List<UserModel> user = new List<UserModel> { new UserModel { FirstName = "Test", LastName = "test", Role = "man" } };
        private readonly SetupDB set = new SetupDB();

        [Obsolete]
        public ActionResult<List<UserModel>> Get()
        {
            set.Setup();
            return Ok(user);
        }
    }
}