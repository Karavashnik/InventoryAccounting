using System.ComponentModel.DataAnnotations;
using System.Linq;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Filters
{
    public class UnpUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var companies = (Companies)validationContext.ObjectInstance;
            if(companies == null) return new ValidationResult("Модель пустая");
            
            var context = (InventoryAccountingContext)validationContext
                .GetService(typeof(InventoryAccountingContext));
            var company = context?.Companies.FirstOrDefault(x => x.Unp == (int) value);
            
            return company == null ? ValidationResult.Success : new ValidationResult("УНП уже существует.");
        }
    }
}