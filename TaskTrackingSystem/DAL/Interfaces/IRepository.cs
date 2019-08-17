namespace TaskTrackingSystem.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        T Create(T item);

        T Update(T item);

        T Delete(int id);

        IEnumerable<T> Filter(Func<T, bool> predicate);
    }
}
