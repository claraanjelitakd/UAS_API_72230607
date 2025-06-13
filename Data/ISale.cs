using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface ISale
    {
        IEnumerable<Sale> GetAllSales();
        Sale GetSaleById(int saleId);
        Sale AddSale(Sale sale);
        Sale UpdateSale(Sale sale);
        void DeleteSale(int saleId);
        Sale GetInvoiceById(int saleId);
        IEnumerable<Sale> GetAllOfSales();
    }
}