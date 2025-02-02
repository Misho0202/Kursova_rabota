using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Domain.Models;

namespace Tanks.Application.Interfaces
{
    public interface IBattleService
    {
        Task<Battle> SimulateBattleAsync(Guid tank1Id, Guid tank2Id);
        Task<IEnumerable<Battle>> GetBattleHistoryAsync();
    }
}
