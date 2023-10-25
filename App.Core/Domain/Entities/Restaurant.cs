using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Domain.Entities
{
    public class Restaurant: Auditory
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        public string Logo { get; set; }
        public string Banner { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }
        public List<ContactBase> Contact { get; set; }

        public List<TableBase> Tables { get; set; }

        public string MenuImage { get; set; } 


    }

}
