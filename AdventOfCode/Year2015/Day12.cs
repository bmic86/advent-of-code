using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day12 : BaseDay
    {
        public class State
        {
            public Stack<int> ObjectStartPositions { get; set; } = new Stack<int>();
            public int BufferPosition { get; set; }
            public bool IsInvalidObject { get; set; }
            public int InvalidObjectLevel { get; set; }
        }

        private const string InvalidValue = ":\"red\"";

        private readonly char[] _separators = new[] { ',', '{', '}', '[', ']', ':' };

        private readonly string _inputData;

        public Day12() => _inputData = File.ReadAllText(InputFilePath);

        public override ValueTask<string> Solve_1()
            => new(SumAllNumbers(_inputData).ToString());

        public override ValueTask<string> Solve_2()
        {
            string input = RemoveInvalidData(_inputData);
            return new(SumAllNumbers(input).ToString());
        }

        private static string RemoveInvalidData(string inputData)
        {
            var state = new State();
            var buffer = new Span<char>(new char[inputData.Length]);

            for (int i = 0; i < inputData.Length; ++i)
            {
                char c = inputData[i];
                switch (c)
                {
                    case '{':
                        ObjectStart(state);
                        break;
                    case '}':
                        ObjectEnd(state);
                        break;
                    case ':':
                        PropertyValueStart(state, IsInvalid(inputData, i));
                        break;
                };

                buffer[state.BufferPosition] = c;
                state.BufferPosition++;
            }

            return buffer.Slice(0, state.BufferPosition).ToString();
        }

        private static void PropertyValueStart(State state, bool isInvalid)
        {
            if (isInvalid)
            {
                state.IsInvalidObject = true;
            };
        }

        private static bool IsInvalid(string inputData, int i)
        {
            int end = i + InvalidValue.Length;
            if (end < inputData.Length)
            {
                return inputData[i..end] == InvalidValue;
            }

            return false;
        }

        private static void ObjectStart(State state)
        {
            state.ObjectStartPositions.Push(state.BufferPosition);

            if (state.IsInvalidObject)
            {
                state.InvalidObjectLevel++;
            }
        }

        private static void ObjectEnd(State state)
        {
            int startPosition = state.ObjectStartPositions.Pop();

            if (state.IsInvalidObject)
            {
                if (state.InvalidObjectLevel == 0)
                {
                    state.BufferPosition = startPosition;
                    state.IsInvalidObject = false;
                    return;
                }

                state.InvalidObjectLevel--;
            }
        }

        public int SumAllNumbers(string inputData)
            => inputData
                .Split(_separators, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(0, (sum, current) =>
                {
                    int.TryParse(current, out int value);
                    return sum + value;
                });
    }
}
