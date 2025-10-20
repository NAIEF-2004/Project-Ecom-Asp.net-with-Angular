using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Interface
{
   public interface IGenricRepository< T> where T : class

    {
       Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression <Func<T,object>>[]includs);
        Task<T>GetByIdAsync(int id );
        Task<T> GetByIdAsync(int id,params Expression<Func<T, object>>[]includs);
        Task AddAsync( T item );
        Task DeleteAsync( int id );
        Task UpdateAsync( T item );

    }
}
