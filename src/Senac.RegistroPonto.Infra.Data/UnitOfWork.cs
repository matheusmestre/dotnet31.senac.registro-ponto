using Senac.RegistroPonto.Domain.Interfaces;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
