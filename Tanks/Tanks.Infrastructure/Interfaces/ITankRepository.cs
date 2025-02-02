using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Domain.Models;

namespace Tanks.Infrastructure.Interfaces
{
    public interface ITankRepository
    {
        Task<Tank> GetByIdAsync(Guid id);
        Task<IEnumerable<Tank>> GetAllAsync();
        Task AddAsync(Tank tank);
        Task UpdateAsync(Tank tank);
        Task DeleteAsync(Guid id);
    }
}
