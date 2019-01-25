using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(PersonsMetaData))]
    public partial class Persons : IEntity
    {
        public Persons() 
        {
            Tmc = new HashSet<Tmc>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int PersonnelNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PassportDetails { get; set; }
        public string Education { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Post { get; set; }

        public virtual ICollection<Tmc> Tmc { get; set; }
    }
}
