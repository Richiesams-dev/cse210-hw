using System;
using System.Collections.Generic;

// Product class
public class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string GetName() { return _name; }
    public string GetProductId() { return _productId; }
    public double GetPrice() { return _price; }
    public int GetQuantity() { return _quantity; }

    public double CalculateTotalCost()
    {
        return _price * _quantity;
    }
}

// Address class
public class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public string GetStreetAddress() { return _streetAddress; }
    public string GetCity() { return _city; }
    public string GetStateProvince() { return _stateProvince; }
    public string GetCountry() { return _country; }

    public bool IsInUSA()
    {
        return _country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }
}

// Customer class
public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName() { return _name; }
    public Address GetAddress() { return _address; }

    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }
}

// Order class
public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateShippingCost()
    {
        return _customer.IsInUSA() ? 5 : 35;
    }

    public double CalculateTotalCost()
    {
        double totalProductCost = 0;
        foreach (Product product in _products)
        {
            totalProductCost += product.CalculateTotalCost();
        }
        return totalProductCost + CalculateShippingCost();
    }

    public string GetPackingLabel()
    {
        string packingLabel = "PACKING LABEL:\n";
        packingLabel += "----------------\n";
        foreach (Product product in _products)
        {
            packingLabel += $"Product: {product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        string shippingLabel = "SHIPPING LABEL:\n";
        shippingLabel += "-----------------\n";
        shippingLabel += $"Customer: {_customer.GetName()}\n";
        shippingLabel += _customer.GetAddress().GetFullAddress();
        return shippingLabel;
    }

    public string GetOrderSummary()
    {
        string summary = "ORDER SUMMARY:\n";
        summary += "---------------\n";
        summary += $"Number of Items: {_products.Count}\n";
        summary += $"Shipping Cost: ${CalculateShippingCost()}\n";
        summary += $"Total Cost: ${CalculateTotalCost():F2}\n";
        return summary;
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ORDER PROCESSING SYSTEM");
        Console.WriteLine("------------------------\n");

        // Create addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Oak Ave", "Toronto", "Ontario", "Canada");
        Address address3 = new Address("789 Pine Rd", "Los Angeles", "CA", "USA");

        // Create customers
        Customer customer1 = new Customer("John Smith", address1);
        Customer customer2 = new Customer("Marie Dubois", address2);
        Customer customer3 = new Customer("Sarah Johnson", address3);

        // Create products
        Product product1 = new Product("Laptop", "LAP1001", 899.99, 1);
        Product product2 = new Product("Mouse", "MOU2002", 25.50, 2);
        Product product3 = new Product("Keyboard", "KEY3003", 75.00, 1);
        Product product4 = new Product("Monitor", "MON4004", 249.99, 1);
        Product product5 = new Product("Webcam", "WEB5005", 45.00, 1);
        Product product6 = new Product("Headphones", "HEA6006", 89.99, 2);

        // Create orders and add products
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);
        order2.AddProduct(product6);

        Order order3 = new Order(customer3);
        order3.AddProduct(product2);
        order3.AddProduct(product5);

        // Display order information
        List<Order> orders = new List<Order> { order1, order2, order3 };

        for (int i = 0; i < orders.Count; i++)
        {
            Console.WriteLine($"ORDER #{i + 1}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine(orders[i].GetShippingLabel());
            Console.WriteLine();
            Console.WriteLine(orders[i].GetPackingLabel());
            Console.WriteLine(orders[i].GetOrderSummary());
        }
    }
}