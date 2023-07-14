using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VendorServiceRepository
{
    public interface IVendorRepository
    {
        public List<Vendor> GetAllVendor();
        public Vendor GetVendorById(int id);
        public Vendor GetVendorByName(string name);
    }
}
