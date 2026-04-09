using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Interface
{
   public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool>UpdateAsync(UpdateProductDTO productDTO);
        Task DeleteAsync(Product product);

    }
}
