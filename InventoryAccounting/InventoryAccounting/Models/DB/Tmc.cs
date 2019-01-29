using System;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(TmcMetaData))]
    public partial class Tmc : IEntity
    {
        public Tmc()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int InventoryNumber { get; set; }
        public Guid ResponsiblePersonId { get; set; }
        public Guid? ActId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TypeId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int FactoryNumber { get; set; }
        public DateTime? WriteOffDate { get; set; }
        public Guid RoomId { get; set; }
        public DateTime? WarrantyDate { get; set; }

        public virtual Acts Act { get; set; }
        public virtual Persons ResponsiblePerson { get; set; }
        public virtual Rooms Room { get; set; }
        public virtual TmcTypes Type { get; set; }
    }
}
