using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DefinitionObjects
{
    public class Family
    {
        [Key]
        public ObjectId Id { get; set; }
        public ObjectId HeadOfFamily { get; set; }
    }
}
