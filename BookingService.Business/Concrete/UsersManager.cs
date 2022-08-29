using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class UsersManager : IUsersService
    {
        IUsersDAL usersAccess;
        public UsersManager(IUsersDAL usersAccess)
        {
            this.usersAccess = usersAccess;
        }
        public async Task DeleteItem(int id)
        {
            await usersAccess.DeleteItem(id);
        }

        public async Task<List<users>> GetAllElement()
        {
            var usersList = await usersAccess.GetAllItems();
            return usersList;
        }

        public async Task<users> GetElementById(int id)
        {
            var user = await usersAccess.GetItemById(id);
            return user;
        }

        public async Task<string> GetUserEmail(users item)
        {
            string email = item.email;
            return email;
        }

        public async Task<string> GetUserFirstName(users item)
        {
            string firstName = item.first_name;
            return firstName;
        }

        public async Task<string> GetUserLastName(users item)
        {
            string lastName = item.last_name;
            return lastName;
        }

        public async Task<string> GetUserPhoneNo(users item)
        {
            string phoneNo = item.phone;
            return phoneNo;
        }

        public async Task<users> InsertElement(users item)
        {
            List<users> userLsit = await usersAccess.GetAllItems();
            var count = userLsit.Count;
            if (count > 0)
            {
                var lastUser = userLsit[count - 1];
                item.id = lastUser.id + 1;
            }
            else
            {
                item.id = 0;
            }
            await usersAccess.InsertItem(item);
            return item;
        }

        public async Task<users> UpdateElement(users item)
        {
            await usersAccess.UpdateItem(item);
            return item;
        }

    }
}
