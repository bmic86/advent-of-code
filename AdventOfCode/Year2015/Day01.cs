using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2015
{
    public class Day01 : BaseDay
    {
        private int GetFloorChange(char symbol)
            => symbol switch
            {
                '(' => 1,
                ')' => -1,
                _ => 0,
            };

        public override ValueTask<string> Solve_1()
        {
            string input = File.ReadAllText(InputFilePath);

            int result = input.Aggregate(0, (value, next) => value + GetFloorChange(next));
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string input = File.ReadAllText(InputFilePath);

            int currentFloor = 0;
            int currentPosition = 1;
            foreach (char c in input)
            {
                currentFloor += GetFloorChange(c);

                if (currentFloor == -1)
                {
                    return new(currentPosition.ToString());
                }

                currentPosition++;
            }

            return new("No result.");
        }
    }
}
