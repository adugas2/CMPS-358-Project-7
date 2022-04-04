// Austin Dugas, Cameron Washington
// C00231110, C00439696
// CMPS 358
// Project: p7

PrintMenu();
Console.Write("Enter the number of your choice: ");
var input = Console.ReadLine();
Console.WriteLine();

while (input != "6")
{
    if (input == "1")
    {
        isDiscontinued();
    } else if (input == "2")
    {
        Console.Write("Enter a country: ");
        var country = Console.ReadLine();
        Console.WriteLine();
        ListCustomersInfo(country);
    } else if (input == "3")
    {
        Console.Write("Enter a country: ");
        var country = Console.ReadLine();
        Console.WriteLine();
        ListSupplierInfo(country);
    } else if (input == "6")
    {
        continue;
    }

    PrintMenu();
    Console.Write("Enter the number of your choice: ");
    input = Console.ReadLine();
    Console.WriteLine();
}

static void PrintMenu()
{
    Console.WriteLine("Select an option below to traverse the database:");
    Console.WriteLine("1. List the discontinued products");
    Console.WriteLine("2. List the customer information from a given country");
    Console.WriteLine("3. List the supplier information from a given country");
    Console.WriteLine("4. List the non-discontinued products and the information associated with them");
    Console.WriteLine("5. List the order for a customer from a given order number");
    Console.WriteLine("6. Quit");
}

static void isDiscontinued()
{
    using var db = new smallbusiness.smallbusiness();
    {
        var results =
            from p in db.Products
            where p.IsDiscontinued.ToString() == "1"
            select p.ProductName;

        Console.WriteLine("Products that are discontinued: ");
        foreach (var p in results)
        {
            Console.WriteLine(p + " ");
        }
        Console.WriteLine();
    }
}

static void ListCustomersInfo(string country)
{
    using var db = new smallbusiness.smallbusiness();
    {
        var results =
            from co in db.Customers
            where co.Country == country
            select new {co.FirstName, co.LastName, co.Phone};
        if (results.Count() == 0)
        {
            Console.WriteLine($"No customers in {country}.");
            return;
        }

        Console.WriteLine($"Customers in {country}:");
        foreach (var c in results)
            Console.WriteLine($" {c}");
        Console.WriteLine();
    }
}

static void ListSupplierInfo(string country)
{
    using var db = new smallbusiness.smallbusiness();
    {
        var results =
            from su in db.Suppliers
            where su.Country == country
            select new {su.Id, su.CompanyName, su.Phone, su.Fax, su.City };
        if (results.Count() == 0)
        {
            Console.WriteLine($"No suppliers in {country}.");
            return;
        }

        Console.WriteLine($"Suppliers in {country}:");
        foreach (var c in results)
            Console.WriteLine($" {c}");
        Console.WriteLine();
    }
}

Console.WriteLine("You have exited the database");
/*
isDiscontinued();
ListCustomersInfo("Canada");
ListSupplierInfo("USA");
*/