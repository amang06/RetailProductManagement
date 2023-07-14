using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorServiceRepository
{
    public class VendorStockRepository:IVendorStockRepository
    {
        private readonly VendorDBContext con;
        public VendorStockRepository()
        {
            con = new VendorDBContext();
        }
        public Vendor_Stock GetStockByProductAndVendorId(int productId, int vendorId)
        {
            var query = (from data in con.Vendor_Stocks where data.Product_Id == productId && data.Vendor_Id == vendorId select data).FirstOrDefault();
            return query;
        }

        public int GetStockByProductId(int productId)
        {
            var query = (from data in con.Vendor_Stocks where data.Product_Id == productId select data).FirstOrDefault();
            return (int)query.Stock_In_Hand;
        }
        public DateTime GetStockReplenishmentDateByProductId(int productId)
        {
            var query = (from data in con.Vendor_Stocks where data.Product_Id == productId select data).FirstOrDefault();
            return (DateTime)query.Expected_StockReplenish_Date;
        }

        public void UpdateStockByProductAndVendorId(int productId, int vendorId, int qty)
        {
            var query = (from data in con.Vendor_Stocks where data.Product_Id == productId && data.Vendor_Id == vendorId select data).FirstOrDefault();
            query.Stock_In_Hand -= qty;
            con.SaveChanges();
        }
    }
}
