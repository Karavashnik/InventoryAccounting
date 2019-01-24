using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(ActsMetaData))]
    public partial class Acts : IEntity, IValidatableObject
    {
        public Acts()
        {
            Tmc = new HashSet<Tmc>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid? ContractId { get; set; }
        public int? ActNumber { get; set; }
        public string Type { get; set; }
        public DateTime? CompilationDate { get; set; }

        public Contracts Contract { get; set; }
        public ICollection<Tmc> Tmc { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var context = new InventoryAccountingContext())
            {
                var existingAct = context.Acts.FirstOrDefaultAsync(x=>x.ActNumber == this.ActNumber);
                if (existingAct != null)
                {
                    yield return new ValidationResult("Акт с таким номером уже существует.");
                }
            }
        }
    }
}
