using System;
using System.Collections.Generic;

// Base abstract Shape class
public abstract class Shape
{
    private string _color;

    public Shape(string color)
    {
        _color = color;
    }

    public string GetColor()
    {
        return _color;
    }

    public void SetColor(string color)
    {
        _color = color;
    }

    public abstract double GetArea();
}

// Square class derived from Shape
public class Square : Shape
{
    private double _side;

    public Square(string color, double side) : base(color)
    {
        _side = side;
    }

    public override double GetArea()
    {
        return _side * _side;
    }
}

// Rectangle class derived from Shape
public class Rectangle : Shape
{
    private double _length;
    private double _width;

    public Rectangle(string color, double length, double width) : base(color)
    {
        _length = length;
        _width = width;
    }

    public override double GetArea()
    {
        return _length * _width;
    }
}

// Circle class derived from Shape
public class Circle : Shape
{
    private double _radius;

    public Circle(string color, double radius) : base(color)
    {
        _radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Test individual shapes first
        
        Square square = new Square("Red", 5);
        Console.WriteLine($"Square - Color: {square.GetColor()}, Area: {square.GetArea():F2}");
        
        Rectangle rectangle = new Rectangle("Blue", 4, 6);
        Console.WriteLine($"Rectangle - Color: {rectangle.GetColor()}, Area: {rectangle.GetArea():F2}");
        
        Circle circle = new Circle("Green", 3);
        Console.WriteLine($"Circle - Color: {circle.GetColor()}, Area: {circle.GetArea():F2}");
        
        Console.WriteLine("");
        
        // Create a list of shapes
        List<Shape> shapes = new List<Shape>();
        
        // Add different shapes to the list
        shapes.Add(new Square("Yellow", 7));
        shapes.Add(new Rectangle("Purple", 5, 8));
        shapes.Add(new Circle("Orange", 4.5));
        shapes.Add(new Square("Pink", 3.2));
        shapes.Add(new Rectangle("Brown", 10, 2));
        
        // Iterate through the list and display color and area
        foreach (Shape shape in shapes)
        {
            string shapeType = shape.GetType().Name;
            Console.WriteLine($"{shapeType} - Color: {shape.GetColor()}, Area: {shape.GetArea():F2}");
        }
        
        // Display total area
        double totalArea = 0;
        foreach (Shape shape in shapes)
        {
            totalArea += shape.GetArea();
        }
        Console.WriteLine("");
        Console.WriteLine($"Total area of all shapes: {totalArea:F2}");
    }
}