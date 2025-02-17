public class Stack
{
    private readonly int[] items;                   // Stack array
    private int top;                                // Index representation for the top of the stack

    public Stack(int size)
    {
        items = new int[size];
        top = -1;                                   // Apparently industry standard is -1 to show and empty stack. Makes sense since 0 is first in the array.
    }

    public bool IsEmpty()
    {
        return top == -1;                           // Allows for checking for empty stack for underflow
    }

    public bool IsFull()                            // Allows for checking of full stack for overflow
    {
        return top == items.Length - 1;
    }

    public void Push(int item)                      // Pushing into stack array
    {
        if (IsFull())
        {
            Console.WriteLine("Stack is full.");
            return;
        }
        items[++top] = item;
    }

    public int Pop()                                 // Removing last input from stack array
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty.");
            return -1;
        }
        return items[top--];
    }

    public static void PrintStack(Stack stack)
    {
        if (stack.IsEmpty())
        {
            Console.WriteLine("The stack is empty");
            return;
        }
        for (int i = stack.top; i >= 0; i--)        // This is so we print from the top down since stacks are FILO
        {
            Console.Write(stack.items[i] + " ");    // So it'll print "Stack after processing: 1 2 3" etc.
        }

        Console.WriteLine();
    }

    public int Search(int value)                    // Search array for number
    {
        for (int i = 0; i <= top; i++)
        {
            if (items[i] == value)
            {
                return i;                           // Return the index if the value is found
            }
        }

        return -1;
    }
    public void SortStack()
    {
        List<int> sortedList = items.Take(top + 1).ToList();
        sortedList.Sort();

        Array.Clear(items, 0, items.Length);
        top = -1;

        foreach (int item in sortedList)
        {
            Push(item);
        }

    }

    internal class Program
    {
        public static void Main()
        {
            Stack stack = new Stack(100);

            string filePath = @"c:\temp\myinput2.txt"; // Replace with the actual path to your text file, this is where he had me put the last file in Assign 1, so I put it there again

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int num = 0;
                    string pushNum;

                    while (!reader.EndOfStream)        // Continue until there is no more input
                    {
                        string input = reader.ReadLine();

                        if (input == null)
                        {
                            Console.WriteLine("Input is null. Exiting the loop.");
                            break;
                        }

                        string line = input.ToLower();      // This solution to splitting the string line in the text file is thanks to Stack Overflow

                        if (line.StartsWith("push") && line.Length > 4)
                        {
                            pushNum = line.Substring(4).Trim(); // Extract the number part after "push"

                            if (Int32.TryParse(pushNum, out num))
                            {
                                stack.Push(num);
                                Console.WriteLine($"Pushed {num}");
                            }
                        }
                        else if (line.StartsWith("pop"))
                        {
                            int poppedValue = stack.Pop();
                            if (poppedValue != -1)
                            {
                                Console.WriteLine($"Popped {poppedValue}");
                            }
                        }
                        else if (line.StartsWith("find") && line.Length > 4)
                        {
                            if (Int32.TryParse(line.Substring(4).Trim(), out num))
                            {
                                int index = stack.Search(num);
                                if (index != -1)
                                {
                                    Console.WriteLine($"Searching list for item {num}, found it in array position {index}.");
                                }
                                else
                                {
                                    Console.WriteLine($"{num} not found in the stack");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid number after 'find' in the line.");
                            }
                        }
                        else if (line.StartsWith("(stack end)") && line.Length > 8)
                        {
                            Console.Write("Stack after processing: ");
                            Stack.PrintStack(stack);
                            Console.WriteLine();
                        }
                        else if (line.StartsWith("(find end)") && line.Length > 8)
                        {
                            Console.WriteLine($"\nUnsorted Array: ");
                            Stack.PrintStack(stack);

                            stack.SortStack();                        // Sort the stack

                            Console.WriteLine($"Sorted Array: ");
                            Stack.PrintStack(stack);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}