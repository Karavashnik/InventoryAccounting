using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{

    [ModelMetadataType(typeof(ContractsMetaData))]
    public partial class Contracts
    {
        public Contracts()
        {
            Acts = new HashSet<Acts>();
        }

        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public int ContractNumber { get; set; }
        public string Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CompilationDate { get; set; }

        public Companies Company { get; set; }
        public ICollection<Acts> Acts { get; set; }
    }
}
