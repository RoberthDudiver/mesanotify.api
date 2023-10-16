using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Domain.Entities
{
    public class Table: TableBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
   
    }


      public class TableBase
    {
        public string ExternalId { get; set; }

        public string TableNumber { get; set; }

        public string QrCode { get; set; }

        public bool IsAvailable { get; set; }
    }
}
