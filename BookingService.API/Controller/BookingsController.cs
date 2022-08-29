using BookingService.Business.Abstract;
using BookingService.DataAccess.Helper.Exceptions;
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
    public class BookingsController : ControllerBase
    {
        private IBookingsService manageBookings;
        public BookingsController(IBookingsService agentService)
        {
            this.manageBookings = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await manageBookings.GetAllElement()); // 200 + retrieved data 
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {            
            return Ok(await manageBookings.GetElementById(id)); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentName(int id)
        {
            var booking = manageBookings.GetElementById(id);
            string aptName = await manageBookings.GetAppartmentName(await booking);
            return Ok(aptName); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentAddress(int id)
        {
            var booking = manageBookings.GetElementById(id);
            string aptAddress = await manageBookings.GetAppartmentAddress(await booking);
            return Ok(aptAddress); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentAddressZipCode(int id)
        {
            var booking = manageBookings.GetElementById(id);
            string aptAddressZipCode = await manageBookings.GetAppartmentAddressZipCode(await booking);
            return Ok(aptAddressZipCode); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentCity(int id)
        {
            var booking = manageBookings.GetElementById(id);
            string aptCity = await manageBookings.GetAppartmentCity(await booking);
            return Ok(aptCity); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentCountry(int id)
        {
            var booking = manageBookings.GetElementById(id);
            string aptCountry = await manageBookings.GetAppartmentCountry(await booking);
            return Ok(aptCountry); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingStartDate(int id)
        {
            string startDate = await manageBookings.GetBookingStartDate(id);
            return Ok(startDate); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingEndDate(int id)
        {
            string endDate = await manageBookings.GetBookingEndDate(id);
            return Ok(endDate); // 200 + retrieved data   
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingConfirmationStatus(int id)
        {
            string confirmationStatus = await manageBookings.GetBookingConfirmationStatus(id);
            return Ok(confirmationStatus); // 200 + retrieved data   
        }
    }
}
