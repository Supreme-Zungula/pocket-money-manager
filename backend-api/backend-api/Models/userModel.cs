using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class userModel
    {
        [Key]
        private int id { get; set; }
        private int familyId { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Role { get; set; }

        /*public static userModel FromDomain(User user)

        {
            return new userModel
            {
                ID = person.ID,
                Name = person.Name,
                Surname = person.Surname,
                Gender = person.Gender,
                Phone = person.Phone,
                Province = person.Province
            };
        }*/
    }
}
