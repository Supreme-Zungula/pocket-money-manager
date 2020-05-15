using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    interface ISetCollections
    {
        void UsersCollection(MongoDatabase db);
    }
}
