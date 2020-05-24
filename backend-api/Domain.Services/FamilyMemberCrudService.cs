using Domain.DefinitionObjects;
using MongoDB.Bson;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class FamilyMemberCrudService
    {
        private readonly FamilyMemberRepositories repositories = new FamilyMemberRepositories();

        public List<FamilyMember> GetAllMembers(int familyId)
        {
            return repositories.GetAllMembers(familyId);
        }

        public void AddMember(FamilyMember userDetails)
        {
            repositories.AddMember(userDetails);
        }

        public FamilyMember GetMemberById(string id)
        {
            FamilyMember result = repositories.GetMemberById(id);
            return result;
        }

        public FamilyMember GetMemberByPhone(string phone)
        {
            FamilyMember result = repositories.GetMemberByPhone(phone);
            return result;
        }

        public void UpdateMember(FamilyMember updatedDetails, string id)
        {
            repositories.UpdateMember(updatedDetails, id);
        }

        public void DeleteMember(string id)
        {
            repositories.DeleteMember(id);
        }
    }
}
