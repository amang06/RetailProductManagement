using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorServiceRepository
{
    public interface IVendorStockRepository
    {
        //Read
        public int GetStockByProductId(int productId);
        public Vendor_Stock GetStockByProductAndVendorId(int productId, int vendorId);
        public void UpdateStockByProductAndVendorId(int productId, int vendorId, int qty);
        public DateTime GetStockReplenishmentDateByProductId(int productId);
    }
}
