using Domain.DefinitionObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    interface IFamilyMemberOperations
    {
        void AddMember(FamilyMember userDetails);
        void UpdateMember(FamilyMember userDetails, string id);
        void DeleteMember(string id);
        FamilyMember GetMemberById(string id);
        FamilyMember GetMemberByPhone(string phone);
    }
}
