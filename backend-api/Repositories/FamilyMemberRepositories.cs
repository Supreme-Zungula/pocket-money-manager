using Data;
using Domain.DefinitionObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class FamilyMemberRepositories : IFamilyMemberOperations
    {
        private GetDB database = new GetDB();
        private IMongoCollection<FamilyMemberData> FamilyMembers;

        public FamilyMemberRepositories()
        {
            FamilyMembers = database.GetFamilyMembersCollection();
        }

        public FamilyMember ToDomain(FamilyMemberData userData)
        {
            if (userData == null)
                return null;

            return new FamilyMember
            {
                Id = userData.Id,
                FamilyId = userData.FamilyId,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Relationship = userData.Relationship,
                Phone = userData.Phone
            };
        }

        public FamilyMemberData FromDomain(FamilyMember userData)
        {
            if (userData == null)
                return null;

            return new FamilyMemberData
            {
                Id = userData.Id,
                FamilyId = userData.FamilyId,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Relationship = userData.Relationship,
                Phone = userData.Phone
            };
        }

        public List<FamilyMember> GetAllMembers(int familyId)
        {
            var users = FamilyMembers.Find<FamilyMemberData>(members => members.FamilyId == familyId).ToList();
            var result = users.Select(member => ToDomain(member));
            return result.ToList();
        }

        public void AddMember(FamilyMember userDetails)
        {
            FamilyMembers.InsertOne(FromDomain(userDetails));
        }

        public void DeleteMember(string id)
        {
            FamilyMembers.DeleteOne(member => member.Id == id);
        }

        public FamilyMember GetMemberById(string id)
        {
            return ToDomain(FamilyMembers.Find<FamilyMemberData>(member => member.Id == id).FirstOrDefault());
        }

        public FamilyMember GetMemberByPhone(string phone)
        {
            return ToDomain(FamilyMembers.Find<FamilyMemberData>(member => member.Phone == phone).FirstOrDefault());
        }

        public void UpdateMember(FamilyMember userDetails, string id)
        {
            FamilyMembers.ReplaceOne(member => member.Id == id, FromDomain(userDetails));
        }
    }
}
