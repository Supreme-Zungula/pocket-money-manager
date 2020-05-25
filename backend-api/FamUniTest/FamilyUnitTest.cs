using Data;
using Domain.DefinitionObjects;
using Domain.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace FamilyCrudUniTest
{
    public class FamilyUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllMembers()
        {
            FamilyCrudService family = new FamilyCrudService();
            List<UserData> familymember = family.GetAllMembers(3704290);
            Assert.IsNotNull(familymember);
        }

        [Test]
        public void GetMembersbyId()
        {
            FamilyCrudService family = new FamilyCrudService();
            List<UserData> familymember = family.GetMembersById(3704290);
            Assert.IsNotNull(familymember);
        }
    }
}