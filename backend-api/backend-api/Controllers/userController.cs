﻿using System;
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
        private readonly SetupDB set = new SetupDB();

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

            if (existingUser == null)
                return NotFound("member not found!");

            return Ok(existingUser);
        }

        [HttpPost]
        public ActionResult Post(UserModel userDetails)
        {
            if (userDetails.FamilyId == 0)
                userDetails.FamilyId = ObjectId.GenerateNewId().Increment;
            userService.Register(userDetails.ToDomain());
            var userFromDB = UserModel.FromDomain(userService.GetUserByPhone(userDetails.Phone));
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(userDetails.FirstName));
            return Created(resourceUrl, userFromDB);
        }

        [HttpPut]
        public ActionResult Put(UserModel userDetails)
        {

            UserModel existingUser = UserModel.FromDomain(userService.GetUserById(userDetails.Id));

            if (existingUser == null)
            {
                return BadRequest("User not found!");
            }
            else
            {
                userDetails.Id = existingUser.Id;
                userDetails.FamilyId = existingUser.FamilyId;
                userService.UpdateUser(userDetails.ToDomain(), userDetails.Id);
                return Ok();
            }
        }

        [HttpDelete]
        [Route("{Phone}")]
        public ActionResult Delete(string phone)
        {
            UserModel existingUser = UserModel.FromDomain(userService.GetUserByPhone(phone));

            if (existingUser == null)
            {
                return NotFound();
            }
            else
            {
                var id = existingUser.Id;
                userService.DeleteUser(id);
                return NoContent();
            }
        }
    }
}