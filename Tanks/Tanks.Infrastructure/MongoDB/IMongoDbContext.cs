using MongoDB.Driver;

namespace Tanks.Infrastructure.MongoDB
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }

}
