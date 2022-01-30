using Microsoft.EntityFrameworkCore;
using Senac.RegistroPonto.Domain.Entidades;
using Senac.RegistroPonto.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity, IAggregateRoot
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual T Adicionar(T entity)
        {
            var entry = _dbSet.Add(entity);
            return entry.Entity;
        }
        public virtual T Alterar(T entity)
        {
            //var entry = _dbSet.Update(entity);

            var obj = _dbSet.Find(entity.Id);
            var entry = _context.Attach(obj);

            entry.CurrentValues.SetValues(entity);

            return entry.Entity;
        }
        public virtual async Task<T> ObterPorIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }
    }
}
