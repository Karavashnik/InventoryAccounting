using System;
using System.Collections.Generic;
using InventoryAccounting.Models;

namespace InventoryAccounting.Models.DB
{
    public partial class Rooms
    {
        public Rooms()
        {
            Tmc = new HashSet<Tmc>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public string Phone { get; set; }

        public ICollection<Tmc> Tmc { get; set; }
    }
}
