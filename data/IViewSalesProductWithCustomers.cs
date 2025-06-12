using System.Collections.Generic;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface IViewSalesProductWithCustomers
    {
        IEnumerable<ViewSalesProductWithCustomers> GetViewSalesProductWithCustomers();
        ViewSalesProductWithCustomers GetViewSalesProductWithCustomers(int saleId); // Mengambil data berdasarkan ID di view
    }
}