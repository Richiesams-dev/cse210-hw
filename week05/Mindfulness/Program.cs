using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base abstract class for all mindfulness activities
    public abstract class MindfulnessActivity
    {
        protected string Name { get; set; }
        protected string Description { get; set; }
        protected int Duration { get; set; }
        protected static int TotalActivitiesCompleted { get; set; }

        public MindfulnessActivity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Start()
        {
            DisplayStartingMessage();
            SetDuration();
            PrepareToBegin();
            RunActivity();
            DisplayEndingMessage();
            TotalActivitiesCompleted++;
        }

        protected virtual void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Starting: {Name}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(Description);
        }

        protected void SetDuration()
        {
            while (true)
            {
                Console.Write("\nHow long would you like this activity to last (in seconds)? ");
                if (int.TryParse(Console.ReadLine(), out int duration) && duration > 0)
                {
                    Duration = duration;
                    break;
                }
                Console.WriteLine("Please enter a valid positive number.");
            }
        }

        protected void PrepareToBegin()
        {
            Console.WriteLine("\nPrepare to begin...");
            ShowSpinner(3);
        }

        protected virtual void DisplayEndingMessage()
        {
            Console.WriteLine("\nGood job! You have completed the activity.");
            ShowSpinner(2);
            Console.WriteLine($"\nActivity: {Name}");
            Console.WriteLine($"Duration: {Duration} seconds");
            ShowSpinner(3);
        }

        protected abstract void RunActivity();

        protected void ShowSpinner(int seconds)
        {
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            string[] spinner = { "|", "/", "-", "\\" };
            int spinnerIndex = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write($"\r{spinner[spinnerIndex]} ");
                spinnerIndex = (spinnerIndex + 1) % spinner.Length;
                Thread.Sleep(200);
            }
            Console.Write("\r  \r");
        }

        protected void ShowCountdown(int seconds, string message = "")
        {
            for (int i = seconds; i > 0; i--)
            {
                // Clear the line and rewrite the message with countdown
                Console.Write($"\r{message}{i} ");
                Thread.Sleep(1000);
            }
            // Clear the entire line including the message
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        }

        public static void DisplayActivityStats()
        {
            Console.WriteLine($"\nTotal activities completed: {TotalActivitiesCompleted}");
        }
    }

    // Breathing Activity Class
    public class BreathingActivity : MindfulnessActivity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing."
        ) { }

        protected override void RunActivity()
        {
            Console.WriteLine("\nStarting breathing exercise...");
            DateTime endTime = DateTime.Now.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("\nBreathe in... ");
                ShowCountdown(4, "Breathe in... ");

                if (DateTime.Now >= endTime) break;

                Console.Write("Breathe out... ");
                ShowCountdown(6, "Breathe out... ");
            }
        }
    }

    // Reflection Activity Class
    public class ReflectionActivity : MindfulnessActivity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life."
        ) { }

        protected override void RunActivity()
        {
            string prompt = GetRandomPrompt();
            Console.WriteLine($"\nPrompt: {prompt}");
            Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience:");
            Console.Write("Starting in ");
            ShowCountdown(3, "Starting in ");

            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            Random random = new Random();
            List<string> availableQuestions = new List<string>(_questions);

            while (DateTime.Now < endTime && availableQuestions.Count > 0)
            {
                string question = GetRandomQuestion(availableQuestions, random);
                availableQuestions.Remove(question);

                Console.WriteLine($"\n{question}");
                ShowSpinner(5);
            }
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            return _prompts[random.Next(_prompts.Count)];
        }

        private string GetRandomQuestion(List<string> questions, Random random)
        {
            return questions[random.Next(questions.Count)];
        }
    }

    // Listing Activity Class
    public class ListingActivity : MindfulnessActivity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private List<string> _userItems = new List<string>();

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
        ) { }

        protected override void RunActivity()
        {
            string prompt = GetRandomPrompt();
            Console.WriteLine($"\nPrompt: {prompt}");

            Console.WriteLine("\nGet ready to list items...");
            ShowCountdown(5, "Get ready to list items... ");

            Console.WriteLine("\nStart listing items (press Enter after each item):");
            DateTime endTime = DateTime.Now.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    _userItems.Add(item);
                }
            }

            Console.WriteLine($"\nYou listed {_userItems.Count} items!");
            if (_userItems.Count > 0)
            {
                Console.WriteLine("\nYour items:");
                foreach (var item in _userItems)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            return _prompts[random.Next(_prompts.Count)];
        }
    }

    // Main Application Class
    public class MindfulnessApp
    {
        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new BreathingActivity().Start();
                        break;
                    case "2":
                        new ReflectionActivity().Start();
                        break;
                    case "3":
                        new ListingActivity().Start();
                        break;
                    case "4":
                        MindfulnessActivity.DisplayActivityStats();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using the Mindfulness App. Take care!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("MINDFULNESS ACTIVITIES APP");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. View Activity Statistics");
            Console.WriteLine("5. Exit");
            Console.WriteLine(new string('-', 50));
            Console.Write("Choose an activity (1-5): ");
        }
    }

    // Main Program Class
    class Program
    {
        static void Main(string[] args)
        {
            MindfulnessApp app = new MindfulnessApp();
            app.Run();
        }
    }
}