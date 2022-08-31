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
    public class UsersManager : IUsersService
    {
        IUsersDAL usersAccess;
        IBookingsDAL bookingsAccess;
        public UsersManager(IUsersDAL usersAccess, IBookingsDAL bookingsAccess)
        {
            this.usersAccess = usersAccess;
            this.bookingsAccess = bookingsAccess;
        }
        public async Task DeleteItem(int id)
        {
            await usersAccess.DeleteItem(id);
        }

        public async Task<bool> DeleteItemWithRecordCheck(int id)
        {
            var deleteItem = usersAccess.GetItemById(id);
            var isUsed = bookingsAccess.GetAllItemsByFilter(x => x.user_id == deleteItem.Result.id);
            if (isUsed.Count == 0)
            {
                await usersAccess.DeleteItem(deleteItem.Result.id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<users>> GetAllElement()
        {
            var usersList = await usersAccess.GetAllItems();
            return usersList;
        }

        public List<users> GetAllItemsByFilter(Expression<Func<users, bool>> filter)
        {
            return usersAccess.GetAllItemsByFilter(filter);
        }

        public async Task<users> GetElementById(int id)
        {
            var user = await usersAccess.GetItemById(id);
            return user;
        }

        public async Task<List<users>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var userList = await usersAccess.GetElementsByPaging(pagingParameters);
            return userList;
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
