using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class SetupDB
    {
        private readonly ConfigDB read = new ConfigDB();

        [Obsolete]
        public void Setup()
        {
            read.setupDB();
        }
    }
}
