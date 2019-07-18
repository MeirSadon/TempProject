using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public interface IBasicDB<T> where T : IPoco
    {
        void Add(T t);
        T GetById(int id);
        IList<T> GetAll();
        void Remove(T t);
        void Update(T t);
    }
}