using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EF9andJson;

public class MyDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=ALIKOLAHDOOZAN;Database=EFandJSON;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .OwnsOne(x => x.Contact, contactOptions =>
            {
                contactOptions.ToJson();

                contactOptions.OwnsOne(x => x.Address);

                contactOptions.OwnsMany(x => x.PhoneNumbers);
            });
    }





    // Old way 
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // Configure Customer entity
    //    modelBuilder.Entity<Customer>()
    //        .Property(c => c.Contact)
    //        .HasConversion(
    //            new ValueConverter<Contact, string>(
    //                contact => JsonConvert.SerializeObject(contact), // Serialize Contact to JSON
    //                json => JsonConvert.DeserializeObject<Contact>(json)! // Deserialize JSON to Contact
    //            ));
    //}







}