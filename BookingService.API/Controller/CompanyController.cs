using BookingService.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyService manageCompany;
        public CompanyController(ICompanyService companyService)
        {
            manageCompany = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await manageCompany.GetAllElement()); // 200 + retrieved data 
        }
    }
}
