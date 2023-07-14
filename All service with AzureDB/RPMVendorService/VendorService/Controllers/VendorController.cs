using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorServiceRepository;

namespace VendorService.Controllers
{
    [Route("[controller]/[action]/")]
    [ApiController]
    [Authorize]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IVendorStockRepository _vendorStockRepository;
        public VendorController()
        {
            _vendorRepository = new VendorRepository();
            _vendorStockRepository = new VendorStockRepository();

        }

        [HttpGet]
        public IEnumerable<Vendor> GetAllVendor()       //Return all vendor details
        {
            return _vendorRepository.GetAllVendor();
        }

        [HttpGet]
        public Vendor GetVendorById(int id)             //Return vendor details by id
        {
            return _vendorRepository.GetVendorById(id);
        }

        [HttpGet]
        public Vendor GetVendorByName(string name)      //Return vendor details by name
        {
            return _vendorRepository.GetVendorByName(name);
        }

        //----------------------------------------------------------------------------------------------------------------

        [HttpGet]
        
        public Vendor_Stock GetStockByProductAndVendorId(int productId, int vendorId)       //Return stock of product of particular vendor
        {
            return _vendorStockRepository.GetStockByProductAndVendorId(productId, vendorId);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{productId}")]
        public int GetStockByProductId(int productId)          //Return total stock of product
        {
            return _vendorStockRepository.GetStockByProductId(productId);
        }
        [HttpGet]
        [Route("{productId}")]
        [AllowAnonymous]
        public DateTime GetStockReplenishmentDateByProductId(int productId)          //Return stock replenishment date of product
        {
            return _vendorStockRepository.GetStockReplenishmentDateByProductId(productId);
        }

    }
}
