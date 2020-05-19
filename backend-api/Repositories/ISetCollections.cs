using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    interface ISetCollections
    {
        void UsersCollection(MongoDatabase db);
        void FamilyMembersCollection(MongoDatabase db);
    }
}
