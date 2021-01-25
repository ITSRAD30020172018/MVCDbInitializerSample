using ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClubContext
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ClubsContext db = new ClubsContext())
            {
                Console.WriteLine("No of Clubs {0}", db.Clubs.Count());
            }
        }
    }
}
