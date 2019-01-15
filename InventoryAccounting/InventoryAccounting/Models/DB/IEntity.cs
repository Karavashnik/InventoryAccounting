using System;

namespace InventoryAccounting.Models.DB
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
