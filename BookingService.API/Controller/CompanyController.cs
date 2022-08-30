using BookingService.Business.Abstract;
using BookingService.DataAccess.Helper.Exceptions;
using BookingService.Entity.Concrete;
using BookingSevice.Entity.Concrete.DTO;
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

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await manageCompany.GetElementsByPaging(pagingParameters)); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] company company)
        {
            if (ModelState.IsValid)
            {
                var newCompany = await manageCompany.InsertElement(company);
                return CreatedAtAction("Get", new { companyId = newCompany.id }, newCompany); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
        //-------------------------------------------------------Post Requests Ends------------------------------------------//

        //-------------------------------------------------------Put Requests Starts------------------------------------------//
        [HttpPut]
        [ExceptionFilter]

        public async Task<IActionResult> Put([FromBody] company oldCompany)
        {
            if (await manageCompany.GetElementById(oldCompany.id) != null)
            {
                return Ok(await manageCompany.UpdateElement(oldCompany)); // 200 + data
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Put Requests Ends------------------------------------------//

        //-------------------------------------------------------Delete Requests Starts------------------------------------------//
        [HttpDelete]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteCompanyById(int id)
        {
            if (await manageCompany.GetElementById(id) != null)
            {
                await manageCompany.DeleteItem(id);
                return Ok(); // 200
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
