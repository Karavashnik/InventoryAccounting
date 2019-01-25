using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(ContractsMetaData))]
    public partial class Contracts : IEntity
    {
        public Contracts()
        {
            Acts = new HashSet<Acts>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public int ContractNumber { get; set; }
        public string Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CompilationDate { get; set; }

        public virtual Companies Company { get; set; }
        public virtual ICollection<Acts> Acts { get; set; }
    }
}
