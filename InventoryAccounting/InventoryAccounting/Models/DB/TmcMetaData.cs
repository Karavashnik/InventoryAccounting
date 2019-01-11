using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAccounting.Models.DB
{
    public class TmcMetaData
    {
        [Display(Name = "Инвентарный номер")]
        public int InventoryNumber { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Тип")]
        public string Type { get; set; }
        [Display(Name = "Дата покупки")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "Ответственное лицо")]
        public int PesponsiblePersonNumber { get; set; }
        [Display(Name = "Заводской номер")]
        public int FactoryNumber { get; set; }
        [Display(Name = "Дата списания")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? WriteOffDate { get; set; }
        [Display(Name = "Помещение")]
        public Guid RoomId { get; set; }
        [Display(Name = "Акт")]
        public int? ActId { get; set; }
        [Display(Name = "Гарантия")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? WarrantyDate { get; set; }

        [Display(Name = "Акт")]
        public Acts Act { get; set; }
        [Display(Name = "Ответственное лицо")]
        public Persons ResponsiblePerson { get; set; }
        [Display(Name = "Помещение")]
        public Rooms Room { get; set; }
    }
}
