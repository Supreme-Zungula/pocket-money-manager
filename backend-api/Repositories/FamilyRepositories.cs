using Data;
using Domain.DefinitionObjects;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class FamilyRepositories
    {
        private GetDB database = new GetDB();
        private IMongoCollection<UserData> Family;

        public FamilyRepositories()
        {
            Family = database.GetUserCollection();
            var filter = Builders<UserData>.Filter.Empty;

            foreach (UserData user in Family.Find(filter).ToListAsync().Result)
            {

            }
        }

        public Family ToDomain(FamilyData familyData)
        {
            if (familyData == null)
                return null;

            return new Family
            {
                Id = familyData.Id
            };
        }

        public FamilyData FromDomain(Family familyData)
        {
            if (familyData == null)
                return null;

            return new FamilyData
            {
                Id = familyData.Id,
            };
        }

        public List<UserData> GetAllMembers(int familyId)
        {
            var filter = Builders<UserData>.Filter.Eq("FamilyId", familyId);
            var users = Family.Find<UserData>(filter).ToList();
            return users;
        }


        public List<UserData> GetMembersById(int id)
        {
            var filter = Builders<UserData>.Filter.Eq("familyId", id);
            var ret = Family.Find(filter).ToListAsync().Result;
            return ret;
        }
    }
}
