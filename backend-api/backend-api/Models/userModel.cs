using Domain.DefinitionObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }

        public static UserModel FromDomain(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FamilyId = user.FamilyId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Relationship = user.Relationship
            };
        }

        public User ToDomain()
        {
            return new User
            {
                Id = this.Id,
                FamilyId = this.FamilyId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Relationship = this.Relationship,
            };
        }
    }
}
