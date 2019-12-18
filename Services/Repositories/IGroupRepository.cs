using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface IGroupRepository
    {

        bool Add(Group entity, bool autoSave = true);
        bool Update(Group entity, bool autoSave = true);
        bool Delete(Group entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        Group Find(int id);
        IQueryable<Group> Where(Expression<Func<Group, bool>> predicate);
        IQueryable<Group> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Group, TResult>> selector);
        int GetLastIdentity();
        int Save();
    }
}
