// Austin Dugas, Cameron Washington
// C00231110, C00439696
// CMPS 358
// Project: p7

using System.Linq;
using System.Text;

// Display menu
PrintMenu();
var input = Console.ReadLine();
Console.WriteLine();

// Menu loop
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
    } else if (input == "4")
    {
        Console.Write("Enter a supplier: ");
        var supplier = Console.ReadLine();
        Console.WriteLine();
        isNotDiscontinued(supplier);
    } else if (input == "5")
    {
        Console.Write("Enter an order number: ");
        var order = Console.ReadLine();
        Console.WriteLine();
        Order(order);
    } else if (input == "6")
    {
        continue;
    }
    else
    {
        Console.WriteLine("Invalid input, try again");
        Console.WriteLine();
    }

    PrintMenu();
    input = Console.ReadLine();
    Console.WriteLine();
}

// Display menu method
static void PrintMenu()
{
    Console.WriteLine("Select an option below to traverse the database:");
    Console.WriteLine("1. List the discontinued products");
    Console.WriteLine("2. List the customer information from a given country");
    Console.WriteLine("3. List the supplier information from a given country");
    Console.WriteLine("4. List the non-discontinued products and the information associated with them");
    Console.WriteLine("5. List the order for a customer from a given order number");
    Console.WriteLine("6. Quit");
    Console.Write("Enter the number of your choice: ");
}

// 3. a) Check database for discontinued items
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

// 3. b) List customer info based on country
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

// 3. c) List supplier info based on country
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

// 3. d) List supported products based on supplier
static void isNotDiscontinued(string supplier)
{
    using var db = new smallbusiness.smallbusiness();
    {
        var results =
            from ss in db.Suppliers
            join sp in db.Products
                on ss.Id equals sp.SupplierId
            where sp.IsDiscontinued.ToString() != "1" && ss.CompanyName == supplier
            select new {ss.CompanyName, sp.ProductName, sp.UnitPrice, sp.Package};
        
        if (results.Count() == 0)
        {
            Console.WriteLine($"No supported products from {supplier}.");
            return;
        }
        
        Console.WriteLine($"Products supported by {supplier}:");
        foreach (var x in results) 
            Console.WriteLine(" { CompanyName = " + x.CompanyName + ", ProductName = " + x.ProductName + ", UnitPrice = " 
                              + Encoding.UTF8.GetString(x.UnitPrice) + ", Package = " + x.Package + " } ");
        Console.WriteLine();
    }
}

// 3. e) List order items based on order number
static void Order(string order)
{
    using var db = new smallbusiness.smallbusiness();
    {
        var results =
            from ot in db.Orders
            join co in db.Customers
                on ot.CustomerId equals co.Id
            join od in db.OrderItems
                on ot.Id equals od.OrderId
            join pi in db.Products
                on od.ProductId equals pi.Id
            where ot.OrderNumber == order
            select new {co.FirstName, co.LastName, pi.ProductName, od.UnitPrice, od.Quantity, ot.TotalAmount};

        if (results.Count() == 0)
        {
            Console.WriteLine($"No items in order {order}.");
            return;
        }
        
        Console.WriteLine($"Items in order {order}:");
        foreach (var y in results)
        {
            var s = Encoding.UTF8.GetString(y.UnitPrice);
            var d = double.Parse(s);
            Console.WriteLine(" { FirstName = " + y.FirstName + ", LastName = " + y.LastName
                              + ", ProductName = " + y.ProductName + ", UnitPrice = " + s + ", Quantity = " 
                              + y.Quantity + ", Subtotal = " + (d * y.Quantity) + ", TotalAmount = " 
                              + Encoding.UTF8.GetString(y.TotalAmount) + " } ");
        }
        Console.WriteLine();
    }
}

// Exit menu
Console.WriteLine("You have exited the database");