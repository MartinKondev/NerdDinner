using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Models;

namespace NerdDinner.Tests.Fakes
{
    class FakeDinnerRepository : IDinnerRepository
    {
        private List<Dinner> Dinners;
        public FakeDinnerRepository(List<Dinner> dinners)
        {
            Dinners = dinners;
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return Dinners.AsQueryable();
        }

        public IQueryable<Dinner> FindUpcommingDinners()
        {
            var dinners = from dinner in Dinners
                          where dinner.EventDate > DateTime.Now
                          select dinner;
            return dinners.AsQueryable();
        }

        public Dinner GetDinnerByID(int id)
        {
            return Dinners.SingleOrDefault(x => x.DinnerId == id);
        }

        public void Add(Dinner dinner)
        {
            Dinners.Add(dinner);    
        }

        public void Delete(Dinner dinner)
        {
            Dinners.Remove(dinner);
        }

        public void Save()
        {
            foreach (var dinner in Dinners)
            {
                if (!dinner.IsValid)
                {
                    throw new ApplicationException("Rule violations");
                }
            }
        }
    }
}
