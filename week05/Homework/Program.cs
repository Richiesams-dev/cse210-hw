using System;

namespace Homework
{
    // Base class for all assignments
    public class Assignment
    {
        protected string _studentName;
        protected string _topic;

        public Assignment(string studentName, string topic)
        {
            _studentName = studentName;
            _topic = topic;
        }

        public string GetSummary()
        {
            return $"{_studentName} - {_topic}";
        }

        public string GetStudentName()
        {
            return _studentName;
        }
    }

    // Derived class for Math assignments
    public class MathAssignment : Assignment
    {
        private string _textbookSection;
        private string _problems;

        public MathAssignment(string studentName, string topic, string textbookSection, string problems)
            : base(studentName, topic)
        {
            _textbookSection = textbookSection;
            _problems = problems;
        }

        // Method to get homework list
        public string GetHomeworkList()
        {
            return $"Section {_textbookSection} Problems {_problems}";
        }
    }

    // Derived class for Writing assignments
    public class WritingAssignment : Assignment
    {
        private string _title;

        public WritingAssignment(string studentName, string topic, string title)
            : base(studentName, topic)
        {
            _title = title;
        }

        public string GetWritingInformation()
        {
            return $"{_title} by {_studentName}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test the base Assignment class
            Assignment simpleAssignment = new Assignment("Samuel Bennett", "Multiplication");
            Console.WriteLine(simpleAssignment.GetSummary());
            Console.WriteLine();

            // Test the MathAssignment class
            MathAssignment mathHomework = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
            
            // Test inherited method
            Console.WriteLine(mathHomework.GetSummary());
            
            // Test Math-specific method
            Console.WriteLine(mathHomework.GetHomeworkList());
            Console.WriteLine();

            // Test the WritingAssignment class
            WritingAssignment writingHomework = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
            
            // Test inherited method
            Console.WriteLine(writingHomework.GetSummary());
            
            // Test Writing-specific method
            Console.WriteLine(writingHomework.GetWritingInformation());
        }
    }
}