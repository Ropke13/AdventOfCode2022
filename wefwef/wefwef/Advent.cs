﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace wefwef
{
    class Advent
    {
        public class Directory
        {
            public string Dir { get; set; }
            public long Size { get; set; }

            public Directory(string dir, int size)
            {
                Dir = dir;
                Size = size;
            }
        }

        public static void Day7()
        {
            string line;
            List<string> path = new List<string>();
            List<Directory> dirs = new List<Directory>();
            Directory item;

            using (StreamReader read = new StreamReader("input7.txt"))
            {
                while ((line = read.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (line.Contains("$ cd"))
                    {
                        if (parts[2] == "..")
                        {
                            path.RemoveAt(path.Count - 1);
                            continue;
                        }

                        path.Add(parts[2]);
                        string j = "";
                        foreach (var i in path)
                        {
                            j = j + "`" + i;
                        }

                        dirs.Add(item = new Directory(j, 0));
                    }
                    else if (long.TryParse(parts[0], out long value))
                    {
                        for (int i = path.Count - 1; i >= 0; i--)
                        {
                            string current = "";
                            for (int j = 0; j <= i; j++)
                            {
                                current = current + "`" + path[j];
                            }
                            dirs.Where(f => f.Dir == current).Select(f => f.Size += value).ToList();
                        }
                    }
                }
                //part1
                //var dirsSmall = dirs.Where(f => f.Size <= 100000).ToList();
                //Console.WriteLine(dirsSmall.Sum(f => f.Size));

                //part2
                var answer = dirs.Select(f => f.Size).ToList();
                answer.Sort();
                foreach (var j in answer)
                {
                    if (70000000 - answer[answer.Count - 1] + j > 30000000)
                    {
                        Console.WriteLine(j);
                        break;
                    }
                }
            }

            Console.ReadLine();
        }

        public static void Day8()
        {
            int[,] input = new int[99, 99];

            using (StreamReader read = new StreamReader("input8.txt"))
            {
                string line;
                int hight = 0;
                while((line = read.ReadLine()) != null)
                {
                    for(int i = 0; i < line.Length; i++)
                    {
                        input[hight,i] = int.Parse(line[i].ToString());
                    }
                    hight++;
                }
            }

            int top = 0;
            int right = 0;
            int bot = 0;
            int left = 0;

            int score = 0;
            int answer = 0;

            for(int i = 1; i < 98; i++)
            {
                for(int j = 1; j < 98; j++)
                {
                    int treeValue = int.Parse(input[i, j].ToString());

                    //Check top
                    for(int x = i-1; x >= 0; x--)
                    {
                        if (int.Parse(input[x, j].ToString()) >= treeValue)
                        {
                            top++;
                            break;
                        }
                        else top++;
                    }

                    //Check Right
                    for (int x = j+1; x < 99; x++)
                    {
                        if (int.Parse(input[i, x].ToString()) >= treeValue)
                        {
                            right++;
                            break;
                        }
                        else right++;
                    }

                    //Check Left
                    for (int x = j-1; x >= 0; x--)
                    {
                        if (int.Parse(input[i, x].ToString()) >= treeValue)
                        {
                            left++;
                            break;
                        }
                        else left++;
                    }


                    //Check Bot
                    for (int x = i+1; x < 99; x++)
                    {
                        if (int.Parse(input[x, j].ToString()) >= treeValue)
                        {
                            bot++;
                            break;
                        }
                        else bot++;
                    }

                    score = top * left * right * bot;

                    if(score > answer)
                    {
                        answer = score;
                    }

                    top = 0;
                    bot = 0;
                    left = 0;
                    right = 0;
                }
            }

            Console.WriteLine(answer);
        }

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Move(string direction)
            {
                if (direction == "U") Y += 1;
                else if (direction == "D") Y -= 1;
                else if (direction == "L") X -= 1;
                else if (direction == "R") X += 1;
            }

            public void Follow(Point target)
            {
                if (Math.Abs(target.X - X) <= 1 && Math.Abs(target.Y - Y) <= 1) return;

                int stepX = target.X - X;
                int stepY = target.Y - Y;

                X += Math.Sign(stepX);
                Y += Math.Sign(stepY);
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }
        public static void Day9()
        {
            int ropelength = 10;

            var lines = File.ReadAllLines("input9.txt");
            var visited = new HashSet<string>();

            var segments = Enumerable.Range(0, ropelength).Select(_ => new Point(0, 0)).ToArray();
            var Head = segments[0];
            var Tail = segments[--ropelength];

            foreach(var line in lines)
            {
                var direction = line.Split(' ')[0];
                var places = int.Parse((line.Split(' ')[1]));

                for(int i = 0; i < places; i++)
                {
                    Head.Move(direction);
                    for(int j = 1; j < segments.Length; j++)
                    {
                        segments[j].Follow(segments[j-1]);
                    }

                    visited.Add(Tail.ToString());
                }
            }

            Console.WriteLine(visited.Count);
        }

        public static void Day10()
        {
            var lines = File.ReadAllLines("input10.txt");

            List<string> drawing = new List<string>();
            List<int> ints = new List<int>();

            int currentCycle = 1;
            int amount = 0;
            int wait = 0;
            int answer = 1;

            foreach (var line in lines)
            {
                if (line.Contains("addx"))
                {
                    amount = int.Parse(line.Split(' ')[1]);
                    wait = 2;
                }
                if (line == "noop")
                {
                    amount = 0;
                    wait = 1;
                }
                for (int i = 1; i <= wait; i++)
                {
                    if (currentCycle == 41)
                    {
                        currentCycle = 1;
                    }
                    if (currentCycle-1 >= answer-1 && answer+1 >= currentCycle-1)
                    {
                        drawing.Add("#");
                    }
                    else
                    {
                        drawing.Add(" ");
                    }
                    if (i == wait) answer += amount;
                    currentCycle++;
                }
            }

            int count = 1;
            foreach (var draw in drawing)
            {
                if(draw == "#")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(draw);
                if (count % 40 == 0)
                {
                    Console.WriteLine();
                }
                count++;
                Console.ForegroundColor = ConsoleColor.White;
            }
            //Console.WriteLine(ints.Sum());
        }

        public static void Day11()
        {

        }
    }
}
