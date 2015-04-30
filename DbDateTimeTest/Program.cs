using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDateTimeTest
{
    [Serializable]
    public class MyClasse
    {
        [Key]
        public DateTime dateTime { get; set; }
    }

    public class MyContext : DbContext
    {
        public DbSet<MyClasse> MyClasses { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Store it in the queue
            MyClasse original = new MyClasse();
            original.dateTime = DateTime.Now;

            // Store it in the database
            var db = new MyContext();
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.MyClasses");
            db.MyClasses.Add(original);
            db.SaveChanges();

            // Check the database version
            MyClasse dbItem = db.MyClasses.FirstOrDefault();
            Console.WriteLine("DB Item:");
            Console.WriteLine("  * Original: " + original.dateTime.ToString() + " ... Milliseconds: " + original.dateTime.Millisecond);
            Console.WriteLine("  * DB Item: " + dbItem.dateTime.ToString() + " ... Milliseconds: " + dbItem.dateTime.Millisecond);
            Console.WriteLine("  * Equal? " + dbItem.dateTime.Equals(original.dateTime));

            Console.ReadLine();
        }
    }
}
