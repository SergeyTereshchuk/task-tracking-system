namespace TaskTrackingSystem.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();

        T Get(int id);

        T Create(T item);

        T Update(T item);

        T Delete(int id);

        IEnumerable<T> Filter(Func<T, bool> predicate);
    }
}
