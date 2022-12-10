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
        public static void RandomDay()
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
    }
}
