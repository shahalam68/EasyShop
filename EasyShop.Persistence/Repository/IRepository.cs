﻿using EasyShop.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Persistence.Repository
{
    public interface IRepository<T> where T : class
    {
       // Task<IQueryable<T>> GetProductBySearchNameAsync();
        //Task<IEnumerable<T>> GetProductsAsync(ProductFilterDto filter, PaginationDto pagination,string sortAscending, bool sortBy);
        Task<IQueryable<T>> GetProductsAsync();
    }
}
