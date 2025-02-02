using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanks.Domain.Models;

namespace Tanks.Infrastructure.Repositories;

public interface IBattleRepository
{
    Task<Battle> GetByIdAsync(Guid id);
    Task<IEnumerable<Battle>> GetAllAsync();
    Task AddAsync(Battle battle);
    Task UpdateAsync(Battle battle);
    Task DeleteAsync(Guid id);
}