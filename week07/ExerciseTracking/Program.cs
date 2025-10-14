using System;
using System.Collections.Generic;

// Abstract base Activity class
public abstract class Activity
{
    private DateTime _date;
    private int _minutes;
    protected bool _useMiles;

    public Activity(DateTime date, int minutes, bool useMiles = true)
    {
        _date = date;
        _minutes = minutes;
        _useMiles = useMiles;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    // Abstract methods
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Template method pattern
    public virtual string GetSummary()
    {
        string unit = _useMiles ? "miles" : "km";
        string speedUnit = _useMiles ? "mph" : "kph";
        string paceUnit = _useMiles ? "min per mile" : "min per km";

        return $"{_date:dd MMM yyyy} {GetType().Name} ({_minutes} min)- " +
               $"Distance {GetDistance():F1} {unit}, Speed {GetSpeed():F1} {speedUnit}, Pace: {GetPace():F1} {paceUnit}";
    }

    // Protected helper methods
    protected double CalculateSpeed(double distance)
    {
        return (distance / Minutes) * 60;
    }

    protected double CalculatePace(double distance)
    {
        return distance > 0 ? Minutes / distance : 0;
    }
}

// Running class
public class Running : Activity
{
    private double _distance;

    public Running(DateTime date, int minutes, double distance, bool useMiles = true)
        : base(date, minutes, useMiles)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return CalculateSpeed(_distance);
    }

    public override double GetPace()
    {
        return CalculatePace(_distance);
    }
}

// Cycling class
public class Cycling : Activity
{
    private double _speed;

    public Cycling(DateTime date, int minutes, double speed, bool useMiles = true)
        : base(date, minutes, useMiles)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * Minutes) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return CalculatePace(GetDistance());
    }
}

// Swimming class
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps, bool useMiles = true)
        : base(date, minutes, useMiles)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double distanceInKm = _laps * 50.0 / 1000;

        if (_useMiles)
        {
            return distanceInKm * 0.62;
        }

        return distanceInKm;
    }

    public override double GetSpeed()
    {
        return CalculateSpeed(GetDistance());
    }

    public override double GetPace()
    {
        return CalculatePace(GetDistance());
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the ExerciseTracking Project.");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine();

        // Create activities in both units to demonstrate the requirement
        List<Activity> activities = new List<Activity>
        {
            // Miles version (as shown in first sample)
            new Running(new DateTime(2022, 11, 3), 30, 3.0, true),
            
            // Kilometers version (as shown in second sample) 
            new Running(new DateTime(2022, 11, 3), 30, 4.8, false),

            new Cycling(new DateTime(2022, 11, 4), 45, 15.5, true),
            new Swimming(new DateTime(2022, 11, 5), 60, 40, true)
        };

        // Display summaries
        Console.WriteLine("Exercise Tracking Summary:");
        Console.WriteLine("--------------------------");

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine();
        }
    }
}