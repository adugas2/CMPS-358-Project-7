// Austin Dugas, Cameron Washington
// C00231110, C00439696
// CMPS 358
// Project: p7

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

isDiscontinued();
ListCustomersInfo("Canada");
ListSupplierInfo("USA");
