using BookingService.Business.Abstract;
using BookingService.DataAccess;
using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Helper.Calculation;
using BookingService.DataAccess.Helper.Enums;
using BookingService.Entity.Concrete;
using BookingSevice.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class BookingManager : IBookingsService
    {
        IBookingsDAL bookingsAccess;
        IAppartmentsDAL appartmentAccess;
        IUsersDAL userAccess;
        public BookingManager(IBookingsDAL bookingsAccess, IAppartmentsDAL appartmentAccess, IUsersDAL userAccess)
        {
            this.bookingsAccess = bookingsAccess;
            this.appartmentAccess = appartmentAccess;
            this.userAccess = userAccess;
        }

        public async Task DeleteItem(int id)
        {
            var deleteItem = bookingsAccess.GetItemById(id);
            var status = deleteItem.Result.confirmed;
            if (status != 1)
            {
                await bookingsAccess.DeleteItem(id);
            }

        }
        public async Task<bool> DeleteItemWithCretdention(int id)
        {
            var deleteItem = bookingsAccess.GetItemById(id);
            var status = deleteItem.Result.confirmed;
            if (status != 1)
            {
                await bookingsAccess.DeleteItem(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<bookings>> GetAllElement()
        {
            var bookingsList = await bookingsAccess.GetAllItems();
            return bookingsList;
        }

        public List<bookings> GetAllItemsByFilter(Expression<Func<bookings, bool>> filter)
        {
            return bookingsAccess.GetAllItemsByFilter(filter);
        }

        public List<bookings> GetByMultipleFilter(FilterDTO filter)
        {
            int userId = -1;
            int aptID = -1;
            List<bookings> queryResults;

            if (filter.first_name != null || filter.last_name != null)
            {
                List<users> users;
                if (filter.first_name != null)
                {
                    users = userAccess.GetAllItemsByFilter(x => x.first_name == filter.first_name);
                }
                else
                {
                    users = userAccess.GetAllItemsByFilter(x => x.last_name == filter.last_name);
                }
                
                if (users.Count == 1)
                {
                    userId = users[0].id;
                }
            }
            if (filter.apt_name != null)
            {
                List<appartments> appartments;
                appartments = appartmentAccess.GetAllItemsByFilter(x => x.name== filter.apt_name);
                if(appartments.Count == 1)
                {
                    aptID = appartments[0].id;
                }
            }
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                IEnumerable<bookings> query = dbContext.bookings;
                if (userId != -1)
                {
                    query = bookingsAccess.GetAllItemsByFilter(x => x.user_id == userId).ToList();
                }
                if (filter.starts_at != null)
                {
                    //query = query.Where(x => x.starts_at == filter.starts_at).ToList();
                    query = query.Where(x => x.starts_at.Contains(filter.starts_at)).ToList();
                }
                if (filter.end_at != null)
                {
                    var timeCalc1 = new TimeCalculator(filter.end_at);
                    int end_year = timeCalc1.year;
                    int end_month = timeCalc1.month;
                    int end_day = timeCalc1.day;

                    var timeCalc2 = new TimeCalculator(filter.starts_at);
                    int start_year = timeCalc2.year;
                    int start_month = timeCalc2.month;
                    int start_day = timeCalc2.day;

                    if (end_day == start_day)
                    {
                        if (end_month == start_month)
                        {
                            if (end_year == start_year)
                            {
                                query = query.Where(x => x.booked_for == 0).ToList();
                            }
                            else if (end_year - start_year > 0)
                            {
                                query = query.Where(x => x.booked_for == 365 * (end_year - start_year)).ToList();

                            }
                        }
                        else if(end_month - start_month > 0)
                        {
                            query = query.Where(x => x.booked_for == 30 * (end_month - start_month)).ToList();

                        }
                    }
                    else if(end_day>start_day)
                    {
                        var day_count = end_day - start_day;
                        if (end_month > start_month)
                        {
                            day_count += (end_month - start_month) * 30;
                        }
                        else if (end_month < start_month)
                        {
                            if (end_year > start_year)
                            {
                                day_count += (end_year - start_year) * 365;
                            }
                        }
                        else if (end_year > start_year)
                        {
                            day_count += (end_year - start_year) * 365;
                        }
                        query = query.Where(x => x.booked_for == day_count).ToList();

                    }
                    else
                    {
                        var day_count = (30-start_day) + end_day;
                        if (end_month > start_month)
                        {
                            day_count += (end_month - start_month -1) * 30;
                        }
                        else if (end_month < start_month)
                        {
                            if (end_year > start_year)
                            {
                                day_count += (end_year - start_year) * 365;
                            }
                        
                        }
                        else if (end_year > start_year)
                        {

                            day_count += (end_year - start_year) * 365;
                        }
                        query = query.Where(x => x.booked_for == day_count).ToList();

                    }
                }
                if (aptID != -1)
                {
                    query = query.Where(x => x.apartment_id == aptID).ToList();

                }
                if (filter.confirmed != null)
                {
                    if (Convert.ToInt32(filter.confirmed) == 1)
                    {
                        query = query.Where(x => x.confirmed == Convert.ToInt32(filter.confirmed)).ToList();
                    }
                    else
                    {
                        query = query.Where(x => x.confirmed == Convert.ToInt32(filter.confirmed)).ToList();

                    }
                }
                queryResults = query.ToList();
            }

             
            return queryResults;
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
