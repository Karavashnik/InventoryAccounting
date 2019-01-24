﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAccounting.Models.DB
{
    public class ActsMetaData
    {
        [Display(Name = "Id акта")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Номер акта")]
        public int? ActNumber { get; set; }
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [ForeignKey("Contracts")]
        [Display(Name = "Договор")]
        public Guid? ContractId { get; set; }
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [MaxLength(50)]
        [Display(Name="Тип договора")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Дата составления")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? CompilationDate { get; set; }

        [Display(Name = "Договор")]
        public Contracts Contract { get; set; }
    }
    
}
