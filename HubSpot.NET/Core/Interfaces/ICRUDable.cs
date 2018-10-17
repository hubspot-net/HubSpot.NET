using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface ICRUDable
    {
        void Delete(long id);
    }

    public interface ICRUDable<T> : ICRUDable
    {
        T Create(T entity);
        T GetById(long id);
        T Update(T entity);
    }
}
