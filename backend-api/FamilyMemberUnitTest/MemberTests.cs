using Domain.DefinitionObjects;
using Domain.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace FamilyMemberUnitTest
{
    public class MemberTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void AddMember()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();

            FamilyMember _member = new FamilyMember { FamilyId = 3704290, FirstName = "Lorem", LastName = "Ipsum", Relationship = "Mother", Phone = "0123456788" };
            _familyCrud.AddMember(_member);

            FamilyMember _getCreatedMember = _familyCrud.GetMemberByPhone("0123456788");
            Assert.AreEqual(_member.Phone, _getCreatedMember.Phone);
        }

        [Test, Order(2)]
        public void GetAllMembers()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();
            List<FamilyMember> _membersList = _familyCrud.GetAllMembers(3704290);
            Assert.IsNotNull(_membersList);
        }

        [Test, Order(3)]
        public void GetMemberbyId()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();
            FamilyMember member = _familyCrud.GetMemberByPhone("0123456788");
            FamilyMember idResult = _familyCrud.GetMemberById(member.Id);
            Assert.IsNotNull(idResult);
        }

        [Test, Order(4)]
        public void GetMemberbyPhone()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();
            FamilyMember member = _familyCrud.GetMemberByPhone("0123456788");
            Assert.IsNotNull(member);
        }

        [Test, Order(5)]
        public void GetMemberbyPhoneNotFound()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();
            FamilyMember member = _familyCrud.GetMemberByPhone("0800123457");
            Assert.IsNull(member);
        }

        [Test, Order(6)]
        public void UpdateMember()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();
            FamilyMember member = _familyCrud.GetMemberByPhone("0123456788");
            FamilyMember updateInfo = new FamilyMember { Id = member.Id, FamilyId = member.FamilyId, FirstName = "Foo", LastName = "Ipsum", Relationship = "Mother", Phone = "0123456788" };
            _familyCrud.UpdateMember(updateInfo, member.Id);

            FamilyMember getUpdatedMember = _familyCrud.GetMemberById(member.Id);
            Assert.AreNotEqual(member.FirstName, getUpdatedMember.FirstName);
        }

        [Test, Order(7)]
        public void DeleteMember()
        {
            FamilyMemberCrudService _familyCrud = new FamilyMemberCrudService();
            FamilyMember member = _familyCrud.GetMemberByPhone("0123456788");
            _familyCrud.DeleteMember(member.Id);

            FamilyMember deletedMember = _familyCrud.GetMemberByPhone("0123456788");
            Assert.IsNull(deletedMember);
        }
    }
}