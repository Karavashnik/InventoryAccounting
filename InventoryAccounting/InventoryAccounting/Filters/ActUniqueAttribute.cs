using System.ComponentModel.DataAnnotations;
using System.Linq;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Filters
{
    public class ActUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var acts = (Acts)validationContext.ObjectInstance;
            if(acts == null) return new ValidationResult("Модель пустая");
            
            var context = (InventoryAccountingContext)validationContext
                .GetService(typeof(InventoryAccountingContext));
            if (context?.Acts.Any(x => x.ActNumber == (int) value && x.Id == acts.Id) == true)
            {
                return ValidationResult.Success;
            }
            var act = context?.Acts.Any(x => x.ActNumber == (int) value);
            return act == false ? ValidationResult.Success : new ValidationResult("Акт с таким номером уже существует.");
        }
    }
}