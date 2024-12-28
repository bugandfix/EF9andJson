using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF9andJson;

public class OldCustomer
{
    public int ID { get; init; }
    public string Name { get; init; }
    public OldContact Contact { get; set; } = null!;

    public OldCustomer(string name)
    {
        Name = name;
    }
}

public class OldContact
{
    public int ID { get; set; }
    public int CustomerID { get; set; } // Foreign key for Customer
    public int AddressID { get; set; }  // Foreign key for Address
    public OldAddress Address { get; set; } = null!;
    public List<OldPhoneNumber> PhoneNumbers { get; set; } = new();
}

public class OldAddress
{
    public int ID { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
}

public class OldPhoneNumber
{
    public int ID { get; set; }
    public int ContactID { get; set; } // Foreign key for Contact
    public PhoneNumberType Type { get; set; }
    public string Number { get; set; }

    public OldPhoneNumber(PhoneNumberType type, string number)
    {
        Type = type;
        Number = number;
    }
}
