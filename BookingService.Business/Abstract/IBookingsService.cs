using BookingService.Entity.Concrete;
using BookingSevice.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IBookingsService:IGenericService<bookings>
    {
        public Task<string> GetAppartmentName(bookings item);
        public Task<string> GetAppartmentAddress(bookings item);
        public Task<string> GetAppartmentAddressZipCode(bookings item);
        public Task<string> GetAppartmentCity(bookings item);
        public Task<string> GetAppartmentCountry(bookings item);
        public Task<string> GetBookingStartDate(int id);
        public Task<string> GetBookingEndDate(int id);
        public Task<string> GetBookingConfirmationStatus(int id);
        public Task<bool> DeleteItemWithCretdention(int id);
        public List<bookings> GetByMultipleFilter(FilterDTO filter);
    }
}
