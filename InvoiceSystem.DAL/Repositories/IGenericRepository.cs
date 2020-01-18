using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Repositories
{
    interface IGenericRepository<T>: IDisposable
    {
        List<T> GetAll();
        T GetById(int? id);
        void InsertEntity(T entity);
        void UpdateEntity(T entity);
        void Save();

    }
}
