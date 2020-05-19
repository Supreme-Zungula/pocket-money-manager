using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class FamilyMemberData
    {
        [Key]
        public ObjectId Id { get; set; }
        public int FamilyId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Relationship { get; set; }
        [Required]
        [MinLength(10)]
        public string Phone { get; set; }
    }
}
