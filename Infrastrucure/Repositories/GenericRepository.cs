using DAL.Context;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmiratesContext _db;
        public GenericRepository(EmiratesContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int customerId)
        {

            var customer = await _db.Set<T>().FindAsync(customerId);
            _db.Set<T>().Remove(customer);
            await _db.SaveChangesAsync();
        }

    }
}
