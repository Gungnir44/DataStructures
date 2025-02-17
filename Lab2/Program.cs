using System;
using System.Collections.Generic;
using System.IO;

// Enumeration type for JobType
public enum JobType
{
    Analysist,
    Manager,
    Accountant,
    Programmer,
    Sales
}

// Employee class representing each employee
public class Employee : IComparable<Employee>
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public JobType JobType { get; set; }

    public int CompareTo(Employee other)
    {
        // Compare by JobType
        int jobTypeComparison = this.JobType.CompareTo(other.JobType);
        if (jobTypeComparison != 0)
            return jobTypeComparison;

        // If JobType is the same, compare by Age
        int ageComparison = this.Age.CompareTo(other.Age);
        if (ageComparison != 0)
            return ageComparison;

        // If Age is the same, compare by Name
        return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
    }
}

// Doubly linked list node
public class Node
{
    public required Employee Data { get; set; }
    public Node? Next { get; set; }
    public Node? Previous { get; set; }
}

// Doubly linked list class
public class EmployeeList
{
    public Node? Head { get; set; }
    public Node? Tail { get; set; }

    public void Insert(Employee employee)
    {
        Node newNode = new Node { Data = employee };

        // If the list is empty
        if (Head == null)
        {
            Head = Tail = newNode;
            return;
        }

        Node current = Head;
        Node? previous = null;

        // Find the correct position to insert in sorted order
        while (current != null && current.Data.CompareTo(employee) < 0)
        {
            previous = current;
            current = current.Next;
        }

        // Insert the new node
        newNode.Next = current;
        newNode.Previous = previous;

        if (previous != null)
            previous.Next = newNode;
        else
            Head = newNode;

        if (current != null)
            current.Previous = newNode;
        else
            Tail = newNode;
    }

    public void Print()
    {
        Node current = Head;
        while (current != null)
        {
            Console.WriteLine($"{current.Data.Name}, {current.Data.Age}, {current.Data.JobType}");
            current = current.Next;
        }
    }

    public void PrintReverse()
    {
        Node current = Tail;
        while (current != null)
        {
            Console.WriteLine($"{current.Data.Name}, {current.Data.Age}, {current.Data.JobType}");
            current = current.Previous;
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        // ArrayList to hold heads of linked lists for each job type
        List<Node> jobTypeHeads = new List<Node>();
        for (int i = 0; i < Enum.GetNames(typeof(JobType)).Length; i++)
        {
            jobTypeHeads.Add(null);
        }

        // Read values from the "C" data file using I/O redirection
        // Assume the input file contains lines in the format: Name Age JobType
        using (StreamReader sr = new StreamReader(Console.OpenStandardInput()))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(' ');
                string name = values[0];
                JobType jobType = (JobType)Enum.Parse(typeof(JobType), values[1]);
                int age = int.Parse(values[2]);

                Employee employee = new Employee { Name = name, Age = age, JobType = jobType };

                // Insert employee into the appropriate linked list based on job type
                Node newNode = new Node { Data = employee };
                int jobTypeIndex = (int)jobType;
                if (jobTypeIndex >= 0 && jobTypeIndex < jobTypeHeads.Count)
                {
                    if (jobTypeHeads[jobTypeIndex] == null)
                    {
                        jobTypeHeads[jobTypeIndex] = newNode;
                    }
                    else
                    {
                        Node current = jobTypeHeads[jobTypeIndex];
                        Node previous = null;
                        while (current != null && current.Data.CompareTo(employee) < 0)
                        {
                            previous = current;
                            current = current.Next;
                        }

                        if (previous != null)
                            previous.Next = newNode;
                        else
                            jobTypeHeads[jobTypeIndex] = newNode;

                        newNode.Next = current;
                    }
                }
                else
                {
                    // Resize the list to accommodate the new index
                    while (jobTypeIndex >= jobTypeHeads.Count)
                    {
                        jobTypeHeads.Add(null);
                    }
                    // Now, add the new node at the correct index
                    jobTypeHeads[jobTypeIndex] = newNode;
                }

            }
        }

        // Create linked lists using the existing array list
        EmployeeList sortedList = new EmployeeList();
        foreach (var head in jobTypeHeads)
        {
            if (head != null)
            {
                sortedList.Insert(head.Data);
                Node current = head.Next;
                while (current != null)
                {
                    sortedList.Insert(current.Data);
                    current = current.Next;
                }
            }
        }

        // Print the sorted employee list
        Console.WriteLine("Printing in sorted order:");
        sortedList.Print();
        Console.WriteLine("\nPrinting in reverse order:");
        sortedList.PrintReverse();

        Console.WriteLine();
    }
}
