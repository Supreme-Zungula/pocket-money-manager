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
        void UpdateMember(FamilyMember userDetails, ObjectId id);
        void DeleteMember(ObjectId id);
        FamilyMember GetMemberById(ObjectId id);
        FamilyMember GetMemberByPhone(string phone);
    }
}
