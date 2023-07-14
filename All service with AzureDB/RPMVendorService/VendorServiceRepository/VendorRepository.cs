using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorServiceRepository
{
    public class VendorRepository:IVendorRepository
    {
        private readonly VendorDBContext con;

        public VendorRepository()
        {
            con = new VendorDBContext();
        }
        public List<Vendor> GetAllVendor()
        {
            var list = con.Vendors.ToList();
            return list;
        }

        public Vendor GetVendorById(int id)
        {
            Vendor vendor = (from data in con.Vendors where data.Vendor_Id == id select data).FirstOrDefault();
            return vendor;
        }

        public Vendor GetVendorByName(string name)
        {
            Vendor vendor = (from data in con.Vendors where data.Vendor_Name == name select data).FirstOrDefault();
            return vendor;
        }
    }
}
