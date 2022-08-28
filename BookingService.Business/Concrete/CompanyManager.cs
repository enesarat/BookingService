using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
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

        public async Task<company> GetElementById(int id)
        {
            var company = await companyAccess.GetItemById(id);
            return company;
        }

        public async Task<company> InsertElement(company item)
        {
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
