using System;

namespace Infrastructure.Common.Entities
{
    public abstract class BaseEntity
    {
        //TODO: Voltar para Guid
        //public Guid Id { get; set; } = Guid.NewGuid();

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
