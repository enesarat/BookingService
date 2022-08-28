using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Repository;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingService.DataAccess.EntityFramework
{
    public class EfBookingsRepository:EfGenericRepository<bookings>,IBookingsDAL
    {
    }
}
