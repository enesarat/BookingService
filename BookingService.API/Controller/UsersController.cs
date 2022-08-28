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
    public class UsersController : ControllerBase
    {
        IUsersService manageUsers;
        public UsersController(IUsersService usersService)
        {
            manageUsers = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await manageUsers.GetAllElement()); // 200 + retrieved data 
        }
    }
}
