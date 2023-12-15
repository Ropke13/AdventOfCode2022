using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wefwef
{
    internal class Advent2023
    {
        public static void Day1()
        {
            var input = File.ReadAllLines("input1-2023.txt");
            List<string> numberStrings = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            int numberCount = 0;
            string first = "";
            string last = "";
            int sum = 0;

            foreach (var line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (Char.IsNumber(line[i]))
                    {
                        if (numberCount == 0)
                        {
                            first = line[i].ToString();
                            last = line[i].ToString();
                            numberCount++;
                        }
                        if (numberCount > 0)
                        {
                            last = line[i].ToString();
                        }
                    }
                    else
                    {
                        if (numberStrings.Any(s => line.Substring(i).StartsWith(s)))
                        {
                            string foundNumberString = FindNumbers(line, i, numberStrings);
                            if (!string.IsNullOrEmpty(foundNumberString))
                            {
                                int foundNumber = numberStrings.IndexOf(foundNumberString) + 1;

                                if (numberCount == 0)
                                {
                                    first = foundNumber.ToString();
                                    last = foundNumber.ToString();
                                    numberCount++;
                                }
                                if (numberCount > 0)
                                {
                                    last = foundNumber.ToString();
                                }
                            }
                        }
                    }
                }

                sum += int.Parse(first + last);
                numberCount = 0;
            }

            string FindNumbers(string text, int index, List<string> numberStringi)
            {
                foreach (string numberString in numberStringi)
                {
                    if (text.Substring(index).StartsWith(numberString))
                    {
                        return numberString;
                    }
                }
                return "";
            }

            Console.WriteLine(sum);
        }

        public static void Day2()
        {
            var input = File.ReadAllLines("input2-2023.txt");
            int sum = 0;

            int lowestBlue = int.MinValue;
            int lowestRed = int.MinValue;
            int lowestGreen = int.MinValue;

            foreach (var line in input)
            {
                string[] removeGame = line.Split(':');
                string[] gameSets = removeGame[1].Split(';');

                for (int i = 0; i < gameSets.Length; i++)
                {
                    string[] games = gameSets[i].Split(',');
                    for (int j = 0; j < games.Length; j++)
                    {
                        string[] numberColor = games[j].Split(' ');

                        if (numberColor[2] == "blue")
                        {
                            if (int.Parse(numberColor[1]) > lowestBlue)
                            {
                                lowestBlue = int.Parse(numberColor[1]);
                            }
                        }
                        else if (numberColor[2] == "red")
                        {
                            if (int.Parse(numberColor[1]) > lowestRed)
                            {
                                lowestRed = int.Parse(numberColor[1]);
                            }
                        }
                        else
                        {
                            if (int.Parse(numberColor[1]) > lowestGreen)
                            {
                                lowestGreen = int.Parse(numberColor[1]);
                            }
                        }
                    }
                }

                sum += lowestBlue * lowestGreen * lowestRed;

                lowestBlue = int.MinValue;
                lowestRed = int.MinValue;
                lowestGreen = int.MinValue;
            }

            Console.WriteLine(sum.ToString());
        }

        public class NumberNode
        {
            public string Number { get; set; }
            public Tuple<int, int> Gear { get; set; }
            public NumberNode(string number, Tuple<int, int> gear)
            {
                Number = number;
                Gear = gear;
            }
        }

        public static void Day3()
        {
            var input = File.ReadAllLines("input3-2023.txt");
            string currentNumber = "";
            int lineNumber = 0;
            bool addNumber = true;
            int sum = 0;
            List<NumberNode> numbers = new List<NumberNode>();
            Tuple<int, int> forGear = null;

            foreach (var line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (Char.IsNumber(line[i]))
                    {
                        currentNumber += line[i];

                        if (lineNumber != 0)
                        {
                            if (input[lineNumber - 1][i] == '*' && !Char.IsNumber(input[lineNumber - 1][i]))
                            {
                                addNumber = false;
                                forGear = Tuple.Create(lineNumber - 1, i);
                            }

                            if (i != line.Length - 1)
                            {
                                if (input[lineNumber - 1][i + 1] == '*' && !Char.IsNumber(input[lineNumber - 1][i + 1]))
                                {
                                    addNumber = false;
                                    forGear = Tuple.Create(lineNumber - 1, i + 1);
                                }
                            }
                        }

                        if (lineNumber != input.Length - 1)
                        {
                            if (input[lineNumber + 1][i] == '*' && !Char.IsNumber(input[lineNumber + 1][i]))
                            {
                                addNumber = false;
                                forGear = Tuple.Create(lineNumber + 1, i);
                            }

                            if (i != line.Length - 1 && i != 0)
                            {
                                if (input[lineNumber + 1][i - 1] == '*' && !Char.IsNumber(input[lineNumber + 1][i - 1]))
                                {
                                    addNumber = false;
                                    forGear = Tuple.Create(lineNumber + 1, i - 1);
                                }
                            }
                        }

                        if (i != 0)
                        {
                            if (line[i - 1] == '*' && !Char.IsNumber(line[i - 1]))
                            {
                                addNumber = false;
                                forGear = Tuple.Create(lineNumber, i - 1);
                            }

                            if (lineNumber != 0)
                            {
                                if (input[lineNumber - 1][i - 1] == '*' && !Char.IsNumber(input[lineNumber - 1][i - 1]))
                                {
                                    addNumber = false;
                                    forGear = Tuple.Create(lineNumber - 1, i - 1);
                                }
                            }
                        }


                        if (i != input.Length - 1)
                        {
                            if (line[i + 1] == '*' && !Char.IsNumber(line[i + 1]))
                            {
                                addNumber = false;
                                forGear = Tuple.Create(lineNumber, i + 1);
                            }

                            if (lineNumber != input.Length - 1)
                            {
                                if (input[lineNumber + 1][i + 1] == '*' && !Char.IsNumber(input[lineNumber + 1][i + 1]))
                                {
                                    addNumber = false;
                                    forGear = Tuple.Create(lineNumber + 1, i + 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentNumber != string.Empty && !addNumber)
                        {
                            NumberNode item = new NumberNode(currentNumber, forGear);
                            numbers.Add(item);

                            currentNumber = string.Empty;
                            addNumber = true;
                        }
                        else
                        {
                            currentNumber = string.Empty;
                            addNumber = true;
                        }
                    }
                }
                lineNumber++;
            }

            var sortedNumbers = numbers.OrderBy(f => f.Gear).ToList();

            for (int i = 0; i < sortedNumbers.Count - 1; i++)
            {
                if (sortedNumbers[i].Gear.ToString() == sortedNumbers[i + 1].Gear.ToString())
                {
                    sum += int.Parse(sortedNumbers[i].Number) * int.Parse(sortedNumbers[i + 1].Number);
                    //Console.WriteLine("{0}-{1}     {2}-{3}    {4}", sortedNumbers[i].Number, sortedNumbers[i].Gear.ToString(), sortedNumbers[i + 1].Number, sortedNumbers[i + 1].Gear.ToString(), sum);
                }
            }

            Console.WriteLine(sum);
        }

        public static void Day4()
        {
            var input = File.ReadAllLines("input4-2023.txt");

            List<int> CardLine = new List<int>();

            for (int i = 1; i <= input.Length; i++)
            {
                CardLine.Add(i);
            }

            int winCount = 0;
            int totalGames = 0;

            while (CardLine.Count > 0)
            {
                int currentGame = CardLine[0];
                CardLine.RemoveAt(0);
                totalGames++;

                string[] splitgame = input[currentGame - 1].Split(new[] { ": " }, StringSplitOptions.None);
                string[] splitWinAndCurrNumbers = splitgame[1].Split(new[] { " | " }, StringSplitOptions.None);
                string[] winningNumbers = splitWinAndCurrNumbers[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                string[] myNumbers = splitWinAndCurrNumbers[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < winningNumbers.Length; i++)
                {
                    for (int j = 0; j < myNumbers.Length; j++)
                    {
                        if (winningNumbers[i] == myNumbers[j])
                        {
                            winCount++;
                        }
                    }
                }

                for (int j = winCount; j > 0; j--)
                {
                    if (j + currentGame <= input.Length)
                    {
                        CardLine.Insert(0, j + currentGame);
                    }
                }

                winCount = 0;
            }

            Console.WriteLine(totalGames);
        }

        public static void Day5()
        {
            var input = File.ReadAllLines("input5-2023.txt");
            string[] seedsStrings = input[0].Split(' ');
            bool skipTillNextMap = false;

            long currentLocation = 26000000;
            long index = 26000000;
            while (true)
            {
                for (int j = input.Length - 1; j > 0; j--)
                {
                    if (input[j].Contains('-'))
                    {
                        j--;
                        skipTillNextMap = false;
                        continue;
                    }
                    else
                    {
                        string[] numbers = input[j].Split();
                        long temp = long.Parse(numbers[0]) - long.Parse(numbers[1]);
                        temp = currentLocation - temp;

                        if (temp >= long.Parse(numbers[1]) && temp < long.Parse(numbers[1]) + long.Parse(numbers[2]) && !skipTillNextMap)
                        {
                            currentLocation = temp;
                            skipTillNextMap = true;
                        }
                    }
                }

                for (int j = 1; j < seedsStrings.Length; j += 2)
                {
                    long from = long.Parse(seedsStrings[j]);
                    long to = (long.Parse(seedsStrings[j]) + long.Parse(seedsStrings[j + 1]));
                    if (currentLocation >= from && currentLocation < to)
                    {
                        Console.WriteLine(index);
                    }
                }

                index++;
                if (index % 1000000 == 0)
                {
                    Console.WriteLine(index);
                }
                currentLocation = index;
            }
        }

        public static void Day6()
        {
            var input = File.ReadAllLines("input6-2023.txt");
            long score = 1;

            string[] times = input[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string[] distances = input[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            for (long i = 1; i < times.Length; i++)
            {
                var currentTime = long.Parse(times[i]);
                var currentDistance = long.Parse(distances[i]);
                var betterDistance = 0;

                for (long j = 0; j <= currentTime; j++)
                {
                    if (j * (currentTime - j) > currentDistance) betterDistance++;
                }

                score *= betterDistance;

                Console.WriteLine(score);
            }
        }

        public class Hand
        {
            public string Cards { get; set; }
            public int HandType { get; set; }
            public int Score { get; set; }
            public Hand(string hand, int handType, int score)
            {
                Cards = hand;
                HandType = handType;
                Score = score;
            }
        }

        public static void Day7()
        {
            var input = File.ReadAllLines("input7-2023.txt");

            List<Hand> items = new List<Hand>();

            foreach (var line in input)
            {
                string[] parts = line.Split();
                Console.WriteLine("Current hand {0}", parts[0]);
                Hand item = null;
                if (Is5Kind(parts[0]))
                {
                    item = new Hand(parts[0], 7, int.Parse(parts[1]));
                }
                else if (Is4Kind(parts[0]))
                {
                    item = new Hand(parts[0], 6, int.Parse(parts[1]));
                }
                else if (IsFullHouse(parts[0]))
                {
                    item = new Hand(parts[0], 5, int.Parse(parts[1]));
                }
                else if (Is3Kind(parts[0]))
                {
                    item = new Hand(parts[0], 4, int.Parse(parts[1]));
                }
                else if (Is2Pair(parts[0]))
                {
                    item = new Hand(parts[0], 3, int.Parse(parts[1]));
                }
                else if (Is1Pair(parts[0]))
                {
                    item = new Hand(parts[0], 2, int.Parse(parts[1]));
                }
                else
                {
                    item = new Hand(parts[0], 1, int.Parse(parts[1]));
                }

                items.Add(item);


            }
            List<char> value = new List<char>() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
            Console.Clear();
            var sorted = items.OrderBy(f => f.HandType)
                              .ThenBy(f => value.IndexOf(f.Cards[0]))
                              .ThenBy(f => value.IndexOf(f.Cards[1]))
                              .ThenBy(f => value.IndexOf(f.Cards[2]))
                              .ThenBy(f => value.IndexOf(f.Cards[3]))
                              .ThenBy(f => value.IndexOf(f.Cards[4]))
                              .ToList();
            int answer = 0;

            for (int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine("Current hand: {0}, handType: {1}", sorted[i].Cards, sorted[i].HandType);
                answer += sorted[i].Score * (i + 1);
            }

            Console.Write(answer);

            bool Is5Kind(string hand)
            {
                List<char> list = hand.ToList();
                int count = list.Count(c => c == 'J');
                if (count == 5) return true;
                list.Sort();
                var g = list.GroupBy(i => i);
                foreach (var grp in g)
                {
                    if (grp.Key != 'J' && grp.Count() + count == 5)
                    {
                        return true;
                    }
                }
                return false;
            }
            bool Is4Kind(string hand)
            {
                List<char> list = hand.ToList();
                int count = list.Count(c => c == 'J');
                list.Sort();
                var g = list.GroupBy(i => i);
                foreach (var grp in g)
                {
                    if (grp.Key != 'J' && grp.Count() + count == 4)
                    {
                        return true;
                    }
                }
                return false;
            }

            bool IsFullHouse(string hand)
            {
                List<char> list = new List<char>();
                int count = list.Count(c => c == 'J');
                if (count > 1)
                {
                    return false;
                }
                for (int i = 0; i < hand.Length; i++)
                {
                    if (!list.Contains(hand[i]) && hand[i] != 'J')
                    {
                        list.Add(hand[i]);
                    }
                }

                if (list.Count > 2) return false;

                return true;
            }
            bool Is3Kind(string hand)
            {
                List<char> list = hand.ToList();
                int count = list.Count(c => c == 'J');
                list.Sort();
                var g = list.GroupBy(i => i);
                foreach (var grp in g)
                {
                    if (grp.Key != 'J' && grp.Count() + count == 3)
                    {
                        return true;
                    }
                }
                return false;
            }
            bool Is2Pair(string hand)
            {
                int pairs = 0;
                List<char> list = hand.ToList();
                int count = list.Count(c => c == 'J');
                if (count > 0)
                {
                    return false;
                }
                list.Sort();
                var g = list.GroupBy(i => i);
                foreach (var grp in g)
                {
                    if (grp.Count() == 2)
                    {
                        pairs++;
                    }
                }

                if (pairs == 2) return true;

                return false;
            }
            bool Is1Pair(string hand)
            {
                int pairs = 0;
                List<char> list = hand.ToList();
                int count = list.Count(c => c == 'J');
                if (count > 0)
                {
                    return true;
                }
                list.Sort();
                var g = list.GroupBy(i => i);
                foreach (var grp in g)
                {
                    if (grp.Count() == 2)
                    {
                        pairs++;
                    }
                }

                if (pairs == 1) return true;

                return false;
            }
        }
        class Node
        {
            public string Location { get; set; }
            public string LeftNode { get; set; }
            public string RightNode { get; set; }

            public Node(string location, string leftNode, string rigthNode)
            {
                Location = location;
                LeftNode = leftNode;
                RightNode = rigthNode;
            }
        }
        public static void Day8()
        {
            var input = File.ReadAllLines("input8-2023.txt");

            List<Char> Instructions = input[0].ToList();
            List<Node> paths = new List<Node>();

            bool foundZZZ = false;

            for (int i = 2; i < input.Length; i++)
            {
                string[] parts = input[i].Split(new[] { " = " }, StringSplitOptions.None);
                parts[1] = parts[1].Substring(1, parts[1].Length - 2);
                string[] leftRight = parts[1].Split(new[] { ", " }, StringSplitOptions.None);

                Node item = new Node(parts[0], leftRight[0], leftRight[1]);

                paths.Add(item);
            }

            int index = 0;
            List<Node> curentNodes = paths.Where(f => f.Location[2] == 'A').ToList();
            string nextLocation;
            int totalSteps = 0;
            List<long> steps = new List<long>();

            foreach (Node node in curentNodes)
            {
                Node temp = node;
                index = 0;
                totalSteps = 0;
                while (!foundZZZ)
                {
                    if (index > Instructions.Count - 1) index = 0;
                    Char currentInstruction = Instructions[index];

                    if (currentInstruction == 'L')
                    {
                        totalSteps++;
                        nextLocation = temp.LeftNode;
                    }
                    else
                    {
                        totalSteps++;
                        nextLocation = temp.RightNode;
                    }

                    temp = paths.FirstOrDefault(f => f.Location == nextLocation);

                    if (nextLocation[2] == 'Z') break;
                    index++;
                }

                steps.Add(totalSteps);
            }
        }

        public static void Day9()
        {
            var input = File.ReadAllLines("input9-2023.txt");

            long sum = 0;

            foreach(var line in input)
            {
                string[] parts = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine(line);

                List<long> temp = new List<long>();
                List<List<long>> steps = new List<List<long>>();
                bool isAllZero = false;
                int index = 0;

                foreach (var part in parts)
                {
                    temp.Add(long.Parse(part));
                }

                steps.Add(temp);

                while (!isAllZero)
                {
                    List<long> nextLine = new List<long>();
                    isAllZero = true;
                    for (int i = 0; i < steps[index].Count-1; i++)
                    {
                        nextLine.Add(steps[index][i + 1] - steps[index][i]);
                        if(steps[index][i + 1] - steps[index][i] != 0) isAllZero = false;
                    }

                    steps.Add(nextLine);
                    index++;
                }

                int returnI = steps.Count-1;
                List<long> currentLine = steps[returnI];
                currentLine.Insert(0, 0);

                while (returnI > 0)
                {
                    List<long> next = steps[returnI-1];
                    next.Insert(0, next.First() - steps[returnI].First());
                    returnI--;
                }

                sum += steps[0].First();
            }

            Console.WriteLine(sum);
        }

        class Head
        {
            public int x { get; set; }
            public int y { get; set; }

            public Head(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        internal static void Day10()
        {
            var input = File.ReadAllLines("input10-2023.txt").ToArray();
            Head StartingPossition = null;
            List<Head> HeadList = new List<Head>();

            for (int y = 0; y < input.Length; y++) 
            {
                for(int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        StartingPossition = new Head(x, y);
                        break;
                    }
                }
                if (StartingPossition != null) break;
            }

            HeadList.Add(StartingPossition);

            int Y = StartingPossition.y;
            int X = StartingPossition.x;
            char[] toNorth = new char[] { '|', 'L', 'J', 'S' };
            char[] toShouth = new char[] { '|', '7', 'F', 'S' };
            char[] toWest = new char[] { '-', 'J', '7', 'S' };
            char[] toEast = new char[] { '-', 'F', 'L', 'S' };

            char CP = 'S';

            while (true)
            {
                if (Y != 0 && (input[Y - 1][X] == '|' || input[Y - 1][X] == 'F' || input[Y - 1][X] == '7') && !HeadList.Any(f => f.y == Y - 1 && f.x == X) && toNorth.Contains(CP))
                {
                    Y--;
                    Head found = new Head(X, Y);
                    HeadList.Add(found);
                    CP = input[Y][X];
                }
                else if (Y != input.Length - 1 && (input[Y + 1][X] == '|' || input[Y + 1][X] == 'L' || input[Y + 1][X] == 'J') && !HeadList.Any(f => f.y == Y + 1 && f.x == X) && toShouth.Contains(CP))
                {
                    Y++;
                    Head found = new Head(X, Y);
                    HeadList.Add(found);
                    CP = input[Y][X];
                }

                else if (X != 0 && (input[Y][X - 1] == '-' || input[Y][X - 1] == 'F' || input[Y][X - 1] == 'L') && !HeadList.Any(f => f.y == Y && f.x == X - 1) && toWest.Contains(CP))
                {
                    X--;
                    Head found = new Head(X, Y);
                    HeadList.Add(found);
                    CP = input[Y][X];
                }

                else if (X != input[Y].Length - 1 && (input[Y][X + 1] == '-' || input[Y][X + 1] == '7' || input[Y][X + 1] == 'J') && !HeadList.Any(f => f.y == Y && f.x == X + 1) && toEast.Contains(CP))
                {
                    X++;
                    Head found = new Head(X, Y);
                    HeadList.Add(found);
                    CP = input[Y][X];
                }
                else
                {
                    List<Head> inLoopTile = new List<Head>();
                    int inLoopTiles = 0;
                    char[] check = new char[] { '|', 'J', 'L', 'S' };                  

                    for (int k = 0; k < input.Length; k++)
                    {
                        for (int l = 0; l < input[k].Length; l++)
                        {
                            if(!HeadList.Any(f => f.y == k && f.x == l))
                            {
                                List<Head> valid = new List<Head>();
                                var count = HeadList.Where(f => f.y == k && f.x <= l).ToList();
                                foreach (var c in count)
                                {
                                    if (check.Contains(input[c.y][c.x]))
                                    {
                                        valid.Add(c);
                                    }
                                }

                                if(valid.Count != 0 && valid.Count % 2 != 0)
                                {
                                    inLoopTiles++;
                                    Head item = new Head(l, k);
                                    inLoopTile.Add(item);
                                }
                            }
                        }
                    }
                    Draw(input, inLoopTile);
                    Console.WriteLine("Total steps: {0}.,    Halfway: {1}.", HeadList.Count, HeadList.Count / 2);
                    Console.WriteLine("Total Closed in Loop Tiles: {0}.", inLoopTiles);
                    break;
                }
            }

            void Draw(string[] inputa, List<Head> inl)
            {
                for (int y = 0; y < inputa.Length; y++)
                {
                    for(int x = 0; x < inputa[y].Length; x++)
                    {
                        if (HeadList.Any(f => f.y == y && f.x == x))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        } 
                        if (HeadList.Last().y == y && HeadList.Last().x == x)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        if (inputa[y][x] == 'S')
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                        if (inl.Any(f => f.y == y && f.x == x)){
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }

                        if(inputa[y][x] == 'L')
                        {
                            Console.Write("└");
                        }
                        else if (inputa[y][x] == 'J')
                        {
                            Console.Write("┘");
                        }
                        else if (inputa[y][x] == '7')
                        {
                            Console.Write("┐");
                        }
                        else if (inputa[y][x] == 'F')
                        {
                            Console.Write("┌");
                        }
                        else if (input[y][x] == '-')
                        {
                            Console.Write("─");
                        }
                        else if (input[y][x] == '|')
                        {
                            Console.Write("│");
                        }
                        else
                        {
                            Console.Write(inputa[y][x]);
                        }
                        
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine();
                }
            }
        }

        class Galaxy
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public Galaxy(int id, int x, int y)
            {
                Id = id;
                X = x;
                Y = y;
            }
        }
        internal static void Day11()
        {
            var input = File.ReadAllLines("input11-2023.txt").ToArray();
            List<Galaxy> galaxies = new List<Galaxy>();

            int id = 1;
            int xCrossings = 0;
            int yCrossings = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[i].Length; j++)
                {
                    if (!input[i].Contains('#'))
                    {
                        yCrossings++;
                        break;
                    }
                    else if (input[i][j] == '#')
                    {
                        Galaxy item = new Galaxy(id++, j - xCrossings + (xCrossings * 1000000), i - yCrossings + (yCrossings * 1000000));
                        galaxies.Add(item);
                    }
                    else
                    {
                        bool isEmpty = true;
                        for(int x = 0; x < input.Length; x++)
                        {
                            if (input[x][j] == '#')
                            {
                                isEmpty = false;
                                break;
                            }
                        }
                        if (isEmpty)
                        {
                            xCrossings++;
                        }
                    }
                }

                xCrossings = 0;
            }

            long sum = 0;
            for (int i = 0; i < galaxies.Count -1 ; i++)
            {
                for (int j = i+1; j < galaxies.Count; j++)
                {
                    Console.WriteLine("{0}   {1}", galaxies[i].Id, galaxies[j].Id);
                    long distance = Math.Abs(Math.Abs(galaxies[j].X - galaxies[i].X) + Math.Abs(galaxies[j].Y - galaxies[i].Y));
                    sum += distance;
                }
            }

            Console.WriteLine(sum);
        }

        internal static void Day13()
        {
            var input = File.ReadAllLines("input13-2023.txt").ToList();
            List<string> rows = new List<string>();

            List<string> linesHorizontal = new List<string>();
            List<string> linesVertical = new List<string>();

            int sumHor = 0;
            int sumVert = 0;
            int current = 0;

            foreach (string line in input)
            {
                if (line != string.Empty)
                {
                    rows.Add(line);
                }
                else
                {
                    bool foundMAtch = false;
                    int first = 0;
                    int second = 10000;
                    for(int i = 0; i < rows.Count-1; i++)
                    {
                        if (rows[i] == rows[i + 1])
                        {
                            current = i + 1;
                            first = i;
                            second = i+1;
                            foundMAtch = true;
                        }
                        else if (foundMAtch)
                        {
                            while (true)
                            {
                                first--;
                                second++;

                                if ((first < 0 || second > rows.Count-1) && rows[first] == rows[second])
                                {
                                    sumHor += current;
                                    break;
                                }
                                else if ((first < 0 || second > rows[0].Length) && rows[first] != rows[second])
                                {
                                    foundMAtch = false;

                                    break;
                                }
                            }
                        }
                        
                    }

                    
                }
            }

        }

        class Rock
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Rock(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        public static void Day14()
        {
            Console.WriteLine("Reading file");
            var input = File.ReadAllLines("input14-2023.txt");
            List<Rock> rocksRound = new List<Rock>();
            List<Rock> rocksSta = new List<Rock>();
            int loadIndex = input.Length;
            int forEastMax = input[0].Length - 1;
            int sum = 0;

            Console.WriteLine("Collecting rocks");
            for(int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 'O')
                    {
                        Rock item = new Rock(j, i);
                        rocksRound.Add(item);
                    }
                    if (input[i][j] == '#')
                    {
                        Rock item = new Rock(j, i);
                        rocksSta.Add(item);
                    }
                }
            }

            Console.WriteLine("Sort round rocks for north");
            var rocksRoundSorted = rocksRound.OrderBy(r => r.X).ThenBy(r => r.Y).ToList();

            for(int k = 1; k <= 1000; k++)
            {
                //north
                for (int i = 0; i < rocksRoundSorted.Count; i++)
                {
                    int currentX = rocksRoundSorted[i].X;
                    if (rocksRoundSorted[i].Y == 0) continue;

                    else if (rocksSta.Where(f => f.Y < rocksRoundSorted[i].Y && f.X == currentX).Count() > 0 || rocksRoundSorted.Where(f => f.Y < rocksRoundSorted[i].Y && f.X == currentX).Count() > 0)
                    {
                        int one = 0;
                        int second = 0;

                        var hastag = rocksSta.Where(f => f.Y < rocksRoundSorted[i].Y && f.X == currentX).ToList();
                        if (hastag.Any())
                        {
                            one = hastag.Max(f => f.Y);
                        }

                        var ous = rocksRoundSorted.Where(f => f.Y < rocksRoundSorted[i].Y && f.X == currentX).ToList();
                        if (ous.Any())
                        {
                            second = ous.Max(f => f.Y);
                        }

                        var answer = Math.Max(one, second);

                        rocksRoundSorted[i].Y = answer + 1;
                    }
                    else
                    {
                        rocksRoundSorted[i].Y = 0;
                    }
                }


                //west
                rocksRoundSorted = rocksRoundSorted.OrderBy(r => r.Y).ThenBy(r => r.X).ToList();
                for (int i = 0; i < rocksRoundSorted.Count; i++)
                {
                    int currentX = rocksRoundSorted[i].Y;
                    if (rocksRoundSorted[i].X == 0) continue;

                    else if (rocksSta.Where(f => f.X < rocksRoundSorted[i].X && f.Y == currentX).Count() > 0 || rocksRoundSorted.Where(f => f.X < rocksRoundSorted[i].X && f.Y == currentX).Count() > 0)
                    {
                        int one = 0;
                        int second = 0;

                        var hastag = rocksSta.Where(f => f.X < rocksRoundSorted[i].X && f.Y == currentX).ToList();
                        if (hastag.Any())
                        {
                            one = hastag.Max(f => f.X);
                        }

                        var ous = rocksRoundSorted.Where(f => f.X < rocksRoundSorted[i].X && f.Y == currentX).ToList();
                        if (ous.Any())
                        {
                            second = ous.Max(f => f.X);
                        }

                        var answer = Math.Max(one, second);

                        rocksRoundSorted[i].X = answer + 1;
                    }
                    else
                    {
                        rocksRoundSorted[i].X = 0;
                    }
                }

                //south
                rocksRoundSorted = rocksRound.OrderBy(r => r.X).ThenByDescending(r => r.Y).ToList();

                for (int i = 0; i < rocksRoundSorted.Count; i++)
                {
                    int currentX = rocksRoundSorted[i].X;
                    if (rocksRoundSorted[i].Y == loadIndex - 1) continue;

                    else if (rocksSta.Where(f => f.Y > rocksRoundSorted[i].Y && f.X == currentX).Count() > 0 || rocksRoundSorted.Where(f => f.Y > rocksRoundSorted[i].Y && f.X == currentX).Count() > 0)
                    {
                        int one = loadIndex - 1;
                        int second = loadIndex - 1;

                        var hastag = rocksSta.Where(f => f.Y > rocksRoundSorted[i].Y && f.X == currentX).ToList();
                        if (hastag.Any())
                        {
                            one = hastag.Min(f => f.Y);
                        }

                        var ous = rocksRoundSorted.Where(f => f.Y > rocksRoundSorted[i].Y && f.X == currentX).ToList();
                        if (ous.Any())
                        {
                            second = ous.Min(f => f.Y);
                        }

                        var answer = Math.Min(one, second);

                        rocksRoundSorted[i].Y = answer - 1;
                    }
                    else
                    {
                        rocksRoundSorted[i].Y = loadIndex - 1;
                    }
                }

                //east
                rocksRoundSorted = rocksRoundSorted.OrderBy(r => r.Y).ThenByDescending(r => r.X).ToList();
                for (int i = 0; i < rocksRoundSorted.Count; i++)
                {
                    int currentX = rocksRoundSorted[i].Y;
                    if (rocksRoundSorted[i].X == forEastMax) continue;

                    else if (rocksSta.Where(f => f.X > rocksRoundSorted[i].X && f.Y == currentX).Count() > 0 || rocksRoundSorted.Where(f => f.X > rocksRoundSorted[i].X && f.Y == currentX).Count() > 0)
                    {
                        int one = forEastMax;
                        int second = forEastMax;

                        var hastag = rocksSta.Where(f => f.X > rocksRoundSorted[i].X && f.Y == currentX).ToList();
                        if (hastag.Any())
                        {
                            one = hastag.Min(f => f.X);
                        }

                        var ous = rocksRoundSorted.Where(f => f.X > rocksRoundSorted[i].X && f.Y == currentX).ToList();
                        if (ous.Any())
                        {
                            second = ous.Min(f => f.X);
                        }

                        var answer = Math.Min(one, second);

                        rocksRoundSorted[i].X = answer - 1;
                    }
                    else
                    {
                        rocksRoundSorted[i].X = forEastMax;
                    }
                }

                Console.WriteLine(k);
            }


            foreach (var f in rocksRoundSorted)
            {
                sum += loadIndex - f.Y;
            }
            Console.WriteLine(sum);
        }

        internal static void Day15()
        {
            var input = File.ReadAllLines("input15-2023.txt");
            List<string> items = input[0].Split(',').ToList();
            List<List<string>> boxes = new List<List<string>>();

            for(int i = 0; i < 256; i++)
            {
                boxes.Add(new List<string>());
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in items)
            {
                //Console.WriteLine("Current item: {0}", item);

                if (item.Contains('-'))
                {
                    string[] toHash = item.Split('-');
                    var location = HASH(toHash[0]);
                    boxes[location].RemoveAll(box => box.Contains(toHash[0]));
                }
                else
                {
                    string[] toHash = item.Split('=');
                    var location = HASH(toHash[0]);
                    bool containsLabel = boxes[location].Any(it => it.Contains(toHash[0]));

                    if (containsLabel)
                    {
                        int index = boxes[location].FindIndex(box => box.Contains(toHash[0]));
                        boxes[location].RemoveAt(index);
                        boxes[location].Insert(index, toHash[0] + " " + toHash[1]);
                    }
                    else
                    {
                        boxes[location].Add(toHash[0] + " " + toHash[1]);
                    }                    
                }
            }

            int sum = 0;
            int ii = 1;
            int slot = 1;
            foreach(var item in boxes)
            {
                foreach (var box in item)
                {
                    string[] split = box.Split(' ');
                    sum += ii * slot * int.Parse(split[1]);
                    //Console.WriteLine("Box: {2}, Box value: {0}, Total sum: {1}", ii * slot * int.Parse(split[1]), sum, box);

                    slot++;
                }
                ii++;
                slot = 1;
            }
            stopWatch.Stop();
            Console.WriteLine("ANSWER: {0}", sum);

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + ts.TotalMilliseconds);

            int HASH(string item)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(item);
                int currentValue = 0;
                foreach (byte b in asciiBytes)
                {
                    currentValue = (currentValue + b) * 17 % 256;
                }

                return currentValue;
            }
        }
    }
}
