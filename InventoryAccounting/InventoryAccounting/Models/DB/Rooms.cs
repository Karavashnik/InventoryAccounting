using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{

    [ModelMetadataType(typeof(RoomsMetaData))]
    public partial class Rooms : IEntity
    {
        public Rooms()
        {
            Tmc = new HashSet<Tmc>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public string Phone { get; set; }

        public ICollection<Tmc> Tmc { get; set; }
    }
}
