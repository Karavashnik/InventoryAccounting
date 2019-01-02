using System;
using System.Collections.Generic;

namespace InventoryAccounting.Models.DB
{
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
