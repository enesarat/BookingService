using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IAppartmentsService : IGenericService<appartments>
    {
        public Task<bool> DeleteItemWithRecordCheck(int id);

    }
}
