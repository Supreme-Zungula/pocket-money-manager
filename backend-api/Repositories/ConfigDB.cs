using System;
using System.Collections.Generic;
using System.Text;
using Domain.DefinitionObjects;
using MongoDB.Driver;

namespace Repositories
{
    public class ConfigDB
    {
        [Obsolete]
        public void setupDB()
        {
            _ = new DBSetUp();
        }

    }
}
