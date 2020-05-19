using System;
using System.Collections.Generic;
using System.IO;
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
    public class FamilyMemberController : ControllerBase
    {
        private readonly FamilyMemberCrudService familyCrud = new FamilyMemberCrudService();

        public ActionResult Get()
        {
            return Ok("Family Members running");
        }

        [HttpGet]
        [Route("getmembers/{FamilyId}")]
        public ActionResult<FamilyMemberModel> GetFamilyMembers(int familyId)
        {
            var members = familyCrud.GetAllMembers(familyId);
            var result = members.Select(member => FamilyMemberModel.FromDomain(member));

            return Ok(result.ToList());    
        }

        [HttpGet]
        [Route("getbyphone/{Phone}")]
        public ActionResult<List<FamilyMemberModel>> GetUserByPhone(string phone)
        {

            FamilyMemberModel existingUser = FamilyMemberModel.FromDomain(familyCrud.GetMemberByPhone(phone));

            if (existingUser == null)
                return NotFound("Member not found!");

            return Ok(existingUser);
        }

        [HttpPost]
        public ActionResult Post(FamilyMemberModel userDetails)
        {
            familyCrud.AddMember(userDetails.ToDomain());
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(userDetails.FirstName));
            return Created(resourceUrl, userDetails);
        }

        [HttpPut]
        public ActionResult Put(FamilyMemberModel userDetails)
        {
            FamilyMemberModel ex = FamilyMemberModel.FromDomain(familyCrud.GetMemberByPhone("0732255321"));
            userDetails.Id = ex.Id;
            FamilyMemberModel existingUser = FamilyMemberModel.FromDomain(familyCrud.GetMemberById(userDetails.Id));

            if (existingUser == null)
            {
                return BadRequest("Member not found!");
            }
            else
            {
                familyCrud.UpdateMember(userDetails.ToDomain(), userDetails.Id);
                return Ok();
            }
        }

        [HttpDelete]
        [Route("{Phone}")]
        public ActionResult Delete(string phone)
        {
            FamilyMemberModel existingUser = FamilyMemberModel.FromDomain(familyCrud.GetMemberByPhone(phone));

            if (existingUser == null)
            {
                return NotFound();
            }
            else
            {
                var id = existingUser.Id;
                familyCrud.DeleteMember(id);
                return NoContent();
            }
        }
    }
}