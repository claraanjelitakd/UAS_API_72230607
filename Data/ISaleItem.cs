using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface ISaleItem
    {
        IEnumerable<SaleItems> GetAllSaleItems();
        SaleItems GetSaleItemById(int saleItemId);
        SaleItems AddSaleItem(SaleItems saleItem);
        SaleItems UpdateSaleItem(SaleItems saleItem);
        void DeleteSaleItem(int saleItemId);
    }
}