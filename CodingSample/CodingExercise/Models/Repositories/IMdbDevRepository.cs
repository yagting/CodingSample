using System;
using System.Linq;

namespace CodingExercise.Models.Repositories
{
    public interface IMdbRepository<T>
    {
        IQueryable<T> Find(Func<T, bool> predicate);
        void Update(T entity);
    }
}
