using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF9andJson;

public class Customer
{
    public int ID { get; init; }

    public string Name { get; init; }

    public Contact Contact { get; set; } = null!;

    public Customer(string name)
    {
        Name = name;
    }
}

public class Contact
{
    public required Address Address { get; set; }

    public List<PhoneNumber> PhoneNumbers { get; set; } = new();
}

public class Address
{
    public required string Street { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public required string PostalCode { get; set; }
}

public class PhoneNumber
{
    public PhoneNumberType Type { get; set; }

    public string Number { get; set; }

    public PhoneNumber(PhoneNumberType type, string number)
    {
        Type = type;
        Number = number;
    }
}

public enum PhoneNumberType
{
    Mobile,
    Home
}
