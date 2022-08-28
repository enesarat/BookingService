using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class BookingManager : IBookingsService
    {
        IBookingsDAL bookingsAccess;
        public BookingManager(IBookingsDAL bookingsAccess)
        {
            this.bookingsAccess = bookingsAccess;
        }
        public async Task<List<bookings>> GetAllElement()
        {
            var bookingsList = await bookingsAccess.GetAllItems();
            return bookingsList;
        }

        public Task<bookings> GetElementById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bookings> InsertElement(bookings item)
        {
            throw new NotImplementedException();
        }

        public Task<bookings> UpdateElement(bookings item)
        {
            throw new NotImplementedException();
        }
    }
}
