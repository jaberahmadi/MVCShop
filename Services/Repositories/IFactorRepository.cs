using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface IFactorRepository
    {

        bool Add(Factor entity, bool autoSave = true);
        bool Update(Factor entity, bool autoSave = true);
        bool Delete(Factor entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        Factor Find(int id);
        IQueryable<Factor> Where(Expression<Func<Factor, bool>> predicate);
        IQueryable<Factor> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Factor, TResult>> selector);
        int GetLastIdentity();
        int Save();
    }
}
