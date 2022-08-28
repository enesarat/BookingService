using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class AppartmentsManager : IAppartmentsService
    {
        IAppartmentsDAL appartmentsAccess;
        public AppartmentsManager(IAppartmentsDAL appartmentsAccess)
        {
            this.appartmentsAccess = appartmentsAccess;
        }
        public async Task DeleteItem(int id)
        {
            await appartmentsAccess.DeleteItem(id);
        }

        public async Task<List<appartments>> GetAllElement()
        {
            var appartmentsList = await appartmentsAccess.GetAllItems();
            return appartmentsList;
        }

        public async Task<appartments> GetElementById(int id)
        {
            var appartment = await appartmentsAccess.GetItemById(id);
            return appartment;
        }

        public async Task<appartments> InsertElement(appartments item)
        {
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
