using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
