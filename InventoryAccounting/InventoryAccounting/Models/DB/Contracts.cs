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

        public int ContractNumber { get; set; }
        public int CompanyUnp { get; set; }
        public DateTime Date { get; set; }

        public CompanyName CompanyUnpNavigation { get; set; }
        public ICollection<Acts> Acts { get; set; }
    }
}
