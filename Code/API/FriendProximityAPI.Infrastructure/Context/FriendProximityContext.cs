using FriendProximityAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FriendProximityAPI.Infrastructure.Context
{
    public class FriendProximityContext
    {
        private readonly IMongoDatabase _database = null;

        public FriendProximityContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            if (client != null)
                _database = client.GetDatabase(configuration.GetValue<string>("ConnectionStrings:DefaultDatabaseName"));
        }

        public IMongoCollection<Friend> Friend
        {
            get
            {
                return _database.GetCollection<Friend>("Friend");
            }
        }
    }
}
