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

        public async Task DeleteItem(int id)
        {
            await bookingsAccess.DeleteItem(id);
        }

        public async Task<List<bookings>> GetAllElement()
        {
            var bookingsList = await bookingsAccess.GetAllItems();
            return bookingsList;
        }

        public async Task<bookings> GetElementById(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            return booking;
        }

        public async Task<bookings> InsertElement(bookings item)
        {
            await bookingsAccess.InsertItem(item);
            return item;
        }

        public async Task<bookings> UpdateElement(bookings item)
        {
            await bookingsAccess.UpdateItem(item);
            return item;
        }
    }
}
