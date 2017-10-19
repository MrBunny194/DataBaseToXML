using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository<T>
    {
        T Get(int? id);
        List<T> GetAllData();
        List<Year> GetYears();
        bool Delete(int? id);
        void Add(int year);
        void Add(string model, string name, int id);
    }
}