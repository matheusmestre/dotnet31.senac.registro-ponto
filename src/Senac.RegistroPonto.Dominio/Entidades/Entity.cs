using System;
using System.Collections.Generic;
using System.Text;

namespace Senac.RegistroPonto.Domain.Entidades
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; protected set; }
    }
}
