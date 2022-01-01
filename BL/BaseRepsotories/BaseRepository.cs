using e_c_Project.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace e_c_Project.BL.BaseRepsotories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext con)
        {
            _context = con;
        }

        public async Task<bool> Add(T data)
        {
            try
            {
                await _context.Set<T>().AddAsync(data);
                await _context.SaveChangesAsync();
                var i = data;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            

        }

        public async Task<bool> AddRange(IEnumerable<T> datas)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(datas);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T> GetByID(ApplicationDbContext dbContext,string id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }


        public async Task<T> GetByID(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByID(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByID(Expression<Func<T,bool>> match,string[] includes)
        {
            IQueryable<T> data = _context.Set<T>();
            if(includes != null)
                foreach(var include in includes)
                {
                    data = data.Include(include);
                }
            
            return await data.SingleOrDefaultAsync(match);
        }




        


    }
}
