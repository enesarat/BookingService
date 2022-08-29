using BookingService.Business.Abstract;
using BookingService.Entity.Concrete;
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
    public class AppartmentsController : ControllerBase
    {
        IAppartmentsService manageAppartments;
        public AppartmentsController(IAppartmentsService appartmentService)
        {
            manageAppartments = appartmentService;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await manageAppartments.GetAllElement()); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] appartments appartment)
        {
            if (ModelState.IsValid)
            {
                var newAppartment = await manageAppartments.InsertElement(appartment);
                return CreatedAtAction("Get", new { appartmentId = newAppartment.id }, newAppartment); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
    }
}
