using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        ICompanyDAL companyAccess;
        public CompanyManager(ICompanyDAL companyAccess)
        {
            this.companyAccess = companyAccess;
        }
        public async Task DeleteItem(int id)
        {
            await companyAccess.DeleteItem(id);
        }

        public async Task<List<company>> GetAllElement()
        {
            var companyList = await companyAccess.GetAllItems();
            return companyList;
        }

        public List<company> GetAllItemsByFilter(Expression<Func<company, bool>> filter)
        {
            return companyAccess.GetAllItemsByFilter(filter);
        }

        public async Task<company> GetElementById(int id)
        {
            var company = await companyAccess.GetItemById(id);
            return company;
        }

        public async Task<company> InsertElement(company item)
        {
            List<company> companyList = await companyAccess.GetAllItems();
            var count = companyList.Count;
            if (count > 0 || companyList!=null)
            {
                var lastCompany = companyList[count - 1];
                item.id = lastCompany.id + 1;
            }
            else
            {
                item.id = 0;
            }
            await companyAccess.InsertItem(item);
            return item;
        }

        public async Task<company> UpdateElement(company item)
        {
            await companyAccess.UpdateItem(item);
            return item;
        }
    }
}
