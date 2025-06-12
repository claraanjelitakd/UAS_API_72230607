using System;
using SimpleRESTApi.Models;
namespace SimpleRESTApi.Data
{
    public interface ICustomers
    {
        //crud
        IEnumerable<Customers> GetCustomers();
        Customers GetCustomersById(int CustomerID);
        Customers addCustomers(Customers Customers);
        Customers updateCustomers(Customers Customers);
        void deleteCustomers(int CustomerID);
    }
}