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
        }

        public int Id { get; set; }
        public DateTime CompilationDate { get; set; }
        public int ContractNumber { get; set; }

        public Contracts ContractNumberNavigation { get; set; }
        public ICollection<Tmc> Tmc { get; set; }
    }
}
