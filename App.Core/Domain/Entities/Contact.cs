using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Domain.Entities
{
    public class Contact: ContactBase
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
     
    }

    public class ContactBase
    {


        public string Name { get; set; }

        public string Value { get; set; }

    }

}
