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
            if (context?.Persons.Any(x => x.PersonnelNumber == (int) value && x.Id == persons.Id) == true)
            {
                return ValidationResult.Success;
            }
            var person = context?.Persons.Any(x => x.PersonnelNumber == (int) value);
            return person == false ? ValidationResult.Success : new ValidationResult("Работник с таким табельным номером уже существует.");
        }
    }
}