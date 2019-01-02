using System;
using System.Collections.Generic;

namespace InventoryAccounting.Models.DB
{
    public partial class Tmc
    {
        public int InventoryNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PesponsiblePersonNumber { get; set; }
        public int FactoryNumber { get; set; }
        public DateTime? WriteOffDate { get; set; }
        public Guid RoomId { get; set; }
        public int? ActId { get; set; }
        public DateTime? WarrantyDate { get; set; }

        public Acts Act { get; set; }
        public ResponsiblePersons PesponsiblePersonNumberNavigation { get; set; }
        public Rooms Room { get; set; }
    }
}
