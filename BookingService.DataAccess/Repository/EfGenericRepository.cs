using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Helper.Exceptions;
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

        public async Task<T> GetItemById(int id)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                var item = await dbContext.Set<T>().FindAsync(id);
                if (item == null)
                    throw new DataNotFoundException(nameof(item), id);
                return item;
            }
        }

        public async Task<T> InsertItem(T item)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                await dbContext.Set<T>().AddAsync(item);
                await dbContext.SaveChangesAsync();
                return item;
            }
        }

        public async Task<T> UpdateItem(T item)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                dbContext.Set<T>().Update(item);
                await dbContext.SaveChangesAsync();
                return item;
            }
        }
    }
}
