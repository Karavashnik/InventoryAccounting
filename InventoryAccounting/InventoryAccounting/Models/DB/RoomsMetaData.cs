using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAccounting.Models.DB
{
    public class RoomsMetaData
    {
        public Guid Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Этаж")]
        public int Floor { get; set; }
        [Display(Name = "Номер")]
        public int Number { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        public ICollection<Tmc> Tmc { get; set; }
    }
}
