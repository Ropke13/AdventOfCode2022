using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            List<long> seeds = new List<long>();
            bool skipTillNextMap = false;

            long currentLocation = 26000000;
            long index = 26000000;
            while (true)
            {
                for (int j = input.Length-1; j > 0; j--)
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

                for(int j = 1; j < seedsStrings.Length; j += 2) 
                {
                    long from = long.Parse(seedsStrings[j]);
                    long to = (long.Parse(seedsStrings[j]) + long.Parse(seedsStrings[j + 1]));
                    if (currentLocation >= from && currentLocation < to)
                    {
                        Console.WriteLine(index);
                    }
                }

                index++;
                if(index % 1000000 == 0)
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

            for(long i = 1; i < times.Length; i++) 
            {
                var currentTime = long.Parse(times[i]);
                var currentDistance = long.Parse(distances[i]);
                var betterDistance = 0;

                for(long j = 0; j <= currentTime; j++)
                {
                    if (j * (currentTime - j) > currentDistance) betterDistance++;
                }

                score *= betterDistance;

                Console.WriteLine(score);
            }
		}
    }
}
