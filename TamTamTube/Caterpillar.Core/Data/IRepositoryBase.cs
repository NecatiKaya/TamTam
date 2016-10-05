using System.Collections.Generic;

namespace Caterpillar.Core.Data
{
    public interface IRepositoryBase<T>
    {
        T SelectById(T entity);

        T Select(T entity);

        List<T> SelectList(T entity);

        T Save(T entity);

        int Delete(T entity);
    }
}
