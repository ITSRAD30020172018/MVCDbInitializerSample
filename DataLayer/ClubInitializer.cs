using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ClassLib.Models
{
    public class ClubInitializer : DropCreateDatabaseIfModelChanges<ClubsContext>
    {
        public ClubInitializer()
        {}
        protected override void Seed(ClubsContext context)
        {
            Seed_students(context);
            context.Clubs.AddOrUpdate(club => club.CreationDate, new Club[] {
               new Club
               {

                   ClubName = "The Chess Club",
                   CreationDate = DateTime.Parse("25/01/2020"),
                   clubEvents = new List<ClubEvent>()
                   {
                         new ClubEvent { StartDateTime = DateTime.Parse("25/01/2020").Add(new TimeSpan(5,15,0,0,0)),
                                        EndDateTime =  DateTime.Parse("25/01/2020").Add(new TimeSpan(5,16,0,0,0)),
                            Location = "Sligo", Venue="Arena"
                        },
                        new ClubEvent { StartDateTime = DateTime.Parse("25/01/2020").Add(new TimeSpan(3,10,0,0,0)),
                                        EndDateTime =  DateTime.Parse("25/01/2020").Add(new TimeSpan(3,12,0,0,0)),
                            Location = "Sligo", Venue="Main Canteen"
                        },
                   }// End of new CLub events
               }, // End of First club added other clubs can be added next
               // Club 2
               new Club
               {

                   ClubName = "The Vollyball club",
                   CreationDate = DateTime.Parse("01/01/2020"),
                   clubEvents = new List<ClubEvent>()
                   {
                         new ClubEvent { StartDateTime = DateTime.Parse("01/01/2020").Add(new TimeSpan(5,15,0,0,0)),
                                        EndDateTime =  DateTime.Parse("01/01/2020").Add(new TimeSpan(5,16,0,0,0)),
                            Location = "Sligo", Venue="Arena"
                        },
                        new ClubEvent { StartDateTime = DateTime.Parse("01/01/2020").Add(new TimeSpan(3,10,0,0,0)),
                                        EndDateTime =  DateTime.Parse("01/01/2020").Add(new TimeSpan(3,12,0,0,0)),
                            Location = "Sligo", Venue="Main Canteen"
                        },
                   }// End of new CLub events
               }, // End of First club added other clubs can be added next
               // Club 3
               new Club
               {

                   ClubName = "The Soccer club",
                   CreationDate = DateTime.Parse("07/01/2020"),
                   clubEvents = new List<ClubEvent>()
                   {
                         new ClubEvent { StartDateTime = DateTime.Parse("07/01/2020").Add(new TimeSpan(5,15,0,0,0)),
                                        EndDateTime =  DateTime.Parse("07/01/2020").Add(new TimeSpan(5,16,0,0,0)),
                            Location = "Sligo", Venue="Arena"
                        },
                        new ClubEvent { StartDateTime = DateTime.Parse("07/01/2020").Add(new TimeSpan(3,10,0,0,0)),
                                        EndDateTime =  DateTime.Parse("07/01/2020").Add(new TimeSpan(3,12,0,0,0)),
                            Location = "Sligo", Venue="Main Canteen"
                        },
                   }// End of new CLub events
               }, // End of First club added other clubs can be added next


            } // End of Clubs array
           );// End of Add or Update
            context.SaveChanges();
            Seed_Members(context);
        }

        private void Seed_Members(ClubsContext context)
        {
            Club c = context.Clubs.First();

            List<Student> chosen = context.Students.Take(10).ToList();
            chosen.ForEach(s =>
            {
                context.Members.AddOrUpdate(m => m.StudentID,
                    new Member
                    {
                        myClub = c,
                        studentMember = s,
                    });
            });
            context.SaveChanges();
            c.adminID = c.clubMembers.First().MemberID;

        }

        private void Seed_students(ClubsContext context)
        {
            context.Students.AddOrUpdate(s => s.StudentID,
                Get<Student>("DataLayer.ClassLib.Models.StudentList1.csv").ToArray());
            context.SaveChanges();
        }

        private List<T> Get<T>(string resourceName)
        {
            // Get the current assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {   // create a stream reader
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    // create a csv reader for the stream
                    CsvReader csvReader = new CsvReader(reader, config);
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }

        private class StudentDto
        {
            public string StudentID { get; set; }
            public string FirstName { get; set; }

            public string SecondName { get; set; }

        }

        private void SeedEventAttendance(ClubsContext context)
        {
            List<Club> clubs = context.Clubs.ToList();
            // You can't iterate over a context collection and change the context at the same time
            // Hence we need to take a copy of the clubs collection as a list
            foreach (Club club in clubs)
                foreach (ClubEvent ev in club.clubEvents)
                    foreach (Member m in club.clubMembers)
                        context.EventAttendances.AddOrUpdate(a => new { a.EventID, a.AttendeeMember },
                            new EventAttendnace
                            {
                                EventID = ev.EventID,
                                AttendeeMember = m.MemberID
                            });
            context.SaveChanges();
        }


    }
}