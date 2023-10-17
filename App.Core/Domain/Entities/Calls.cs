using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Domain.Entities
{
    public class Calls :Auditory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CallType { get; set; }

        public string  TableId { get; set; }

       public TableBase Table { get; set; }

    }


}



