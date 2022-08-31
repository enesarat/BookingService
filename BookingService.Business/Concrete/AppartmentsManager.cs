using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using BookingSevice.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class AppartmentsManager : IAppartmentsService
    {
        IAppartmentsDAL appartmentsAccess;
        IBookingsDAL bookingsAccess;
        public AppartmentsManager(IAppartmentsDAL appartmentsAccess, IBookingsDAL bookingsAccess)
        {
            this.appartmentsAccess = appartmentsAccess;
            this.bookingsAccess = bookingsAccess;
        }
        public async Task DeleteItem(int id)
        {
            await appartmentsAccess.DeleteItem(id);
        }

        public async Task<bool> DeleteItemWithRecordCheck(int id)
        {
            var deleteItem = appartmentsAccess.GetItemById(id);
            var isUsed = bookingsAccess.GetAllItemsByFilter(x => x.apartment_id == deleteItem.Result.id);
            if (isUsed.Count==0)
            {
                await appartmentsAccess.DeleteItem(deleteItem.Result.id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<appartments>> GetAllElement()
        {
            var appartmentsList = await appartmentsAccess.GetAllItems();
            return appartmentsList;
        }

        public List<appartments> GetAllItemsByFilter(Expression<Func<appartments, bool>> filter)
        {
            return appartmentsAccess.GetAllItemsByFilter(filter);
        }

        public async Task<appartments> GetElementById(int id)
        {
            var appartment = await appartmentsAccess.GetItemById(id);
            return appartment;
        }

        public async Task<List<appartments>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var appartmentList = await appartmentsAccess.GetElementsByPaging(pagingParameters);
            return appartmentList;
        }

        public async Task<appartments> InsertElement(appartments item)
        {
            List<appartments> appartmentList = await appartmentsAccess.GetAllItems();
            var count = appartmentList.Count;
            if (count > 0)
            {
                var lastAppartment = appartmentList[count - 1];
                item.id = lastAppartment.id + 1;
            }
            else
            {
                item.id = 0;
            }
            await appartmentsAccess.InsertItem(item);
            return item;
        }

        public async Task<appartments> UpdateElement(appartments item)
        {
            await appartmentsAccess.UpdateItem(item);
            return item;
        }
    }
}
