using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public interface IDinnerRepository
    {
        IQueryable<Dinner> FindAllDinners();
        IQueryable<Dinner> FindUpcommingDinners();
        Dinner GetDinnerByID(int id);
        void Add(Dinner dinner);
        void Delete(Dinner dinner);
        void Save();
    }

    public class DinnerRepository : IDinnerRepository
    {
        private NerdDinnerDataContext db = new NerdDinnerDataContext();

        //Query
        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }
        public IQueryable<Dinner> FindUpcommingDinners()
        {
            return from dinner in db.Dinners
                   where dinner.EventDate > DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }
        public Dinner GetDinnerByID(int id)
        {
            return db.Dinners.SingleOrDefault(x => x.DinnerId == id);
        }

        //INsert/Delete
        public void Add(Dinner dinner)
        {
             db.Dinners.InsertOnSubmit(dinner);
        }

        public void Delete(Dinner dinner)
        {
            db.RSVPs.DeleteAllOnSubmit(dinner.RSVPs);
            db.Dinners.DeleteOnSubmit(dinner);
        }

        //Persistence
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}