using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly FamilyCrudService familyCrud = new FamilyCrudService();
        private readonly UserCrudService userCrud = new UserCrudService();

        [HttpGet]
        [Route("family")]
        public ActionResult getFamily(UserModel userDetails)
        {
            var user = UserModel.FromDomain(userCrud.GetUserByPhone(userDetails.Phone));
            if (user != null)
            {
                return Ok(familyCrud.GetAllMembers(user.FamilyId).ToList());
            }
            else
                return BadRequest("The user does not exist");
        }


        [HttpPost]
        [Route("create_family")]
        public ActionResult createFamily(UserModel userDetails)
        {
            var user = UserModel.FromDomain(userCrud.GetUserByPhone(userDetails.Phone));
            user.FamilyId = ObjectId.GenerateNewId().Increment;
            userCrud.UpdateUser(user.ToDomain(), user.Id);

            return Ok(user);
        }

        [HttpPost]
        [Route("add_member")]
        public ActionResult addMember(InputMap inMap)
        {
            var user1 = inMap.user1;
            var user2 = inMap.user2;
            var userToJoin = UserModel.FromDomain(userCrud.GetUserById(user1.Id));
            var joiningUser = UserModel.FromDomain(userCrud.GetUserById(user2.Id));

            if (userToJoin == null) { return BadRequest("The user to join is not found"); }
            if (joiningUser == null) return BadRequest("The joining user is not found");

            if (userToJoin.FamilyId != null)
            {
                joiningUser.FamilyId = userToJoin.FamilyId;
                userCrud.UpdateUser(joiningUser.ToDomain(), joiningUser.Id);
            }
            return Ok("The member was added");
        }

        [HttpDelete]
        [Route("remove_Member")]
        public ActionResult removeMember(UserModel userDetails)
        {
            var user = UserModel.FromDomain(userCrud.GetUserById(userDetails.Id));

            if (user != null)
            {
                user.FamilyId = -1;
                userCrud.UpdateUser(user.ToDomain(), user.Id);
            }
            else return BadRequest("The user is not found");
            return Ok("The member was deleted");
        }

        [HttpDelete]
        [Route("/family/rmf")]
        public ActionResult removeFamily()
        {
            return Ok("The family was removed");
        }
    }
    public class InputMap
    {
        public UserModel user1 { get; set; }
        public UserModel user2 { get; set; }
    }
}