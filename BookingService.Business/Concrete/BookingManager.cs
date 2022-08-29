using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Helper.Calculation;
using BookingService.DataAccess.Helper.Enums;
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
        IAppartmentsDAL appartmentAccess;
        public BookingManager(IBookingsDAL bookingsAccess, IAppartmentsDAL appartmentAccess)
        {
            this.bookingsAccess = bookingsAccess;
            this.appartmentAccess = appartmentAccess;
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

        public async Task<string> GetAppartmentAddress(bookings item)
        {
            var aptId = item.apartment_id;
            List<appartments> appartmentsList = await appartmentAccess.GetAllItems();
            var aptAddress = appartmentsList.Find(x => x.id == aptId).address.ToString();
            return aptAddress;
        }

        public async Task<string> GetAppartmentAddressZipCode(bookings item)
        {
            var aptId = item.apartment_id;
            List<appartments> appartmentsList = await appartmentAccess.GetAllItems();
            var aptAddressZipCode = appartmentsList.Find(x => x.id == aptId).zip_code.ToString();
            return aptAddressZipCode;
        }

        public async Task<string> GetAppartmentCity(bookings item)
        {
            var aptId = item.apartment_id;
            List<appartments> appartmentsList = await appartmentAccess.GetAllItems();
            var aptCity = appartmentsList.Find(x => x.id == aptId).city.ToString();
            return aptCity;
        }

        public async Task<string> GetAppartmentCountry(bookings item)
        {
            var aptId = item.apartment_id;
            List<appartments> appartmentsList = await appartmentAccess.GetAllItems();
            var aptCountry = appartmentsList.Find(x => x.id == aptId).country.ToString();
            return aptCountry;
        }

        public async Task<string> GetAppartmentName(bookings item)
        {
            var aptId = item.apartment_id;
            List<appartments> appartmentsList  = await appartmentAccess.GetAllItems();
            var aptName = appartmentsList.Find(x=>x.id == aptId).name.ToString();
            return aptName;
        }

        public async Task<string> GetBookingConfirmationStatus(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            int confirmationStatus = booking.confirmed;
            string statusAsString;
            if (confirmationStatus == 1)
            {
                statusAsString = Enum.GetName(typeof(ConfirmationStatus), ConfirmationStatus.onaylı);
            }
            else
            {
                statusAsString = Enum.GetName(typeof(ConfirmationStatus), ConfirmationStatus.onaylanmamış);
            }
            return statusAsString;
        }

        public async Task<string> GetBookingEndDate(int id)
        {
            string bookingEndDate;
            int currYear, currMonth, finalDay;
            var booking = await bookingsAccess.GetItemById(id);
            string bookingStartDate = booking.starts_at;
            var timeCalc = new TimeCalculator(bookingStartDate);
            var dayCount = Convert.ToInt32(booking.booked_for);

            var startDay = timeCalc.day;
            finalDay = startDay + dayCount;
            if (finalDay > 30) 
            {
                finalDay = finalDay % 30;
                currMonth = timeCalc.month;
                currMonth += 1;
                if (currMonth > 12)
                {
                    currMonth = currMonth % 12;
                    currYear = timeCalc.year;
                    currYear += 1;
                }
                else 
                {
                    currYear = timeCalc.year;
                }


                if (finalDay >= 10)
                {
                    bookingEndDate = String.Format("{0}-{1}-{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
                else
                {
                    bookingEndDate = String.Format("{0}-{1}-0{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
            }
            else
            {
                currMonth = timeCalc.month;
                currYear = timeCalc.year;
                if (finalDay >= 10)
                {
                    bookingEndDate = String.Format("{0}-{1}-{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
                else
                {
                    bookingEndDate = String.Format("{0}-{1}-0{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
            }
         
            return bookingEndDate;
        }

        public async Task<string> GetBookingStartDate(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            string bookingStartDate = booking.starts_at;
            return bookingStartDate;
        }

        public async Task<bookings> GetElementById(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            return booking;
        }

        public async Task<bookings> InsertElement(bookings item)
        {
            List<bookings> bookingList = await bookingsAccess.GetAllItems();
            var count = bookingList.Count;
            if (count > 0)
            {
                var lastBooking = bookingList[count - 1];
                item.id = lastBooking.id + 1;
            }
            else
            {
                item.id = 0;
            }
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
