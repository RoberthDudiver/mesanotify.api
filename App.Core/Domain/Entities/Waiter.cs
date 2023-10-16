using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Domain.Entities
{
    public class Waiter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }

}
