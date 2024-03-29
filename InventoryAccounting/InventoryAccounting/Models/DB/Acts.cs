﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(ActsMetaData))]
    public partial class Acts : IEntity
    {
        public Acts()
        {
            Tmc = new HashSet<Tmc>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid? ContractId { get; set; }
        public int ActNumber { get; set; }
        public string Type { get; set; }
        public DateTime CompilationDate { get; set; }

        public virtual Contracts Contract { get; set; }
        public virtual ICollection<Tmc> Tmc { get; set; }
    }
}
