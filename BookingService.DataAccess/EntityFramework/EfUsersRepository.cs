using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Repository;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingService.DataAccess.EntityFramework
{
    public class EfUsersRepository : EfGenericRepository<users>, IUsersDAL
    {
        public string GetEmail(users user)
        {
            var email = user.email;
            return email;
        }

        public string GetFirstname(users user)
        {
            var first_name = user.first_name;
            return first_name;
        }

        public string GetLastname(users user)
        {
            var last_name = user.last_name;
            return last_name;
        }

        public string GetPhoneNo(users user)
        {
            var phone = user.phone;
            return phone;
        }
    }
}
