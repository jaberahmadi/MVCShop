using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface IResellerRepository
    {
        bool Add(Reseller entity, bool autoSave = true);
        bool Update(Reseller entity, bool autoSave = true);
        bool Delete(Reseller entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        Reseller Find(int id);
        IQueryable<Reseller> Where(Expression<Func<Reseller, bool>> predicate);
        IQueryable<Reseller> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Reseller, TResult>> selector);
        int GetLastIdentity();
        int Save();
    }
}
