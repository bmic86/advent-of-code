using AoCHelper;

namespace AdventOfCode.Year2015
{
    public enum Operation
    {
        Assignment,
        And,
        Or,
        LShift,
        RShift,
        Not
    }

    public class Command
    {
        public string[] Operands { get; private set; }
        public Operation Operation { get; private set; }
        public string Output { get; private set; }
        public bool IsCompleted { get; set; }

        public Command(string[] operands, Operation operation, string output)
        {
            Operands = operands;
            Operation = operation;
            Output = output;
        }

        public static Command Parse(string value)
        {
            string[] tokens = value.Split(' ');

            if (tokens.Length == 5)
            {
                string[] operands = new[] { tokens[0], tokens[2] };
                var operation = tokens[1] switch
                {
                    "AND" => Operation.And,
                    "OR" => Operation.Or,
                    "LSHIFT" => Operation.LShift,
                    "RSHIFT" => Operation.RShift,
                    _ => throw new FormatException("Cannot parse command operation.")
                };

                return new Command(operands, operation, tokens[4]);
            }
            else if (tokens.Length == 4 && tokens[0] == "NOT")
            {
                return new Command(new[] { tokens[1] }, Operation.Not, tokens[3]);
            }
            else if (tokens.Length == 3)
            {
                return new Command(new[] { tokens[0] }, Operation.Assignment, tokens[2]);
            }

            throw new FormatException("Cannot parse command.");
        }
    }

    public class Day07 : BaseDay
    {
        private ushort _firstResult = 0;

        public override ValueTask<string> Solve_1()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            _firstResult = RunCircuit(inputLines, "a");
            return new(_firstResult.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);
            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i].EndsWith("-> b"))
                {
                    inputLines[i] = $"{_firstResult} -> b";
                    break;
                }
            }

            ushort newSignal = RunCircuit(inputLines, "a");
            return new(newSignal.ToString());
        }

        private static ushort RunCircuit(string[] inputLines, string wire)
        {
            Dictionary<string, ushort> signalValues = new();

            List<Command> commands = inputLines.Select(l => Command.Parse(l)).ToList();
            while (commands.Count > 0)
            {
                foreach (Command command in commands)
                {
                    int resolvedOperands = 0;
                    foreach (string operand in command.Operands)
                    {
                        if (signalValues.ContainsKey(operand))
                        {
                            resolvedOperands++;
                        }
                        else if (ushort.TryParse(operand, out ushort numericValue))
                        {
                            signalValues[operand] = numericValue;
                            resolvedOperands++;
                        }
                    }

                    if (resolvedOperands == command.Operands.Length)
                    {
                        signalValues[command.Output] = (ushort)ExecuteOperation(signalValues, command.Operation, command.Operands);
                        command.IsCompleted = true;
                    }
                }

                commands.RemoveAll(c => c.IsCompleted);
            }

            return signalValues[wire];
        }

        private static int ExecuteOperation(Dictionary<string, ushort> signalValues, Operation operation, string[] operands)
            => operation switch
            {
                Operation.And => signalValues[operands[0]] & signalValues[operands[1]],
                Operation.Or => signalValues[operands[0]] | signalValues[operands[1]],
                Operation.LShift => signalValues[operands[0]] << signalValues[operands[1]],
                Operation.RShift => signalValues[operands[0]] >> signalValues[operands[1]],
                Operation.Not => ~signalValues[operands[0]],
                Operation.Assignment => signalValues[operands[0]],
                _ => throw new InvalidOperationException()
            };
    }
}
