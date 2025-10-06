using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Journal Program!");

        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Exit");
            Console.Write("What would like to do?: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = journal.GetRandomPrompt();
                    Console.WriteLine($"{prompt}");
                    string response = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        string date = DateTime.Now.ToString("yyyy-MM-dd");
                        journal.AddEntry(prompt, response, date);
                    }
                    else
                    {
                        Console.WriteLine("No response provided. Entry not saved.");
                    }
                    break;

                case "2":
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.WriteLine("What is the filename?");
                    string loadFilename = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(loadFilename))
                    {
                        journal.LoadFromFile(loadFilename);
                    }
                    else
                    {
                        Console.WriteLine("Filename cannot be empty.");
                    }
                    break;


                case "4":
                    Console.WriteLine("What is the filename?");
                    string saveFilename = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(saveFilename))
                    {
                        journal.SaveToFile(saveFilename);
                    }
                    else
                    {
                        Console.WriteLine("Filename cannot be empty.");
                    }
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose a number between 1-5.");
                    break;
            }
        }
    }
}

public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {Date} - prompt: {Prompt}");
        Console.WriteLine($"{Response}");
        Console.WriteLine("");
    }
}

public class Journal
{
    private JournalEntry[] entries;
    private int entryCount;
    private const int MAX_ENTRIES = 100;
    private string[] prompts;

    public Journal()
    {
        entries = new JournalEntry[MAX_ENTRIES];
        entryCount = 0;

        prompts = new string[]
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What made me smile today?",
            "What challenge did I overcome today?",
            "What am I grateful for today?",
            "What did I learn today?",
            "How did I take care of myself today?"
        };
    }

    public void AddEntry(string prompt, string response, string date)
    {
        if (entryCount < MAX_ENTRIES)
        {
            entries[entryCount] = new JournalEntry(prompt, response, date);
            entryCount++;
        }
        else
        {
            Console.WriteLine("Journal is full! Cannot add more entries.");
        }
    }

    public void DisplayEntries()
    {
        if (entryCount == 0)
        {
            Console.WriteLine("No entries found. Start by writing a new entry!");
            return;
        }

        for (int i = 0; i < entryCount; i++)
        {
            entries[i].Display();
        }
    }

    public string GetRandomPrompt()
    {
        Random random = new Random();
        return prompts[random.Next(prompts.Length)];
    }

    public void SaveToFile(string filename)
    {
        try
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filename))
            {
                for (int i = 0; i < entryCount; i++)
                {
                    writer.WriteLine($"{entries[i].Date}~|~{entries[i].Prompt}~|~{entries[i].Response}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            if (!System.IO.File.Exists(filename))
            {
                Console.WriteLine($"File {filename} does not exist.");
                return;
            }

            // Reset journal
            entries = new JournalEntry[MAX_ENTRIES];
            entryCount = 0;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null && entryCount < MAX_ENTRIES)
                {
                    string[] parts = line.Split(new[] { "~|~" }, StringSplitOptions.None);
                    if (parts.Length >= 3)
                    {
                        entries[entryCount] = new JournalEntry(parts[1], parts[2], parts[0]);
                        entryCount++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }
}