using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CustomersEF : ICustomers
    {
        private readonly ApplicationDbContext _context;

    public CustomersEF(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Customers> GetCustomers()
    {
        return _context.Customers.ToList();
    }

    public Customers GetCustomersById(int CustomerID)
    {
        return _context.Customers.FirstOrDefault(c => c.CustomerId == CustomerID);
    }


        public Customers addCustomers(Customers Customers)
        {
                    _context.Customers.Add(Customers);
        _context.SaveChanges();
        return Customers;
        }

        public Customers updateCustomers(Customers Customers)
        {
            var existingCustomer = _context.Customers.Find(Customers.CustomerId);
        if (existingCustomer == null)
            return null;

        existingCustomer.CustomerName = Customers.CustomerName;
        existingCustomer.Email = Customers.Email;
        existingCustomer.Address = Customers.Address;
        // tambahkan field lain sesuai entitas Customers

        _context.SaveChanges();
        return existingCustomer;
        }

        public void deleteCustomers(int CustomerID)
        {
                    var customer = _context.Customers.Find(CustomerID);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
        }
    }
}