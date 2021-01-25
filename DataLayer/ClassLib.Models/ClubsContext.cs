
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Models
{
    public class ClubsContext : DbContext
    {

        public ClubsContext()
            : base(nameOrConnectionString: "DefaultConnection")
        {
            Database.SetInitializer(new ClubInitializer());
            Database.Initialize(true);
        }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<ClubEvent> ClubEvents { get; set; }

        public DbSet<EventAttendnace> EventAttendances { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Student> Students { get; set; }
        
       

    }
}
