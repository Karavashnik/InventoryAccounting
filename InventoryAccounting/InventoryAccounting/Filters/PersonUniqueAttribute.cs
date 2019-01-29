using System.ComponentModel.DataAnnotations;
using System.Linq;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Filters
{
    public class PersonUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var persons = (Persons)validationContext.ObjectInstance;
            if(persons == null) return new ValidationResult("Модель пустая");
            
            var context = (InventoryAccountingContext)validationContext
                .GetService(typeof(InventoryAccountingContext));
            var person = context?.Persons.FirstOrDefault(x => x.PersonnelNumber == (int) value);
            
            return person == null ? ValidationResult.Success : new ValidationResult("Работник с таким табельным номером уже существует.");
        }
    }
}