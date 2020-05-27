using Data;
using Repositories;
using System.Collections.Generic;

namespace Domain.Services
{
    public class FamilyCrudService
    {
        private readonly FamilyRepositories repositories = new FamilyRepositories();

        public List<UserData> GetAllMembers(int familyId)
        {
            return repositories.GetAllMembers(familyId);
        }

        public List<UserData> GetMembersById(int id)
        {
            var result = repositories.GetMembersById(id);
            return result;
        }

        public void removeMember(string id)
        {
            repositories.removeMember(id);
        }
    }
}
