using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        Customer AddCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }
}