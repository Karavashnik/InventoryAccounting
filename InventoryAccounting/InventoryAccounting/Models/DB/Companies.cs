﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{

    [ModelMetadataType(typeof(CompaniesMetaData))]
    public partial class Companies
    {
        public Companies()
        {
            Contracts = new HashSet<Contracts>();
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public int Unp { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DirectorsName { get; set; }
        public string DirectorsPhone { get; set; }

        public ICollection<Contracts> Contracts { get; set; }
    }
}
