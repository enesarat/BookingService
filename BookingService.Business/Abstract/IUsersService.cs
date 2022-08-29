using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IUsersService : IGenericService<users>
    {
        public Task<string> GetUserFirstName(users item);
        public Task<string> GetUserLastName(users item);
        public Task<string> GetUserEmail(users item);
        public Task<string> GetUserPhoneNo(users item);
    }
}
