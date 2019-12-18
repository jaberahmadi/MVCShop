using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface ICityRepository
    {
        bool Add(City entity, bool autoSave = true);
        bool Update(City entity, bool autoSave = true);
        bool Delete(City entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        City Find(int id);
        IQueryable<City> Where(Expression<Func<City, bool>> predicate);
        IQueryable<City> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<City, TResult>> selector);
        int GetLastIdentity();
        int Save();

    }
}
