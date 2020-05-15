using Domain.DefinitionObjects;
using MongoDB.Bson;
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
        public ObjectId Id { get; set; }
        public int FamilyId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [MinLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

        public static UserModel FromDomain(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FamilyId = user.FamilyId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Phone = user.Phone,
                Password = user.Password
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
                Role = this.Role,
                Phone = this.Phone,
                Password = this.Password
            };
        }
    }
}
