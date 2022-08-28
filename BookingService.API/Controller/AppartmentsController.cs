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
    public class AppartmentsController : ControllerBase
    {
        IAppartmentsService manageAppartments;
        public AppartmentsController(IAppartmentsService appartmentService)
        {
            manageAppartments = appartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await manageAppartments.GetAllElement()); // 200 + retrieved data 
        }
    }
}
