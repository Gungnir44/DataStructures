/*
public class HashLab3
{
    private class HashNode
    {
        public string Key { get; }
        public string Value { get; }
        public HashNode Next { get; set; }

        public HashNode(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    private HashNode[] hashTable;
    private double loadFactor;
    private string probeMethod;
    private string hashMethod;

    public HashLab3(double load, string probe, string hash)
    {
        loadFactor = load;
        probeMethod = probe;
        hashMethod = hash;
        hashTable = new HashNode[128];
    }

    public void Hash()
    {
        if (hashMethod.Equals("Burris"))
        {
            if (probeMethod.Equals("Linear"))
            {
                try
                {
                    FinalHash(loadFactor, hashMethod, probeMethod);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("test");
                }
            }
            else if (probeMethod.Equals("Random"))
            {
                try
                {
                    FinalHash(loadFactor, hashMethod, probeMethod);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("test");
                }
            }
            else
            {
                Console.WriteLine("Error wrong selection");
            }
        }
        else if (hashMethod.Equals("MyHash"))
        {
            if (probeMethod.Equals("Linear"))
            {
                try
                {
                    FinalHash(loadFactor, hashMethod, probeMethod);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("test");
                }
            }
            else if (probeMethod.Equals("Random"))
            {
                try
                {
                    FinalHash(loadFactor, hashMethod, probeMethod);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("test");
                }
            }
            else
            {
                Console.WriteLine("Error wrong selection");
            }
        }
    }

    public void BurrisHash(string word, out int key)
    {
        double power = Math.Pow(2, 16);
        key = Math.Abs(
            (word[4] * 256 + word[1] + 317 + word[8]) / (int)power + word[5] - 10);
    }

    public int MyHash(string word)
    {
        int key = 0;
        long prime = 12582917;
        foreach (char c in word)
        {
            key = (key * (int)prime + c) % 128;
        }
        return key;
    }

    public void AverageWhole(int[] array)
    {
        double average = 0;
        double sum = 0;
        int temp = 0;
        for (int i = 0; i < 127; i++)
        {
            sum = sum + array[i];
            if (array[i] != 0)
            {
                temp++;
            }
        }
        average = sum / temp;
        Console.WriteLine(average);
    }

    public void FirstMinMaxAvgProbes(int[] array, int end)
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

    public int GenRandom(int t)
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

    public void Display(int[] array)
    {
        string str;
        int key = 0;
        int probes;
        Console.WriteLine(
            "*Hash Address  |    *Contents        |    *Original Hash   |    *Final Hash    |    *Probes to Store/Retrieve");
        for (int i = 0; i < 128; i++)
        {
            probes = array[i];
            if (hashTable[i] == null)
            {
                str = "empty           ";
                key = -1;
            }
            else
            {
                str = hashTable[i].Value;
                if (hashMethod.Equals("Burris"))
                {
                    BurrisHash(str, out key);
                }
                else if (hashMethod.Equals("MyHash"))
                {
                    key = MyHash(str);
                }
            }
            Console.WriteLine($"{i,-15}|    {str,-15} |    {key,-17}|    {i,-15}|    {probes,-15}");
        }
    }

    public void FinalHash(double loadFact, string hashMethod, string probeMethod)
    {
        int[] probes = new int[128];
        int[] realprobes = new int[128];
        double end = loadFact * 128;
        Console.WriteLine("\nfinalHash" + " " + hashMethod + " " + probeMethod);

        using (StreamReader sr = new StreamReader("C:/Software/DataStructures/Words200D16.txt"))
        {
            string line;
            int index = 0;
            while ((line = sr.ReadLine()) != null && index < 128 * loadFact)
            {
                int maxprobes = 1;
                if (hashMethod.Equals("Burris"))
                {
                    int key;
                    BurrisHash(line, out key);
                    if (probeMethod.Equals("Linear"))
                    {
                        while (hashTable[key] != null)
                        {
                            key = key + 1;
                            maxprobes = maxprobes + 1;
                            if (key == 128)
                            {
                                key = 0;
                            }
                        }
                        realprobes[key] = maxprobes;
                        probes[index++] = maxprobes;
                        hashTable[key] = new HashNode(key.ToString(), line);
                    }
                    else if (probeMethod.Equals("Random"))
                    {
                        int temp = key;
                        while (hashTable[temp] != null)
                        {
                            temp = (GenRandom(maxprobes) + key) % 128;
                            maxprobes = maxprobes + 1;
                        }
                        realprobes[temp] = maxprobes;
                        probes[index++] = maxprobes;
                        hashTable[temp] = new HashNode(temp.ToString(), line);
                    }
                }
                else if (hashMethod.Equals("MyHash"))
                {
                    int key = MyHash(line);
                    if (probeMethod.Equals("Linear"))
                    {
                        while (hashTable[key] != null)
                        {
                            key = key + 1;
                            maxprobes = maxprobes + 1;
                            if (key == 128)
                            {
                                key = 0;
                            }
                        }
                        realprobes[key] = maxprobes;
                        probes[index++] = maxprobes;
                        hashTable[key] = new HashNode(key.ToString(), line);
                    }
                    else if (probeMethod.Equals("Random"))
                    {
                        int temp = key;
                        while (hashTable[temp] != null)
                        {
                            temp = (GenRandom(maxprobes) + key) % 128;
                            maxprobes = maxprobes + 1;
                        }
                        realprobes[temp] = maxprobes;
                        probes[index++] = maxprobes;
                        hashTable[temp] = new HashNode(temp.ToString(), line);
                    }
                }
            }
        }
        Display(realprobes);
        Console.Write("\n\nFirst 25 keys:");
        FirstMinMaxAvgProbes(probes, 25);
        Console.Write("\n\nLast 25 keys:");
        FirstMinMaxAvgProbes(probes, (int)end);
        Console.Write("\n\nTotal Average:");
        AverageWhole(probes);
        Theoretical();
    }

    public void Theoretical()
    {
        double theo = 1 - (loadFactor / 2);
        theo = theo / (1 - loadFactor);
        Console.WriteLine("Theoretical Probes: " + theo);
    }

    public static void Main(string[] args)
    {
        HashLab3 Mylin40 = new HashLab3(.4, "Linear", "MyHash");
        HashLab3 Mylin80 = new HashLab3(.8, "Linear", "MyHash");
        HashLab3 Myrand40 = new HashLab3(.4, "Random", "MyHash");
        HashLab3 Myrand80 = new HashLab3(.8, "Random", "MyHash");
        HashLab3 Brand40 = new HashLab3(.4, "Random", "Burris");
        HashLab3 Blin40 = new HashLab3(.4, "Linear", "Burris");
        HashLab3 Brand80 = new HashLab3(.8, "Random", "Burris");
        HashLab3 Blin80 = new HashLab3(.8, "Linear", "Burris");
        Blin40.Hash();
        Brand40.Hash();
        Brand80.Hash();
        Blin80.Hash();
        Mylin40.Hash();
        Mylin80.Hash();
        Myrand40.Hash();
        Myrand80.Hash();
    }
}
*/