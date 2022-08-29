using System;
using System.Collections.Generic;
using System.Text;

namespace BookingService.DataAccess.Helper.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string type, object id)
        : base($"{id} id değerine sahip, {type} tipinde olan herhangi bir obje bulunamadı! ") { }

    }
}
