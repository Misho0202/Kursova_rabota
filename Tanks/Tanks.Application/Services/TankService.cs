using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Application.Interfaces;
using Tanks.Domain.DTOs;
using Tanks.Domain.Models;
using Tanks.Infrastructure.Interfaces;

namespace Tanks.Application.Services
{
    public class TankService : ITankService
    {
        private readonly ITankRepository _tankRepository;

        public TankService(ITankRepository tankRepository)
        {
            _tankRepository = tankRepository;
        }

        public async Task<Tank> GetTankByIdAsync(Guid id)
        {
            return await _tankRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Tank>> GetAllTanksAsync()
        {
            return await _tankRepository.GetAllAsync();
        }

        public async Task<Tank> AddTankAsync(TankDto tankDto)
        {
            var tank = new Tank
            {
                Id = Guid.NewGuid(),
                Name = tankDto.Name,
                Health = tankDto.Health
            };
            await _tankRepository.AddAsync(tank);
            return tank; 
        }

        public async Task UpdateTankAsync(Guid id, TankDto tankDto)
        {
            var tank = await _tankRepository.GetByIdAsync(id);
            if (tank == null) throw new Exception("Tank not found");

            tank.Name = tankDto.Name;
            tank.Health = tankDto.Health;
            await _tankRepository.UpdateAsync(tank);
        }

        public async Task DeleteTankAsync(Guid id)
        {
            await _tankRepository.DeleteAsync(id);
        }
    }
}
