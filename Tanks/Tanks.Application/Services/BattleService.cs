using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Application.Interfaces;
using Tanks.Domain.Models;
using Tanks.Infrastructure.Interfaces;
using Tanks.Infrastructure.Repositories;

namespace Tanks.Application.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _battleRepository;
        private readonly ITankRepository _tankRepository;

        public BattleService(IBattleRepository battleRepository, ITankRepository tankRepository)
        {
            _battleRepository = battleRepository;
            _tankRepository = tankRepository;
        }

        public async Task<Battle> SimulateBattleAsync(Guid tank1Id, Guid tank2Id)
        {
            var tank1 = await _tankRepository.GetByIdAsync(tank1Id);
            var tank2 = await _tankRepository.GetByIdAsync(tank2Id);

            if (tank1 == null || tank2 == null) throw new Exception("Tank not found");

            var winner = tank1.Health > tank2.Health ? tank1 : tank2;

            var battle = new Battle
            {
                Id = Guid.NewGuid(),
                Tank1Id = tank1Id,
                Tank2Id = tank2Id,
                WinnerId = winner.Id
            };

            await _battleRepository.AddAsync(battle);
            return battle;
        }

        public async Task<IEnumerable<Battle>> GetBattleHistoryAsync()
        {
            return await _battleRepository.GetAllAsync();
        }
    }
}
