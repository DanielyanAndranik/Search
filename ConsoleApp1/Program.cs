using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static Dictionary<string, LinkedList<int>> dictionary;

        static void Main(string[] args)
        {
            var texts = ReadAllDocs();
            var set = SplitAndStore(texts);
            set.Sort((c, n) => c.Key.CompareTo(n.Key));
            dictionary = Merge(set);
            while (true)
            {
                Console.WriteLine("Input two words");
                var word1 = Console.ReadLine();
                if(!dictionary.ContainsKey(word1))
                {
                    Console.WriteLine($"Couldn't find {word1}");
                    continue;
                }
                var word2 = Console.ReadLine();
                if (!dictionary.ContainsKey(word2))
                {
                    Console.WriteLine($"Couldn't find {word2}");
                    continue;
                }
                var node1 = dictionary[word1].First;
                var node2 = dictionary[word2].First;
                foreach (var index in Intersect(node1, node2))
                {
                    Console.Write($"{index}   ");
                }
            }
        }

        static List<string> ReadAllDocs()
        {
            var texts = new List<string>();
            string input = Console.ReadLine();
            while (input != "q")
            {
                texts.Add(input);
                input = Console.ReadLine();
            }
            return texts;
        }

        static List<KeyValuePair<string, int>> SplitAndStore(List<string> texts)
        {
            var set = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < texts.Count; i++)
            {
                foreach (var word in texts[i].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    set.Add(new KeyValuePair<string, int>(word, i + 1));
                }
            }
            return set;
        }

        static Dictionary<string, LinkedList<int>> Merge(List<KeyValuePair<string, int>> words)
        {
            var dict = new Dictionary<string, LinkedList<int>>();
            var current = words[0].Key;
            for (int i = 0; i < words.Count; i++)
            {
                dict.Add(current, new LinkedList<int>());
                while (i < words.Count && current == words[i].Key)
                {
                    if (dict[current].Last == null || dict[current].Last.Value != words[i].Value)
                        dict[current].AddLast(words[i].Value);
                    i++;
                }
                if (i != words.Count)
                {
                    current = words[i].Key;
                    i--;
                }
            }
            return dict;
        }

        static List<int> Intersect(LinkedListNode<int> node1, LinkedListNode<int> node2)
        {
            var result = new List<int>();
            while(node1 != null && node2 != null)
            {
                if (node1.Value == node2.Value)
                {
                    result.Add(node1.Value);
                    node1 = node1.Next;
                    node2 = node2.Next;
                }
                else
                if (node1.Value < node2.Value)
                    node1 = node1.Next;
                else
                    node2 = node2.Next;
            }

            return result;
        }

    }
}
