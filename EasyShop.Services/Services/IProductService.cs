using EasyShop.DTO;
using EasyShop.Persistence.Models;
using EasyShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Services.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByName(string name);
        Task<List<ResponseDTO>> GetAllProduct(FilterDto filter,PaginationDto pagination,string sortBy,bool sortAscending);
    }
}
