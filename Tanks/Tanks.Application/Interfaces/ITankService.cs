using Tanks.Domain.DTOs;
using Tanks.Domain.Models;
using Tanks.Infrastructure.Interfaces;

namespace Tanks.Application.Interfaces
{
    public interface ITankService
    {
        Task<Tank> GetTankByIdAsync(Guid id);
        Task<IEnumerable<Tank>> GetAllTanksAsync();
        Task<Tank> AddTankAsync(TankDto tankDto);
        Task UpdateTankAsync(Guid id, TankDto tankDto);
        Task DeleteTankAsync(Guid id);
    }
}
