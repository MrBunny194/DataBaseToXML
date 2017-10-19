using Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace Data.Repository
{
    public class CarsRepository : IRepository<Car>
    {
        private Context _db;

        public CarsRepository(Context context)
        {
            _db = context;
        }

        public List<Car> GetAllData() => _db.Cars.Include(y => y.Year).ToList();

        public List<Year> GetYears() => _db.Years.ToList();

        public Car Get(int? id)
        {
           if (id == null)
           {
                return null;
           }
           return _db.Cars.Find(id);
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
                _db.Cars.Remove(car);
                _db.SaveChanges();

                return true;
            }
        }

        public void Add(string model, string name, int id)
        {
            _db.Cars.Add(new Car { Model = model, Name = name, YearId = id });
            _db.SaveChanges();
        }

        public void Add(int year)
        {
            throw new NotImplementedException();
        }
    }
}
