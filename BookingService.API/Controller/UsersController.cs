using BookingService.Business.Abstract;
using BookingService.DataAccess.Helper.Exceptions;
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
    public class UsersController : ControllerBase
    {
        IUsersService manageUsers;
        public UsersController(IUsersService usersService)
        {
            manageUsers = usersService;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await manageUsers.GetAllElement()); // 200 + retrieved data 
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetFistName(int id)
        {
            var user = manageUsers.GetElementById(id);
            var fistname =  manageUsers.GetUserFirstName(await user);
            return Ok(fistname); // 200 + retrieved data 
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetLastName(int id)
        {
            var user = manageUsers.GetElementById(id);
            var lastname = manageUsers.GetUserLastName(await user);
            return Ok(lastname); // 200 + retrieved data 
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetEmail(int id)
        {
            var user = manageUsers.GetElementById(id);
            var email = manageUsers.GetUserEmail(await user);
            return Ok(email); // 200 + retrieved data 
        }

        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetPhoneNo(int id)
        {
            var user = manageUsers.GetElementById(id);
            var phoneno = manageUsers.GetUserPhoneNo(await user);
            return Ok(phoneno); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] users user)
        {
            if (ModelState.IsValid)
            {
                var newUser = await manageUsers.InsertElement(user);
                return CreatedAtAction("Get", new { userId = newUser.id }, newUser); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
        //-------------------------------------------------------Post Requests Ends------------------------------------------//

        //-------------------------------------------------------Put Requests Starts------------------------------------------//
        [HttpPut]
        [ExceptionFilter]

        public async Task<IActionResult> Put([FromBody] users oldUser)
        {
            if (await manageUsers.GetElementById(oldUser.id) != null)
            {
                return Ok(await manageUsers.UpdateElement(oldUser)); // 200 + data
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Put Requests Ends------------------------------------------//

        //-------------------------------------------------------Delete Requests Starts------------------------------------------//
        [HttpDelete]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            if (await manageUsers.GetElementById(id) != null)
            {
                await manageUsers.DeleteItem(id);
                return Ok(); // 200
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
