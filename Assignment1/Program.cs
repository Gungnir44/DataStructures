using System;
using System.IO;

class Assignment1
{
    static void Main()
    {
        try
        {
            // File path for myinput.txt, the provided document is required for Assignment 1
            string filePath = @"c:\temp\myinput.txt";

            // Should read lines from the filePath
            string[] lines = File.ReadAllLines(filePath);

            // Pulls information from myinput.txt hopefully... most of this is from book and from Geek2Geek.com
            // Used var since I wanted to simulate not knowing what was in the file
            var firstName = lines[0];
            var lastName = lines[1];
            var birthplace = lines[2];
            var countryOfBirth = lines[3];
            var capital = lines[4];
            var gdp = decimal.Parse(lines[5]);
            var population = 100000;
            var friendName = lines[6];
            var friendAge = byte.Parse(lines[7]);
            long widgets = 30000;
            var friendWidgets = new[] { int.Parse(lines[8]), int.Parse(lines[9]), int.Parse(lines[10]), int.Parse(lines[11]) };  // Note: did not work well for the widgets... had to try multiple time before getting it to work
            var anotherFriendAge = byte.Parse(lines[12]);
            var stringAsNumber1 = lines[13];
            var stringAsNumber2 = lines[14];

            // Should print out just like example from Assignment one
            string output =
                $"Hi, my name is {firstName} {lastName}, and I was born in {birthplace}. " +
                $"It is polite to write my name like this: ({firstName.ToUpper()} {lastName.ToUpper()}). " +
                $"My name is huge; it is {firstName.Length + lastName.Length} characters long!\n \n" +
                $"Many of my friends were born in the country of {countryOfBirth}. Its capital is {capital}, " +
                $"but people call it: {capital.Replace("Para", "")}.\n \n" +
                $"My country is very wealthy, with a GDP of {gdp:C}. We have {population} citizens, " +
                $"and each generates {gdp / population:C} of the GDP output. Each of us produces " +
                $"30,000 widgets a year, for a total of {widgets * population} widgets per year!\n" +
                $"Many of the world’s population of {WorldPopulation} people will buy them!\n \n" +
                $"Here is my friend, {friendName}, her age: {friendAge}, and how many widgets she produced per quarter last year:\n\n" +
                $"Name: {friendName} Age: {friendAge} Q1: {friendWidgets[0]} Q2: {friendWidgets[1]} Q3: {friendWidgets[2]} Q4: {friendWidgets[3]}. " +
                $"Total: {friendWidgets.Sum()}.\n \n" +
                $"Here is another friend, and he is {anotherFriendAge} years old. They are a total of " +
                $"{(int)friendAge + anotherFriendAge} years old.\n \n" +
                $"Is this string {stringAsNumber1} a number? {(int.TryParse(stringAsNumber1, out int result1) ? result1.ToString() : "No, didn't convert")}.\n" +
                $"Is this string {stringAsNumber2} a number? {(int.TryParse(stringAsNumber2, out int result2) ? result2.ToString() : "No, didn't convert")}.\n";
            Console.WriteLine(output);
        }
        // Should catch exceptions...
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        // Actor tuples consisting of Name, Age, and a Movie. Which are all great movies btw
        var actor1 = System.Tuple.Create("Keanu Reeves", 57, "The Matrix");
        var actor2 = System.Tuple.Create("Mel Gibson", 68, "The Patriot");
        var actor3 = System.Tuple.Create("Bruce Willis", 68, "Die Hard");

        Console.WriteLine($"Actor 1: {actor1}");
        Console.WriteLine($"Actor 2: {actor2}");
        Console.WriteLine($"Actor 3: {actor3}\n");

        // numbers typed at random, because why not
        var tuple1 = System.Tuple.Create(44, 4);
        var tuple2 = System.Tuple.Create(11, 345);

        // Tuple addition and multiplication
        Console.WriteLine($"The sum and product of number tuples 1: {tuple1} and 2: {tuple2} are {tuple1.Item1 + tuple1.Item2 + tuple2.Item1 +tuple2.Item2} and {tuple1.Item1 * tuple1.Item2 * tuple2.Item1 * tuple2.Item2}.\n");

        // A string array of large dog breeds courtesy of Google
        string[] dogBreeds = { "Alaskan Malamute", "German Shepherd", "Golden Retriever", "Great Pyrenees", "Bernese Mountain Dog", "Great Dane", "Rottweiler", "Dobermann", "Irish Wolfhound" };

        // Prints full array
        Console.WriteLine($"My Dogs: {string.Join(", ", dogBreeds)}\n");

        // Prints just 3-5 from the array
        Console.WriteLine($"The 3rd through 5th dogs: {string.Join(", ", dogBreeds[2..5])}");
    }

    static long WorldPopulation => 7800000000; //approximate world pop
}
