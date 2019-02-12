using System.ComponentModel.DataAnnotations;
using System.Linq;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Filters
{
    public class ContractUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var contracts = (Contracts)validationContext.ObjectInstance;
            if(contracts == null) return new ValidationResult("Модель пустая");
            
            var context = (InventoryAccountingContext)validationContext
                .GetService(typeof(InventoryAccountingContext));
            if (context?.Contracts.Any(x => x.ContractNumber == (int) value && x.Id == contracts.Id) == true)
            {
                return ValidationResult.Success;
            }
            var contract = context?.Contracts.Any(x => x.ContractNumber == (int) value);
            return contract == false ? ValidationResult.Success : new ValidationResult("Контракт с таким номером уже существует.");
        }
    }
}