using BookingService.Entity.Concrete;
using BookingSevice.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Abstract
{
    public interface IBookingsDAL : IGenericEntityDAL<bookings>
    {
    }
}
