using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
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
            var contracts = (Contracts)validationContext.ObjectInstance;
            if(contracts == null) return new ValidationResult("Модель пустая");
            
            var context = (InventoryAccountingContext)validationContext
                .GetService(typeof(InventoryAccountingContext));
            var contract = context?.Contracts.FirstOrDefault(x => x.ContractNumber == (int) value);
            
            return contract == null ? ValidationResult.Success : new ValidationResult("Контракт с таким номером уже существует.");
        }
    }
}