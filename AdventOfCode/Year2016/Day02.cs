using AoCHelper;

namespace AdventOfCode.Year2016
{
    public class Day02 : BaseDay
    {
        private readonly string[] _instructions;

        public Day02()
            => _instructions = File.ReadAllLines(InputFilePath);

        public override ValueTask<string> Solve_1()
            => new(FindBathroomCode(GetKeyCodeForSimpleKeypad));

        public override ValueTask<string> Solve_2()
            => new(FindBathroomCode(GetKeyCodeForComplexKeypad));

        private string FindBathroomCode(Func<int, string, int> getKeyCode)
        {
            List<string> keyCodes = new();

            int keyCode = 5;
            foreach (var instruction in _instructions)
            {
                keyCode = getKeyCode(keyCode, instruction);
                keyCodes.Add($"{keyCode:X}");
            }

            return string.Concat(keyCodes);
        }

        private static int GetKeyCodeForSimpleKeypad(int currentKeyCode, string instruction)
        {
            foreach (char c in instruction)
            {
                currentKeyCode = c switch
                {
                    'U' => SimpleKeypadMoveUp(currentKeyCode),
                    'D' => SimpleKeypadMoveDown(currentKeyCode),
                    'L' => SimpleKeypadMoveLeft(currentKeyCode),
                    'R' => SimpleKeypadMoveRight(currentKeyCode),
                    _ => throw new InvalidOperationException($"Invalid instruction code `{c}`"),
                };
            }

            return currentKeyCode;
        }

        private static int SimpleKeypadMoveUp(int keyCode)
        {
            int newKeyCode = keyCode - 3;
            return (newKeyCode > 0) ? newKeyCode : keyCode;
        }

        private static int SimpleKeypadMoveDown(int keyCode)
        {
            int newKeyCode = keyCode + 3;
            return (newKeyCode <= 9) ? newKeyCode : keyCode;
        }

        private static int SimpleKeypadMoveLeft(int keyCode)
            => (keyCode % 3 != 1) ? keyCode - 1 : keyCode;

        private static int SimpleKeypadMoveRight(int keyCode)
            => (keyCode % 3 != 0) ? keyCode + 1 : keyCode;

        private static int GetKeyCodeForComplexKeypad(int currentKeyCode, string instruction)
        {
            foreach (char c in instruction)
            {
                currentKeyCode = c switch
                {
                    'U' => ComplexKeypadMoveUp(currentKeyCode),
                    'D' => ComplexKeypadMoveDown(currentKeyCode),
                    'L' => ComplexKeypadMoveLeft(currentKeyCode),
                    'R' => ComplexKeypadMoveRight(currentKeyCode),
                    _ => throw new InvalidOperationException($"Invalid instruction code `{c}`"),
                };
            }

            return currentKeyCode;
        }

        private static int ComplexKeypadMoveUp(int keyCode)
        {
            if (keyCode == 5 || keyCode == 9)
            {
                return keyCode;
            }

            int step = (keyCode == 0xD || keyCode == 3) ? 2 : 4;
            int newKeyCode = keyCode - step;
            return (newKeyCode >= 1) ? newKeyCode : keyCode;
        }

        private static int ComplexKeypadMoveDown(int keyCode)
        {
            if (keyCode == 5 || keyCode == 9)
            {
                return keyCode;
            }

            int step = (keyCode == 0xB || keyCode == 1) ? 2 : 4;
            int newKeyCode = keyCode + step;
            return (newKeyCode <= 0xD) ? newKeyCode : keyCode;
        }

        private static int ComplexKeypadMoveLeft(int keyCode)
        {
            if (keyCode != 9 && IsEdgeKey(keyCode))
            {
                return keyCode;
            }

            int newKeyCode = keyCode - 1;
            return (newKeyCode == 5 || !IsEdgeKey(newKeyCode)) ? newKeyCode : keyCode;
        }

        private static int ComplexKeypadMoveRight(int keyCode)
        {
            if (keyCode != 5 && IsEdgeKey(keyCode))
            {
                return keyCode;
            }

            int newKeyCode = keyCode + 1;
            return (newKeyCode == 9 || !IsEdgeKey(newKeyCode)) ? newKeyCode : keyCode;
        }

        // Returns true for keys: 1, 5, 9 and D (0xD == 13).
        private static bool IsEdgeKey(int keyCode)
            => keyCode % 4 == 1;
    }
}
