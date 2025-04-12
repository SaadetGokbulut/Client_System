using Microsoft.AspNetCore.Mvc;
using MyMongoWebApp.Models;
using MyMongoWebApp.Services;

namespace MyMongoWebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Müşteri listesi
        public IActionResult Index()
        {
            var customers = _customerService.GetAll();
            return View(customers);
        }

        // Yeni müşteri ekleme sayfası
        public IActionResult Create()
        {
            return View();
        }

        // Yeni müşteri ekleme işlemi
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            // E-posta doğrulaması
            if (!customer.Email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "E-posta adresi @gmail.com ile bitmelidir.");
                return View(customer);
            }

            if (ModelState.IsValid)
            {
                _customerService.Create(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Düzenleme işlemi (id ile)
        public IActionResult Edit(string id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // Düzenleme işlemi POST
        [HttpPost]
        public IActionResult Edit(string id, Customer customer)
        {
            // E-posta doğrulaması
            if (!customer.Email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "E-posta adresi @gmail.com ile bitmelidir.");
                return View(customer);
            }

            if (ModelState.IsValid)
            {
                _customerService.Update(id, customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
         // GET: Delete Confirmation Page
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer); 
        }

        // POST: Confirm Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            
            _customerService.Delete(id);
            return RedirectToAction("Index");
        }

        
    }
}
