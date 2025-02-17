using System;
using System.Collections.Generic;

public class BTreeNode
{
    public List<int> Keys { get; set; }
    public List<BTreeNode> Children { get; set; }

    public BTreeNode()
    {
        Keys = new List<int>();
        Children = new List<BTreeNode>();
    }
}

public class BTree
{
    private int order;
    private BTreeNode root;

    public BTree(int order)
    {
        this.order = order;
        root = new BTreeNode();
    }

    public void Insert(int key)
    {
        try
        {
            Insert(root, key);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting key {key}: {ex.Message}");
        }
    }

    private void Insert(BTreeNode node, int key)
    {
        if (node.Keys.Count < order - 1)
        {
            // If node is not full, insert the key
            int i = 0;
            while (i < node.Keys.Count && key > node.Keys[i])
            {
                i++;
            }
            node.Keys.Insert(i, key);
        }
        else
        {
            // Split the node if necessary
            int i = 0;
            while (i < node.Keys.Count && key > node.Keys[i])
            {
                i++;
            }

            if (node.Children.Count == 0)
            {
                // If node is a leaf, split it into two halves
                node.Keys.Insert(i, key);
                int midIndex = node.Keys.Count / 2;
                int midKey = node.Keys[midIndex];

                BTreeNode leftChild = new BTreeNode();
                leftChild.Keys.AddRange(node.Keys.GetRange(0, midIndex));
                BTreeNode rightChild = new BTreeNode();
                rightChild.Keys.AddRange(node.Keys.GetRange(midIndex + 1, node.Keys.Count - midIndex - 1));

                node.Keys.Clear();
                node.Keys.Add(midKey);

                node.Children.Add(leftChild);
                node.Children.Add(rightChild);
            }
            else
            {
                // Insert into appropriate child
                if (i == node.Keys.Count)
                {
                    Insert(node.Children[node.Children.Count - 1], key);
                }
                else
                {
                    Insert(node.Children[i], key);
                }
            }
        }
    }

    public void Print()
    {
        Print(root);
    }

    private void Print(BTreeNode node)
    {
        Queue<BTreeNode> queue = new Queue<BTreeNode>();
        queue.Enqueue(node);
        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                BTreeNode current = queue.Dequeue();
                foreach (var key in current.Keys)
                {
                    Console.Write($"{key} ");
                }
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BTree bTree = new BTree(5);

        int[] numbers = { 23, 89, 67, 12, 1, 15, 107, 98, 45, 78, 247, 672, 67, 1024, 2048, 587, 1776, 4096, 33, 17, 99, 53, 51, 63, 69, 72 };

        foreach (var number in numbers)
        {
            try
            {
                bTree.Insert(number);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting key {number}: {ex.Message}");
            }
        }

        Console.WriteLine("B-Tree:");
        bTree.Print();
    }
}
