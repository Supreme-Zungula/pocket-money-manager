using Domain.DefinitionObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class FamilyModel
    {
        [Key]
        public ObjectId Id { get; set; }

        public static FamilyModel FromDomain(Family fam)
        {
            return new FamilyModel
            {
                Id = fam.Id,
            };
        }

        public Family ToDomain()
        {
            return new Family
            {
                Id = this.Id
            };
        }
    }
}
