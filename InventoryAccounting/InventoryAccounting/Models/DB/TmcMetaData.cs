using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAccounting.Models.DB
{
    public class TmcMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Инвентарный номер")]
        public int InventoryNumber { get; set; }
        [Required]
        [MaxLength(80)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [MaxLength(255)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Тип")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Дата покупки")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PurchaseDate { get; set; }
        [Required]
        [ForeignKey("Persons")]
        [Display(Name = "Ответственное лицо")]
        public Guid ResponsiblePersonId { get; set; }
        [Required]
        [Display(Name = "Заводской номер")]
        public int FactoryNumber { get; set; }
        [Display(Name = "Дата списания")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? WriteOffDate { get; set; }
        [Required]
        [ForeignKey("Rooms")]
        [Display(Name = "Помещение")]
        public Guid RoomId { get; set; }
        [Display(Name = "Акт")]
        [ForeignKey("Acts")]
        public int? ActId { get; set; }
        [Display(Name = "Гарантия")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? WarrantyDate { get; set; }

        [ForeignKey("ActId")]
        [Display(Name = "Акт")]
        public Acts Act { get; set; }
        [ForeignKey("ResponsiblePersonId")]
        [Display(Name = "Ответственное лицо")]
        public Persons ResponsiblePerson { get; set; }
        [ForeignKey("RoomId")]
        [Display(Name = "Помещение")]
        public Rooms Room { get; set; }
    }
}
