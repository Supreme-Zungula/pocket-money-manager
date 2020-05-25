using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class FamilyData
    {
        [Key]
        public ObjectId Id { get; set; }
    }
}
