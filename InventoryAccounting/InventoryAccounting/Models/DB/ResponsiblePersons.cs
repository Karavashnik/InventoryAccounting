using System;
using System.Collections.Generic;
using InventoryAccounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(ResponsiblePersonsMetaData))]
    public partial class ResponsiblePersons
    {
        public ResponsiblePersons()
        {
            Tmc = new HashSet<Tmc>();
        }

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

        public ICollection<Tmc> Tmc { get; set; }
    }
}
