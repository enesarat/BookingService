using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingService.DataAccess.Abstract
{
    public interface IBookingsDAL : IGenericEntityDAL<bookings>
    {
    }
}
