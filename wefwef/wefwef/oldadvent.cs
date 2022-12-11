using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wefwef
{
    class oldadvent
    {
        //check which day it was and rename!
        public static void A2020Day3()
        {
            string[,] input = new string[323, 31];
            List<int> output = new List<int>();
            long answer = 1;

            using (StreamReader read = new StreamReader("input.txt"))
            {
                string line;
                int hight = 0;
                while ((line = read.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        input[hight, i] = line[i].ToString();
                    }
                    hight++;
                }
            }
            //output.Add(SlopeFind(input, 1, 1));
            //output.Add(SlopeFind(input, 3, 1));
            output.Add(SlopeFind(input, 1, 1));
            //output.Add(SlopeFind(input, 7, 1));
            //output.Add(SlopeFind(input, 1, 2));

            foreach (var i in output)
            {
                answer *= i;
            }

            Console.WriteLine(answer);
        }

        public static int SlopeFind(string[,] input, int X, int Y)
        {
            int x = 0;
            int y = 0;
            int answer = 0;

            while (y < 322)
            {
                for (int h = 1; h <= Y; h++)
                {
                    if (y + 1 > 322)
                    {
                        continue;
                    }
                    y++;
                    if (y == 322)
                    {
                        Console.WriteLine("bottom rached");
                    }
                }

                for (int j = 1; j <= X; j++)
                {
                    if (x + 1 > 30)
                    {
                        x = 0;
                        continue;
                    }
                    x++;
                }

                if (input[y, x] == "#")
                {
                    answer++;
                }

                for (int j = 0; j < 31; j++)
                {
                    if (j == x)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (input[y, j] == "#")
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write("O");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    Console.Write(input[y, j]);
                }
                Console.WriteLine();
                Thread.Sleep(500);
            }

            return answer;
        }


        public class Node
        {
            public string NodeName { get; set; }
            public int Value { get; set; }

            public Node (string nodename, int value)
            {
                NodeName = nodename;
                Value = value;
            }
        }
        public static void A2015Day7()
        {
            var input = File.ReadAllLines("2015-07.txt");

            List<string> instructions = new List<string>();
            List<string> usedItems = new List<string>();
            List<Node> nodes = new List<Node>();
            int count = 0;

            foreach(var item in input)
            {
                instructions.Add(item);
            }

            while(count != instructions.Count)
            {
                foreach (var item in instructions)
                {
                    if (usedItems.Contains(item))
                    {
                        continue;
                    }
                    string[] number = item.Split(' ');
                    if (item.Contains("AND"))
                    {
                        if (int.TryParse(number[0], out int valu) && nodes.Exists(f => f.NodeName == number[2]))
                        {
                            int value1 = valu;
                            int value2 = nodes.SingleOrDefault(f => f.NodeName == number[2]).Value;

                            Node n = new Node(number[4], value1 & value2);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                        else if (nodes.Where(f => f.NodeName == number[0] || f.NodeName == number[2]).ToList().Count == 2)
                        {
                            int value1 = nodes.SingleOrDefault(f => f.NodeName == number[0]).Value;
                            int value2 = nodes.SingleOrDefault(f => f.NodeName == number[2]).Value;

                            Node n = new Node(number[4], value1 & value2);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                    }
                    else if (item.Contains("OR"))
                    {
                        if (int.TryParse(number[0], out int valu) && nodes.Exists(f => f.NodeName == number[2]))
                        {
                            int value1 = valu;
                            int value2 = nodes.SingleOrDefault(f => f.NodeName == number[2]).Value;

                            Node n = new Node(number[4], value1 | value2);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                        else if (nodes.Where(f => f.NodeName == number[0] || f.NodeName == number[2]).ToList().Count == 2)
                        {
                            int value1 = nodes.SingleOrDefault(f => f.NodeName == number[0]).Value;
                            int value2 = nodes.SingleOrDefault(f => f.NodeName == number[2]).Value;

                            Node n = new Node(number[4], value1 | value2);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                    }
                    else if (item.Contains("LSHIFT"))
                    {
                        if (nodes.Exists(f => f.NodeName == number[0]))
                        {
                            int value1 = nodes.SingleOrDefault(f => f.NodeName == number[0]).Value;
                            int value2 = int.Parse(number[2]);

                            Node n = new Node(number[4], value1 << value2);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                    }
                    else if (item.Contains("RSHIFT"))
                    {
                        if (nodes.Exists(f => f.NodeName == number[0]))
                        {
                            int value1 = nodes.SingleOrDefault(f => f.NodeName == number[0]).Value;
                            int value2 = int.Parse(number[2]);

                            Node n = new Node(number[4], value1 >> value2);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                    }
                    else if (item.Contains("NOT"))
                    {
                        if(nodes.Exists(f => f.NodeName == number[1]))
                        {
                            int value1 = nodes.SingleOrDefault(f => f.NodeName == number[1]).Value;

                            Node n = new Node(number[3], ~value1);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                    }
                    else if (number.Length == 3)
                    {
                        if (nodes.Exists(f => f.NodeName == number[0]))
                        {
                            Node n = new Node(number[2], nodes.SingleOrDefault(f => f.NodeName == number[0]).Value);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                        else if (int.TryParse(number[0], out int value))
                        {
                            Node n = new Node(number[2], value);
                            nodes.Add(n);
                            count++;
                            usedItems.Add(item);
                        }
                    }
                }
            }

            var answer = nodes.SingleOrDefault(f => f.NodeName == "a").Value;
            Console.WriteLine(answer);

        }
    }
}
