using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyMongoWebApp.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
