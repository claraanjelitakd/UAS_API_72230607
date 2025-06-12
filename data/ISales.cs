using System;
using SimpleRESTApi.Models;
namespace SimpleRESTApi.Data
{
    public interface ISales
    {
        //crud
        IEnumerable<Sales> GetSales();
        Sales GetSalesById(int SalesID);
        Sales addSales(Sales Sales);
        Sales updateSales(Sales Sales);
        void deleteSales(int SalesID);
        IEnumerable<SaleDetails> GetSalesWithDetails();
        SaleDetails GetSaleWithDetailsById(int SaleId);

    }
}