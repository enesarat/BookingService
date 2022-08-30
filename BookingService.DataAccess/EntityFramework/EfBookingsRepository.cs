using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Repository;
using BookingService.Entity.Concrete;
using BookingSevice.Entity.Concrete.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.EntityFramework
{
    public class EfBookingsRepository : EfGenericRepository<bookings>, IBookingsDAL
    {
       
    }
}
