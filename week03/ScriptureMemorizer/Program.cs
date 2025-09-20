using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {

        // Create a library of scriptures
        List<Scripture> scriptureLibrary = CreateScriptureLibrary();
        
        // Select a random scripture from the library
        Random random = new Random();
        Scripture scripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];
        
        // Main program loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to continue or type 'quit' to finish:");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            // Hide a few words
            scripture.HideRandomWords(3);

            // If all words are hidden, end the program
            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden! Great job memorizing!");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                break;
            }
        }
    }

    // Create a library of scriptures
    static List<Scripture> CreateScriptureLibrary()
    {
        List<Scripture> library = new List<Scripture>();
        
        // Add multiple scriptures to the library
        library.Add(new Scripture(
            new Reference("John", 3, 16),
            "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."
        ));
        
        library.Add(new Scripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."
        ));
        
        library.Add(new Scripture(
            new Reference("Philippians", 4, 13),
            "I can do all this through him who gives me strength."
        ));
        
        library.Add(new Scripture(
            new Reference("Jacob", 3, 2),
            "O all ye that are pure in heart, lift up your heads and receive the pleasing word of God, and feast upon his love; for ye may, if your minds are firm, forever."
        ));
        
        library.Add(new Scripture(
            new Reference("Romans", 8, 28),
            "And we know that in all things God works for the good of those who love him, who have been called according to his purpose."
        ));
        
        library.Add(new Scripture(
            new Reference("Alma", 2, 35),
            "Yea, come unto me and bring forth works of righteousness, and ye shall not be hewn down and cast into the fire."
        ));
        
        library.Add(new Scripture(
            new Reference("2 Nephi", 7, 5),
            "The Lord God hath opened mine ear, and I was not rebellious, neither turned away back."
        ));
        
        return library;
    }
}

// Represents a single word in the scripture
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }
}

// Represents a scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6")
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int verse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_verse == _endVerse)
        {
            return $"{_book} {_chapter}:{_verse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
    }
}

// Represents the scripture text and reference
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        // Split the text into words
        string[] wordArray = text.Split(' ');
        foreach (string word in wordArray)
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        int wordsHidden = 0;

        // Keep trying to hide words until we've hidden the requested number
        while (wordsHidden < numberToHide && !IsCompletelyHidden())
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                wordsHidden++;
            }
        }
    }

    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText() + " ";

        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }

        return displayText;
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}