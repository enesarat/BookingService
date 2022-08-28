using BookingService.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IGenericService<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllElement();
        Task<T> GetElementById(int id);
        Task<T> InsertElement(T item);
        Task<T> UpdateElement(T item);
    }
}
