using EasyShop.Persistence.Data;
using EasyShop.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EasyShopDataContext _dbContext;

        public Repository(EasyShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<IEnumerable> GetProductBySearchNameAsync(string searchEngineFriendlyName)
        //{
        //    return _dbContext.Set<T>().AsQueryable();
        //}
        public async Task<IQueryable<T>> GetProductsAsync()
        {
            return _dbContext.Set<T>().AsQueryable();
            


        }
       
    }
}
