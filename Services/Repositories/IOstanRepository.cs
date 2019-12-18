using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface IOstanRepository
    {
        bool Add(Ostan entity, bool autoSave = true);
        bool Update(Ostan entity, bool autoSave = true);
        bool Delete(Ostan entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        Ostan Find(int id);
        IQueryable<Ostan> Where(Expression<Func<Ostan, bool>> predicate);
        IQueryable<Ostan> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Ostan, TResult>> selector);
        int GetLastIdentity();
        int Save();


    }
}
