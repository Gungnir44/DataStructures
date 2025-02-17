using System;
using System.IO;

public class Hash
{
    private double loadFactor;
    private string probeMethod;
    private string hashMethod;
    private string[] hash = new string[128];

    public Hash(double load, string probe, string hash)
    {
        this.loadFactor = load;
        this.probeMethod = probe;
        this.hashMethod = hash;
    }

    public void BurrisRandomHash()
    {
        FinalHash("Burris", "Random");
    }

    public void BurrisLinearHash()
    {
        FinalHash("Burris", "Linear");
    }

    public void MyHash()
    {
        FinalHash("MyHash", "");
    }

    public void Hashing()
    {
        if (hashMethod.Equals("Burris"))
        {
            if (probeMethod.Equals("Linear"))
            {
                BurrisLinearHash();
            }
            else if (probeMethod.Equals("Random"))
            {
                BurrisRandomHash();
            }
            else
            {
                Console.WriteLine("Error wrong selection");
            }
        }
        else if (hashMethod.Equals("MyHash"))
        {
            MyHash();
        }
    }

    private static int Slice(string str, int start, int end)
    {
        if (start < 0)
        {
            start = 0;
        }
        if (end > str.Length)
        {
            end = str.Length;
        }
        if (start >= end)
        {
            return 0;
        }
        return str[start] + str[end];
    }

    private static int BurrisHashFunction(string word)
    {
        int key = 0;

        if (word.Length >= 10)
        {
            int slice1 = Slice(word, 0, 1);
            int slice2 = Slice(word, 2, 3);
            int slice3 = word[4];
            int slice4 = word[5];
            int slice5 = word[6];
            int slice6 = word[9];

            double power = Math.Pow(2, 10);

            key = (int)(((slice1 / 256.0 + slice2 / 277.0 + slice3) / power +
                    slice4 / 313.0 + slice5 / 3.0 + slice6) % 128);
        }
        return key;
    }

    private int MyHash(string word)
    {
        int key = 0;
        long prime = 12582917;
        for (int i = 0; i < word.Length; i++)
        {
            key = (key * (int)prime + word[i]) % 128;
        }
        return key;
    }

    private void FinalHash(string hashMethod, string probeMethod)
    {
        int[] probes = new int[128];
        int[] realprobes = new int[128];
        double end = loadFactor * 128;

        using (FileStream fs = new FileStream("c:/Software/DataStructures/Words200D16.txt", FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                int i = 0;
                while (!reader.EndOfStream && i <= 127 * loadFactor)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        int maxprobes = 1;
                        int key = 0;

                        if (hashMethod.Equals("Burris"))
                        {
                            key = BurrisHashFunction(line);
                        }
                        else if (hashMethod.Equals("MyHash"))
                        {
                            key = MyHash(line);
                        }

                        while (true)
                        {
                            if (maxprobes == 128)
                            {
                                Console.WriteLine("Hash table is full.");
                                break;
                            }

                            if (hash[key] == null)
                            {
                                hash[key] = line;
                                realprobes[key] = maxprobes;
                                probes[i] = maxprobes;
                                break;
                            }

                            break;
                        }

                        i++;
                    }
                }
            }
        }

        void DisplayHashTableContents(int[] array)
        {
            Console.WriteLine("*Hash Address  |    *Contents        |    *Original Hash   |    *Final Hash    |    *Probes to Store/Retrieve");
            for (int i = 0; i < 128; i++)
            {
                string str;
                int key = 0;
                int slotProbes = 0; // Initialize slotProbes to 0

                if (hash[i] == null)
                {
                    str = "empty           ";
                    key = -1;
                }
                else
                {
                    str = hash[i];
                    if (hashMethod.Equals("Burris"))
                    {
                        key = BurrisHashFunction(str);
                    }
                    else if (hashMethod.Equals("MyHash"))
                    {
                        key = MyHash(str);
                    }
                    // Assign a value to slotProbes when the slot is not empty
                    slotProbes = array[i];
                }
                Console.WriteLine($"{i,-15}|    {str,-15} |    {key,-17}|    {i,-15}|    {slotProbes,-15}");
            }
        }

        void FirstMinMaxAvgProbes(int[] array, int end)
        {
            int max = -1, min = 800000000;
            int sum = 0;
            double average = 0;
            for (int i = end - 25; i < end; i++)
            {
                sum = sum + array[i];
                if (array[i] > max)
                {
                    max = array[i];
                }
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            average = sum / 25.0;
            Console.WriteLine();
            Console.WriteLine("Minimum probes is: " + min);
            Console.WriteLine("Maximum probes is: " + max);
            Console.WriteLine("Average probes is: " + average);
        }

        int GenRandom(int t)
        {
            int temp = (int)Math.Pow(2, 9);
            int r = 1;
            int q = 0;
            int p = 0;
            while (p != t)
            {
                r = r * 5;
                r = r % temp;
                q = r / 4;
                p++;
            }
            return q;
        }

        void Display(int[] array)
        {
            string str;
            int key = 0;
            int probes;
            Console.WriteLine("*Hash Address  |    *Contents        |    *Original Hash   |    *Final Hash    |    *Probes to Store/Retrieve");
            for (int i = 0; i < 128; i++)
            {
                probes = array[i];
                if (hash[i] == null)
                {
                    str = "empty           ";
                    key = -1;
                }
                else
                {
                    str = hash[i];
                    if (hashMethod.Equals("Burris"))
                    {
                        key = BurrisHashFunction(str);
                    }
                    else if (hashMethod.Equals("MyHash"))
                    {
                        key = MyHash(str);
                    }
                }
                Console.WriteLine($"{i,-15}|    {str,-15} |    {key,-17}|    {i,-15}|    {probes,-15}");
            }
        }

        void Theoretical()
        {
            double theo = 1 - (loadFactor / 2);
            theo = theo / (1 - loadFactor);
            Console.WriteLine("Theoretical Probes: " + theo);
        }
    }
    public class HashLab
    {
        public static void Main(string[] args)
        {
            // Create instances of Hash and perform operations here
            Hash Mylin86 = new Hash(.86, "Linear", "MyHash");
            Hash Myrand86 = new Hash(.86, "Random", "MyHash");
            Hash Brand86 = new Hash(.86, "Random", "Burris");
            Hash Blin86 = new Hash(.86, "Linear", "Burris");

            // Call the Hashing method on each instance to perform hashing operations
            Console.WriteLine("Burris Linear Hash:");
            Blin86.Hashing();     // Burris Linear Hash

            Console.WriteLine("\nBurris Random Hash:");
            Brand86.Hashing();    // Burris Random Hash

            Console.WriteLine("\nMy Linear Hash:");
            Mylin86.Hashing();    // My Linear Hash

            Console.WriteLine("\nMy Random Hash:");
            Myrand86.Hashing();   // My Random Hash
        }
    }
}
