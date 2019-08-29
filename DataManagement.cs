using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    //some of the properties are annotated with special attributes
    // These attributes can be found in the System.ComponentModel.DataAnnotations.dll
    // which is included in the Entity Framework
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        [Required]
        public Address BillingAddress { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string AddressLine1 { get; set; }

        [Required, MaxLength(20)]
        public string AddressLine2 { get; set; }

        [Required, MaxLength(20)]
        public string City { get; set; }

        [RegularExpression(@"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$")]
        public string ZipCode { get; set; }
    }

    public class ShopContext : DbContext
    {
        public IDbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Make sure the database knows how to handle the duplicate address property
            modelBuilder.Entity<Customer>().HasRequired(bm => bm.BillingAddress)
                .WithMany().WillCascadeOnDelete(false);
        }

    }

    public class DataManagement
    {
        public void dateTimeManagement()
        {
            //DateTime and TimeSpan structures support standard arithmetic and comparison operators.
            //A DateTime instance represents a specific time(such as 4:15 a.m.on September 5, 1970)
            //TimeSpan instance represents a period of time(such as 2 hours, 35 minutes)
            //both DateTime and TimeSpan use ticks to represent time. A tick is equal to 100 nanoseconds 

            // Create a TimeSpan representing 2.5 days.
            TimeSpan timespan1 = new TimeSpan(2, 12, 0, 0);
            // Create a TimeSpan representing 4.5 days.
            TimeSpan timespan2 = new TimeSpan(4, 12, 0, 0);
            // Create a TimeSpan representing 3.2 days using a static method
            TimeSpan timespan3 = TimeSpan.FromDays(3.2);
            // Create a TimeSpan representing 1 week.
            TimeSpan oneWeek = timespan1 + timespan2;

            // Create a DateTime with the current date and time.
            DateTime now = DateTime.Now;
            // Create a DateTime representing 1 week ago.
            DateTime past = now - oneWeek;
            // Create a DateTime representing 1 week in the future.
            DateTime future = now + oneWeek;
            // Display the DateTime instances.
            Console.WriteLine("Now   : {0}", now);
            Console.WriteLine("Past  : {0}", past);
            Console.WriteLine("Future: {0}", future);

            // Use the comparison operators.
            Console.WriteLine("Now is greater than past: {0}", now > past);
            Console.WriteLine("Now is equal to future: {0}", now == future);
            Console.WriteLine("\nMain method complete. Press Enter");

        }
    }

}





