using e_c_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace e_c_Project.BL
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T data);
        Task<bool> AddRange(IEnumerable<T> datas);
        Task<T> GetByID(Expression<Func<T, bool>> match, string[] includes);
        Task<T> GetByID(int id);
        Task<T> GetByID(string id);
    }
}
