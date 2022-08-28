using BookingService.DataAccess.Abstract;
using BookingService.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Repository
{
    public class EfGenericRepository<T> : IGenericEntityDAL<T> where T : class, IEntity, new()
    {
        public Task DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllItems()
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                var itemList = await dbContext.Set<T>().ToListAsync();
                return itemList;
            }
        }

        public Task<T> GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertItem(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateItem(T item)
        {
            throw new NotImplementedException();
        }
    }
}
