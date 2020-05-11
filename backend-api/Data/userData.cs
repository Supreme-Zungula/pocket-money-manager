using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class UserData
    {
        public ObjectId Id { get; set; }
        public int FamilyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
    }
}
