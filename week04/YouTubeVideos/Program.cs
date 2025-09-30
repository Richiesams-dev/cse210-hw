using System;
using System.Collections.Generic;

public class Comment
{
    // Properties
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    // Constructor
    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public void DisplayComment()
    {
        Console.WriteLine($"  - {CommenterName}: \"{CommentText}\"");
    }
}

public class Video
{
    // Properties
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> _comments;

    // Constructor
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public void DisplayAllComments()
    {
        foreach (var comment in _comments)
        {
            comment.DisplayComment();
        }
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        DisplayAllComments();
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create first video and add comments
        Video video1 = new Video("C# Programming Tutorial", "CodeMaster", 1200);
        video1.AddComment(new Comment("Alice", "Great tutorial! Very helpful for beginners."));
        video1.AddComment(new Comment("Bob", "I learned so much from this video."));
        video1.AddComment(new Comment("Charlie", "Could you make a follow-up on advanced topics?"));
        videos.Add(video1);

        // Create second video and add comments
        Video video2 = new Video("ASP.NET Core Web Development", "WebDev Pro", 1800);
        video2.AddComment(new Comment("Diana", "Excellent explanation of MVC pattern!"));
        video2.AddComment(new Comment("Eve", "The examples were very practical."));
        video2.AddComment(new Comment("Frank", "Helped me complete my project on time."));
        video2.AddComment(new Comment("Grace", "Clear and concise delivery."));
        videos.Add(video2);

        // Create third video and add comments
        Video video3 = new Video("Entity Framework Basics", "Database Guru", 900);
        video3.AddComment(new Comment("Henry", "Finally understand EF relationships!"));
        video3.AddComment(new Comment("Ivy", "Good pacing and examples."));
        video3.AddComment(new Comment("Jack", "Would love to see more complex scenarios."));
        videos.Add(video3);

        // Create fourth video and add comments
        Video video4 = new Video("Building REST APIs with C#", "API Expert", 1500);
        video4.AddComment(new Comment("Karen", "Perfect timing for my current project."));
        video4.AddComment(new Comment("Leo", "The authentication section was very useful."));
        video4.AddComment(new Comment("Mia", "Well structured and easy to follow."));
        video4.AddComment(new Comment("Nathan", "Best API tutorial I've seen!"));
        videos.Add(video4);

        // Iterate through the list of videos and display information
        Console.WriteLine("YouTube Video Program");
        Console.WriteLine("_____________________\n");

        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}