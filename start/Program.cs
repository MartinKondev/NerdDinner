//page 67 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner;
using NerdDinner.Models;

namespace start
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new DinnerRepository();
            var d = repository.FindUpcommingDinners().ToList();
            for (int i = 0; i < d.Count(); i++)
            {
                d[i].HostedBy = "NerdyMan";
            }
            repository.Save();
        }
    }
}
