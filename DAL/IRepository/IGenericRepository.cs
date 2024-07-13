using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
       Task<IEnumerable<T>> GetAll();
       Task<T> AddAsync(T entity);
       Task DeleteAsync(int customerId);

    }
}
