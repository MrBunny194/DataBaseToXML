using Data.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Data.Repository
{
    public class YearsRepository : IRepository<Year>
    {
        private Context _db;

        public YearsRepository(Context context)
        {
            _db = context;
        }
        
        public List<Year> GetAllData() => _db.Years.ToList();

        public List<Year> GetYears() => _db.Years.ToList();
        
        public Year Get(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return _db.Years.Find(id);
        }

        public bool Delete(int? id)
        {
            var car = Get(id);

            if (car == null)
            {
                return false;
            }
            else
            {
                _db.Years.Remove(Get(id));
                _db.SaveChanges();

                return true;
            }
        }
       
        public void Add(int year)
        {
            _db.Years.Add(new Year { Date = year });
            _db.SaveChanges();
        }

        public void Add(string model, string name, int id)
        {
            throw new NotImplementedException();
        }
    }
}
