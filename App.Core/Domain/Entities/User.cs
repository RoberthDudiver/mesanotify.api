using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    public class User : Auditory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }


        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }


        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("ExternalId")]
        public string ExternalId { get; set; }

    }
}
