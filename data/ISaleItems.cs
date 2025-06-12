using System;
using SimpleRESTApi.Models;
namespace SimpleRESTApi.Data
{
    public interface ISaleItems
    {
        //crud
        IEnumerable<SaleItems> GetSaleItems();
        SaleItems GetSaleItemsById(int SaleItemID);
        SaleItems addSaleItems(SaleItems SaleItems);
        SaleItems updateSaleItems(SaleItems SaleItems);
        void deleteSaleItems(int SaleItemID);
    }
}