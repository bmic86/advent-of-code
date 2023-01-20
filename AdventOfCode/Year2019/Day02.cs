using AoCHelper;
using System;

namespace AdventOfCode.Year2019
{
    public class Day02 : BaseDay
    {
        private readonly int[] _program;
        private readonly int[] _memory;

        public Day02()
        {
            _program = LoadData();
            _memory = new int[_program.Length];
        }

        public override ValueTask<string> Solve_1()
        {
            int output = RunProgram(12, 2);
            return new(output.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            const int maxValue = 99;
            const int expectedOutput = 19690720;

            for (int noun = 0; noun <= maxValue; ++noun)
            {
                for (int verb = 0; verb <= maxValue; ++verb)
                {
                    int output = RunProgram(noun, verb);
                    if (output == expectedOutput)
                    {
                        int result = 100 * noun + verb;
                        return new(result.ToString());
                    }
                }
            }

            return new("No result.");
        }

        private int RunProgram(int noun, int verb)
        {
            _program.CopyTo(_memory, 0);

            _memory[1] = noun;
            _memory[2] = verb;

            bool isRunning = true;
            for (int i = 0; isRunning; i += 4)
            {
                switch (_memory[i])
                {
                    case 1:
                        _memory[_memory[i + 3]] = _memory[_memory[i + 1]] + _memory[_memory[i + 2]];
                        break;
                    case 2:
                        _memory[_memory[i + 3]] = _memory[_memory[i + 1]] * _memory[_memory[i + 2]];
                        break;
                    case 99:
                        isRunning = false;
                        break;
                };
            }

            return _memory[0];
        }

        private int[] LoadData()
            => File.ReadAllText(InputFilePath)
                .Split(',')
                .Select(int.Parse)
                .ToArray();
    }
}
