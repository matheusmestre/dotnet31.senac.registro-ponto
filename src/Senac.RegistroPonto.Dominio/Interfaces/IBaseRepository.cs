using Senac.RegistroPonto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : Entity, IAggregateRoot
    {
        T Adicionar(T entity);
        T Alterar(T entity);
        Task<T> ObterPorIdAsync(Guid id);
    }
}
