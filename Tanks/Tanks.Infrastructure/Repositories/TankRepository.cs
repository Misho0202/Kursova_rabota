using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tanks.Domain.Models;
using Tanks.Infrastructure.Interfaces;
using Tanks.Infrastructure.MongoDB;

namespace Tanks.Infrastructure.Repositories
{
    public class TankRepository : ITankRepository
    {
        private readonly IMongoCollection<Tank> _tanks;

        public TankRepository(IMongoDbContext context)
        {
            _tanks = context.GetCollection<Tank>("Tanks");
        }

        public async Task<Tank> GetByIdAsync(Guid id)
        {
            return await _tanks.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tank>> GetAllAsync()
        {
            return await _tanks.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Tank tank)
        {
            await _tanks.InsertOneAsync(tank);
        }

        public async Task UpdateAsync(Tank tank)
        {
            await _tanks.ReplaceOneAsync(t => t.Id == tank.Id, tank);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _tanks.DeleteOneAsync(t => t.Id == id);
        }
    }
}
