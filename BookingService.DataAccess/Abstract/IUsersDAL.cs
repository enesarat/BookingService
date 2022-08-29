using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingService.DataAccess.Abstract
{
    public interface IUsersDAL : IGenericEntityDAL<users>
    {
        public string GetFirstname(users user);
        public string GetLastname(users user);
        public string GetEmail(users user);
        public string GetPhoneNo(users user);

    }
}
