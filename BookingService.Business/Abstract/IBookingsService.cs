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
        public Task<BookingInfoListDTO> GetBookingInfoById(int id);
        public Task<string> GetUserFirstName(int id);
        public Task<string> GetUserLastName(int id);
        public Task<string> GetUserEmail(int id);
        public Task<string> GetUserPhoneNumber(int id);
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
