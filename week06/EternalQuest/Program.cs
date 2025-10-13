using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the EternalQuest Project.");

        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}

// Abstract base class for all goals
abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetShortName();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

// SimpleGoal: can be completed once
class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetShortName()
    {
        return _name;
    }

    public override string GetDetailsString()
    {
        return $"{_name} ({_description}) - {_points} pts - {(IsComplete() ? "Completed" : "Incomplete")}";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }
}

// EternalGoal: can be completed unlimited times
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetShortName()
    {
        return _name;
    }

    public override string GetDetailsString()
    {
        return $"{_name} ({_description}) - {_points} pts - Eternal";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}";
    }
}

// ChecklistGoal: must be completed a certain number of times for a bonus
class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;
    private int _amountCompleted;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (!IsComplete())
        {
            _amountCompleted++;
            if (_amountCompleted == _target)
            {
                return _points + _bonus;
            }
            else
            {
                return _points;
            }
        }
        return 0;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetShortName()
    {
        return _name;
    }

    public override string GetDetailsString()
    {
        return $"{_name} ({_description}) - {_points} pts - Completed {_amountCompleted}/{_target} times - {(IsComplete() ? "Completed" : "Incomplete")}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_bonus}|{_target}|{_amountCompleted}";
    }
}

class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private List<string> _badges;
    private int _streak;
    private DateTime _lastLogin;
    private int _totalGoalsCompleted;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        _badges = new List<string>();
        _streak = 0;
        _lastLogin = DateTime.MinValue;
        _totalGoalsCompleted = 0;
    }

    public void Start()
    {
        CheckLoginStreak();

        int choice;
        do
        {
            DisplayPlayerInfo();
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. View Achievements");
            Console.WriteLine("  7. Quit");
            Console.Write("Select a choice from the menu: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        CreateGoal();
                        break;
                    case 2:
                        ListGoalDetails();
                        break;
                    case 3:
                        SaveGoals();
                        break;
                    case 4:
                        LoadGoals();
                        break;
                    case 5:
                        RecordEvent();
                        break;
                    case 6:
                        DisplayAchievements();
                        break;
                    case 7:
                        Console.WriteLine("Goodbye! Keep pursuing your Eternal Quest!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        } while (choice != 7);
    }

    private void CheckLoginStreak()
    {
        DateTime today = DateTime.Today;
        if (_lastLogin != DateTime.MinValue)
        {
            if (_lastLogin.AddDays(1) == today)
            {
                _streak++;
                AwardStreakBonus();
            }
            else if (_lastLogin != today)
            {
                _streak = 1;
            }
        }
        else
        {
            _streak = 1;
        }
        _lastLogin = today;
    }

    private void AwardStreakBonus()
    {
        if (_streak % 7 == 0) // Weekly streak
        {
            int bonus = 100 * (_streak / 7);
            _score += bonus;
            Console.WriteLine($"{_streak}-day login streak! +{bonus} bonus points!");

            if (_streak == 7 && !_badges.Contains("Weekly Warrior"))
            {
                _badges.Add("Weekly Warrior");
                Console.WriteLine("New Badge: Weekly Warrior!");
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        UpdateLevel();
        string title = GetPlayerTitle();

        Console.WriteLine($"\n=== {title} ===");
        Console.WriteLine($"Level: {_level} | Points: {_score} | Streak: {_streak} days");
        Console.WriteLine($"Goals: {_goals.Count} | Completed: {_goals.Count(g => g.IsComplete())}");
        Console.WriteLine($"Next Level: {GetNextLevelPoints()} points needed");
    }

    private string GetPlayerTitle()
    {
        return _level switch
        {
            < 5 => "Goal Beginner",
            < 10 => "Quest Adventurer",
            < 15 => "Eternal Champion",
            < 20 => "Master Achiever",
            _ => "Legendary Hero"
        };
    }

    private void UpdateLevel()
    {
        int newLevel = (_score / 500) + 1;
        if (newLevel > _level)
        {
            int oldLevel = _level;
            _level = newLevel;
            Console.WriteLine($"\nLEVEL UP! {GetPlayerTitle()} Level {_level}!");
            AwardLevelBadge();
        }
    }

    private void AwardLevelBadge()
    {
        string badge = _level switch
        {
            5 when !_badges.Contains("Rising Star") => "Rising Star",
            10 when !_badges.Contains("Goal Master") => "Goal Master",
            15 when !_badges.Contains("Eternal Legend") => "Eternal Legend",
            20 when !_badges.Contains("Ultimate Achiever") => "Ultimate Achiever",
            _ => null
        };

        if (badge != null)
        {
            _badges.Add(badge);
            Console.WriteLine($"New Badge Unlocked: {badge}!");
        }
    }

    private int GetNextLevelPoints()
    {
        return (_level * 500) - _score;
    }

    public void ListGoalNames()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            string status = _goals[i].IsComplete() ? "✅" : "🎯";
            Console.WriteLine($"{status} {i + 1}. {_goals[i].GetShortName()}");
        }
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available. Create some goals first!");
            return;
        }

        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        if (!int.TryParse(Console.ReadLine(), out int goalType))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");

        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points value.");
            return;
        }

        switch (goalType)
        {
            case 1:
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case 3:
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                if (!int.TryParse(Console.ReadLine(), out int target))
                {
                    Console.WriteLine("Invalid target value.");
                    return;
                }
                Console.Write("What is the bonus for accomplishing it that many times? ");
                if (!int.TryParse(Console.ReadLine(), out int bonus))
                {
                    Console.WriteLine("Invalid bonus value.");
                    return;
                }
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        CheckGoalCreationAchievements();
        Console.WriteLine("Goal created successfully!");
    }

    private void CheckGoalCreationAchievements()
    {
        if (_goals.Count == 3 && !_badges.Contains("Goal Setter"))
        {
            _badges.Add("Goal Setter");
            Console.WriteLine("New Badge: Goal Setter!");
        }
        else if (_goals.Count == 10 && !_badges.Contains("Master Planner"))
        {
            _badges.Add("Master Planner");
            Console.WriteLine("New Badge: Master Planner!");
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available. Create some goals first!");
            return;
        }

        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");

        if (!int.TryParse(Console.ReadLine(), out int goalNumber) || goalNumber < 1 || goalNumber > _goals.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }

        goalNumber--; // Convert to zero-based index
        Goal goal = _goals[goalNumber];

        bool wasCompleteBefore = goal.IsComplete();
        int pointsEarned = goal.RecordEvent();

        // Apply streak multiplier for high streaks
        double multiplier = _streak >= 14 ? 1.5 : _streak >= 7 ? 1.2 : 1.0;
        int totalPoints = (int)(pointsEarned * multiplier);
        int bonusPoints = totalPoints - pointsEarned;

        _score += totalPoints;

        Console.WriteLine($"\nCongratulations! You've earned {pointsEarned} points!");

        if (bonusPoints > 0)
        {
            Console.WriteLine($"Streak Multiplier: +{bonusPoints} bonus points!");
        }

        Console.WriteLine($"Total Points: {_score}");

        // Check if goal was just completed
        if (!wasCompleteBefore && goal.IsComplete())
        {
            _totalGoalsCompleted++;
            CheckCompletionAchievements();
        }

        CheckPointMilestones();
    }

    private void CheckCompletionAchievements()
    {
        if (_totalGoalsCompleted == 5 && !_badges.Contains("Achievement Hunter"))
        {
            _badges.Add("Achievement Hunter");
            Console.WriteLine("New Badge: Achievement Hunter - 5 goals completed!");
        }
        else if (_totalGoalsCompleted == 20 && !_badges.Contains("Completion Master"))
        {
            _badges.Add("Completion Master");
            Console.WriteLine("New Badge: Completion Master - 20 goals completed!");
        }
    }

    private void CheckPointMilestones()
    {
        if (_score >= 1000 && !_badges.Contains("Point Collector"))
        {
            _badges.Add("Point Collector");
            Console.WriteLine("New Badge: Point Collector - 1,000 points!");
        }
        else if (_score >= 5000 && !_badges.Contains("Point Master"))
        {
            _badges.Add("Point Master");
            Console.WriteLine("New Badge: Point Master - 5,000 points!");
        }
    }

    public void DisplayAchievements()
    {
        Console.WriteLine("\n=== Your Achievements ===");
        Console.WriteLine($"Level: {_level} - {GetPlayerTitle()}");
        Console.WriteLine($"Total Points: {_score}");
        Console.WriteLine($"Login Streak: {_streak} days");
        Console.WriteLine($"Goals Created: {_goals.Count}");
        Console.WriteLine($"Goals Completed: {_totalGoalsCompleted}");

        if (_badges.Count > 0)
        {
            Console.WriteLine($"\nBadges ({_badges.Count}):");
            foreach (string badge in _badges)
            {
                Console.WriteLine($"   ★ {badge}");
            }
        }
        else
        {
            Console.WriteLine("\nNo badges yet. Keep working on your goals!");
        }

        // Show next milestones
        Console.WriteLine($"\nNext Milestones:");
        Console.WriteLine($"Level {_level + 1}: {GetNextLevelPoints()} points needed");
        if (_streak < 7)
            Console.WriteLine($"7-day streak: {7 - _streak} days needed");
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine(_score);
                outputFile.WriteLine(_level);
                outputFile.WriteLine(_streak);
                outputFile.WriteLine(_lastLogin.ToString("yyyy-MM-dd"));
                outputFile.WriteLine(_totalGoalsCompleted);
                outputFile.WriteLine(string.Join("|", _badges));

                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            if (lines.Length < 5)
            {
                Console.WriteLine("Invalid file format.");
                return;
            }

            _goals.Clear();

            // Load player data
            _score = int.Parse(lines[0]);
            _level = int.Parse(lines[1]);
            _streak = int.Parse(lines[2]);
            _lastLogin = DateTime.Parse(lines[3]);
            _totalGoalsCompleted = int.Parse(lines[4]);
            _badges = new List<string>(lines[5].Split('|'));

            // Load goals
            for (int i = 6; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split("|");
                if (parts.Length < 4) continue;

                string goalType = parts[0];
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);

                switch (goalType)
                {
                    case "SimpleGoal":
                        if (parts.Length >= 5 && bool.TryParse(parts[4], out bool isComplete))
                        {
                            _goals.Add(new SimpleGoal(name, description, points, isComplete));
                        }
                        break;
                    case "EternalGoal":
                        _goals.Add(new EternalGoal(name, description, points));
                        break;
                    case "ChecklistGoal":
                        if (parts.Length >= 7 &&
                            int.TryParse(parts[4], out int bonus) &&
                            int.TryParse(parts[5], out int target) &&
                            int.TryParse(parts[6], out int amountCompleted))
                        {
                            _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
                        }
                        break;
                }
            }

            Console.WriteLine("Goals loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }
}