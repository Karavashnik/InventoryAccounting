using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAccounting.Models.DB
{
    public class ActsMetaData
    {
        [Display(Name = "Id акта")]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Номер акта")]
        public int ActNumber { get; set; }
        [ForeignKey("Contracts")]
        [Display(Name = "Договор")]
        public Guid? ContractId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name="Тип договора")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Дата составления")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CompilationDate { get; set; }

        [Display(Name = "Договор")]
        public Contracts Contract { get; set; }
    }
}
