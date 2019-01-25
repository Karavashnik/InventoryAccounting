using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAccounting.Models.DB
{
    [ModelMetadataType(typeof(TmcTypesMetaData))]

    public partial class TmcTypes : IEntity
    {
        public TmcTypes()
        {
            Tmc = new HashSet<Tmc>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tmc> Tmc { get; set; }
    }
}
