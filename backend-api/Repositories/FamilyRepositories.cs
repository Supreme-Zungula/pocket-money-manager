using Data;
using Domain.DefinitionObjects;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Repositories
{
    public class FamilyRepositories
    {
        private UserRepositories userCrud = new UserRepositories();
        private GetDB database = new GetDB();
        private IMongoCollection<UserData> family;

        public FamilyRepositories()
        {
            family = database.GetUserCollection();
        
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
            var filter = Builders<UserData>.Filter.Empty;
            var users = family.Find<UserData>(filter).ToList();
            return users;
        }

        public List<UserData> GetMembersById(int id)
        {
            var filter = Builders<UserData>.Filter.Eq("familyId", id);
            var ret = family.Find(filter).ToListAsync().Result;
            return ret;
        }
        public bool removeMember(string id)
        {
            var user = (userCrud.GetUserById(id));

            if (user != null)
            {
                user.FamilyId = -1;
                userCrud.UpdateUser(user, user.Id);
                return true;
            }
            return false;
        }
    }
}
