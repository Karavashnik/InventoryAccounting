using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(ActsMetaData))]
    public partial class Acts
    {
        public Acts()
        {
            Tmc = new HashSet<Tmc>();
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public Guid? ContractId { get; set; }
        public int ActNumber { get; set; }
        public string Type { get; set; }
        public DateTime CompilationDate { get; set; }

        public Contracts Contract { get; set; }
        public ICollection<Tmc> Tmc { get; set; }
    }
}
