using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tanks.Domain.Models;
using Tanks.Infrastructure.MongoDB;

namespace Tanks.Infrastructure.Repositories;

public class BattleRepository : IBattleRepository
{
    private readonly IMongoCollection<Battle> _battles;

    public BattleRepository(IMongoDbContext context)
    {
        _battles = context.GetCollection<Battle>("Battles");
    }

    public async Task<Battle> GetByIdAsync(Guid id)
    {
        return await _battles.Find(b => b.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Battle>> GetAllAsync()
    {
        return await _battles.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Battle battle)
    {
        await _battles.InsertOneAsync(battle);
    }

    public async Task UpdateAsync(Battle battle)
    {
        await _battles.ReplaceOneAsync(b => b.Id == battle.Id, battle);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _battles.DeleteOneAsync(b => b.Id == id);
    }
}