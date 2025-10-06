using System;
using System.Security.Cryptography.X509Certificates;

class Program
{

    Public class person
    {
        public string GetName()
        {
            return "Richman";
        }

    }

    public class student : person
    {
        public string GetId()
        {
            return "12345678";
        }
    }

    Student student = new Student();
    string name = student.GetName();
    Console.WriteLine(name);

}
