using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day08 : BaseDay
    {
        public override ValueTask<string> Solve_1()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            int codeLiteralsLength = 0;
            int dataLength = 0;
            foreach (string line in inputLines)
            {
                bool isEscapedChar = false;

                // Ignoring first and last double quote (") characters,
                // as they are not counted to data length.
                for (int i = 1; i < line.Length - 1; i++)
                {
                    if (isEscapedChar)
                    {
                        if (line[i] == 'x')
                        {
                            i += 2;
                        }
                        isEscapedChar = false;
                        continue;
                    }

                    if (line[i] == '\\')
                    {
                        isEscapedChar = true;
                    }

                    dataLength++;
                }

                codeLiteralsLength += line.Length;
            }

            return new((codeLiteralsLength - dataLength).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            int codeLiteralsLength = 0;
            int newlyEncodedLength = 0;
            foreach (string line in inputLines)
            {
                newlyEncodedLength += line.Aggregate(2, (length, value) => length + GetEscapedCharLength(value));
                codeLiteralsLength += line.Length;
            }

            return new((newlyEncodedLength - codeLiteralsLength).ToString());
        }

        private static int GetEscapedCharLength(char c)
            => (c == '\\' || c == '\"') ? 2 : 1;
    }
}