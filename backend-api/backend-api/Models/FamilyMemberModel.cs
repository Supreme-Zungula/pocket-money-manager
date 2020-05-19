using Domain.DefinitionObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class FamilyMemberModel
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

        public static FamilyMemberModel FromDomain(FamilyMember user)
        {
            if (user == null)
                return null;

            return new FamilyMemberModel
            {
                Id = user.Id,
                FamilyId = user.FamilyId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Relationship = user.Relationship,
                Phone = user.Phone
            };
        }

        public FamilyMember ToDomain()
        {
            return new FamilyMember
            {
                Id = this.Id,
                FamilyId = this.FamilyId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Relationship = this.Relationship,
                Phone = this.Phone
            };
        }
    }
}
