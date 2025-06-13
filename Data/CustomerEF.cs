using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public class CustomerEF : ICustomer
    {
        private readonly ApplicationDbContext _context;

        public CustomerEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }
            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("An error occurred while deleting the customer.", ex);
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = from c in _context.Customers
                            orderby c.Name
                            select c;
            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            var customer = (from c in _context.Customers
                            where c.CustomerID == customerId
                            select c).FirstOrDefault();
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            
        }

        Customer ICustomer.AddCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return customer;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("An error occurred while adding the customer.", ex);
            }
        }

        Customer ICustomer.UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerById(customer.CustomerID);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }
            try
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
                _context.Customers.Update(existingCustomer);
                _context.SaveChanges();
                return existingCustomer;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("An error occurred while updating the customer.", ex);
            }
        }
    }
}