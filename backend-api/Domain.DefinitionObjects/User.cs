using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DefinitionObjects
{
    public class User
    {
        [Key]
        private int id { get; set; }
        private int familyId { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Role { get; set; }
    }
}
