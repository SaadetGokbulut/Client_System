using MyMongoWebApp.Models;

namespace MyMongoWebApp.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        void Create(Customer customer);
        Customer GetById(string id);
        void Update(string id, Customer customer);
        void Delete(string id);
    }
}