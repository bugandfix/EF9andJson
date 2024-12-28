using EF9andJson;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new MyDbContext();

        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Create
        var customer = new Customer("Ali")
        {
            Contact = new Contact
            {
                Address = new Address
                {
                    Street = "123 Main St",
                    City = "KL",
                    State = "KL",
                    PostalCode = "98052"
                },
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber(PhoneNumberType.Mobile, "111-123-4567"),
                    new PhoneNumber(PhoneNumberType.Home, "222-123-4568")
                }
            }
        };

        await AddCustomerAsync(context, customer);


        // Read
        var retrievedCustomer = await GetCustomerAsync(context, customer.ID);
        Console.WriteLine($"Customer: {retrievedCustomer?.Name}, City: {retrievedCustomer?.Contact.Address.City}");

   

        //Query 1
        //Warning - More than one record is a Headache 
        Customer Ali = await context.Customers.SingleAsync(x => x.Name == "Ali");
        Ali.Contact.Address.PostalCode = "20877";
        await context.SaveChangesAsync();

        //Query 2
        List<Customer> customersHavingHomePhoneNumbers = await context.Customers
       .Where(x => x.Contact.PhoneNumbers.Any(x => x.Type == PhoneNumberType.Home))
       .ToListAsync();

        foreach (var cst in customersHavingHomePhoneNumbers)
        {
            Console.WriteLine($"Customer: {cst?.Name}, City: {cst?.Contact.Address.City}");
        }

        //Query 3
        List<Customer> customersInKL = await context.Customers
       .Where(x => x.Contact.Address.State == "KL")
        .ToListAsync();

        foreach (var cst in customersInKL)
        {
            Console.WriteLine($"Customer: {cst?.Name}, City: {cst?.Contact.Address.City}");
        }



        //// Update
        if (retrievedCustomer != null)
        {
            retrievedCustomer.Contact.Address.City = "Penang";
            await UpdateCustomerAsync(context, retrievedCustomer);
        }

        retrievedCustomer = await GetCustomerAsync(context, customer.ID);
        Console.WriteLine($"Customer: {retrievedCustomer?.Name}, City: {retrievedCustomer?.Contact.Address.City}");

        // Delete
        //if (retrievedCustomer != null)
        //{
        //    await DeleteCustomerAsync(context, retrievedCustomer.ID);
        //}
    }

    // Create
    static async Task AddCustomerAsync(MyDbContext context, Customer customer)
    {
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
    }

    // Read
    static async Task<Customer?> GetCustomerAsync(MyDbContext context, int id)
    {
        return await context.Customers.FindAsync(id);
    }

    // Update
    static async Task UpdateCustomerAsync(MyDbContext context, Customer customer)
    {
        context.Customers.Update(customer);
        await context.SaveChangesAsync();
    }

    // Delete
    static async Task DeleteCustomerAsync(MyDbContext context, int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer != null)
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }
    }


}