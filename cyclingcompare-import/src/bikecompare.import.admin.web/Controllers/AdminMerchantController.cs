using AutoMapper;
using bikecompare.import.admin.web.Domains.Merchants;
using bikecompare.import.admin.web.Domains.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Controllers
{
    [Route("Merchants")]
    [ApiController]
    [Authorize]
    public class AdminMerchantController : ControllerBase
    {
        private readonly MerchantService _merchantService;
        private readonly IMapper _mapper;

        public AdminMerchantController(MerchantService merchantService, IMapper mapper)
        {
            _merchantService = merchantService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllMerchants")]
        public async Task<List<Merchant>> GetAllMerchants()
        {
            var merchants = await _merchantService.GetMerchants();
            return merchants;
        }
    }
}
