using MongoDB.Driver;
using MyMongoWebApp.Models;
using Microsoft.Extensions.Configuration;

namespace MyMongoWebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(IConfiguration configuration)
        {
            var mongoDbSettings = new MongoDbSettings
            {
                ConnectionString = configuration["MongoDbSettings:ConnectionString"],
                DatabaseName = configuration["MongoDbSettings:DatabaseName"],
                CollectionName = configuration["MongoDbSettings:CollectionName"]
            };

            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            _customers = database.GetCollection<Customer>(mongoDbSettings.CollectionName);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers.Find(customer => true).ToList();
        }

        public void Create(Customer customer)
        {
            _customers.InsertOne(customer);
        }

        public Customer GetById(string id)
        {
            return _customers.Find(customer => customer.Id == id).FirstOrDefault();
        }

        public void Update(string id, Customer customer)
        {
            _customers.ReplaceOne(c => c.Id == id, customer);
        }

        public void Delete(string id)
        {
            _customers.DeleteOne(customer => customer.Id == id);
        }
    }
}