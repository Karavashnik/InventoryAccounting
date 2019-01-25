using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryAccounting.Models.DB
{
    public partial class TmcTypesMetaData 
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }

        public virtual ICollection<Tmc> Tmc { get; set; }
    }
}
