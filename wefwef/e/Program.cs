using System;
using System.Diagnostics;

namespace e
{
    class Instruction
    {
        public string Name { get; set; }
        public List<string> Arguments { get; set; }
        public string NextInstruction { get; set; }

        public Instruction(string name, List<string> args, string next)
        {
            Name = name;
            Arguments = args;
            NextInstruction = next;
        }
    }
    internal class Program
    {
        public List<long> answers = new();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Day19();
        }
        
        static void Day19()
        {
            var input = File.ReadAllLines("Day19.txt");
            List<Instruction> instructions = new();
            List<long> answers = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "")
                {
                    break;
                }
                int indexOfOpenBrace = input[i].IndexOf('{');

                string name = indexOfOpenBrace != -1
                    ? input[i].Substring(0, indexOfOpenBrace)
                    : input[i];

                var c = input[i].Replace(name, "").Replace("{", "").Replace("}", "").Split(',').ToList();
                var last = c.Last();
                c.RemoveAt(c.Count - 1);

                Instruction item = new(name, c, last);
                instructions.Add(item);

            }

            Stopwatch stopWatch = new();
            stopWatch.Start();

            Range[] rangesArray = { 1..4001, 1..4001, 1..4001, 1..4001 };
            var first = instructions.FirstOrDefault(f => f.Name == "in");

            Process(first!, rangesArray, instructions);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts);

            Console.WriteLine("ANSWER: {0}", answers.Sum());


            void Process(Instruction instruction, Range[] ranges, List<Instruction> instructions)
            {
                foreach (var item in instruction.Arguments)
                {
                    var parts = item.Split(':');
                    if (parts[0].Contains('<'))
                    {
                        var more = parts[0].Split('<');

                        if (more[0] == "x" && int.Parse(more[1]) > ranges[0].Start.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[0] = new Range(temp[0].Start, int.Parse(more[1]));
                            ranges[0] = new Range(int.Parse(more[1]), ranges[0].End);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));
                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);

                        }
                        else if (more[0] == "m" && int.Parse(more[1]) > ranges[1].Start.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[1] = new Range(temp[1].Start, int.Parse(more[1]));
                            ranges[1] = new Range(int.Parse(more[1]), ranges[1].End);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                        else if (more[0] == "a" && int.Parse(more[1]) > ranges[2].Start.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[2] = new Range(temp[2].Start, int.Parse(more[1]));
                            ranges[2] = new Range(int.Parse(more[1]), ranges[2].End);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                        else if (more[0] == "s" && int.Parse(more[1]) > ranges[3].Start.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[3] = new Range(temp[3].Start, int.Parse(more[1]));
                            ranges[3] = new Range(int.Parse(more[1]), ranges[3].End);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                    }
                    else
                    {
                        var more = parts[0].Split('>');

                        if (more[0] == "x" && int.Parse(more[1]) < ranges[0].End.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[0] = new Range(int.Parse(more[1]) + 1, ranges[0].End);
                            ranges[0] = new Range(ranges[0].Start, int.Parse(more[1]) + 1);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                        else if (more[0] == "m" && int.Parse(more[1]) < ranges[1].End.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[1] = new Range(int.Parse(more[1]) + 1, ranges[1].End);
                            ranges[1] = new Range(ranges[1].Start, int.Parse(more[1])+1);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                        else if (more[0] == "a" && int.Parse(more[1]) < ranges[2].End.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[2] = new Range(int.Parse(more[1]) + 1, ranges[2].End);
                            ranges[2] = new Range(ranges[2].Start, int.Parse(more[1]) + 1);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                        else if (more[0] == "s" && int.Parse(more[1]) < ranges[3].End.Value)
                        {
                            Range[] temp = new Range[ranges.Length];
                            Array.Copy(ranges, temp, ranges.Length);
                            temp[3] = new Range(int.Parse(more[1]) + 1, ranges[3].End);
                            ranges[3] = new Range(ranges[3].Start, int.Parse(more[1]) + 1);

                            if (parts[1] == "A")
                            {
                                answers.Add(((long)temp[0].End.Value - (long)temp[0].Start.Value) *
                                            ((long)temp[1].End.Value - (long)temp[1].Start.Value) *
                                            ((long)temp[2].End.Value - (long)temp[2].Start.Value) *
                                            ((long)temp[3].End.Value - (long)temp[3].Start.Value));

                                continue;
                            }
                            if (parts[1] == "R")
                            {
                                continue;
                            }

                            var nextInstruction = instructions.FirstOrDefault(f => f.Name == parts[1]);

                            Process(nextInstruction!, temp, instructions);
                        }
                    }
                }

                string next = instruction.NextInstruction.ToString();

                if (next == "A")
                {
                    answers.Add(((long)ranges[0].End.Value - (long)ranges[0].Start.Value) *
                                            ((long)ranges[1].End.Value - (long)ranges[1].Start.Value) *
                                            ((long)ranges[2].End.Value - (long)ranges[2].Start.Value) *
                                            ((long)ranges[3].End.Value - (long)ranges[3].Start.Value));
                }
                else if (next != "R")
                {
                    var nextInstructions = instructions.FirstOrDefault(f => f.Name == next);
                    Process(nextInstructions!, ranges, instructions);
                }
            }
        }
    }
}