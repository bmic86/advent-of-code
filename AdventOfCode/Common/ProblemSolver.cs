using AoCHelper;

namespace AdventOfCode.Common
{
    public class ProblemSolver
    {
        private readonly Action<SolverConfiguration> _settingsAction = (options) =>
        {
            options.ShowConstructorElapsedTime = true;
            options.ShowTotalElapsedTimePerDay = true;
            options.ClearConsole = false;
        };

        public async Task RunAsync(string[] applicationArgs)
        {
            if (applicationArgs.Length > 0)
            {
                if (string.Compare(applicationArgs[0], "all", true) == 0)
                {
                    Console.WriteLine("Solving all problems...");
                    await Solver.SolveAll(_settingsAction);
                }
                else
                {
                    var problemNumbers = applicationArgs
                        .Select(uint.Parse)
                        .ToArray();

                    Console.WriteLine($"Solving problem for day(s): {string.Join(',', applicationArgs)}...");
                    await Solver.Solve(_settingsAction, problemNumbers);
                }
            }
            else
            {
                Console.WriteLine("Solving last problem...");
                await Solver.SolveLast(_settingsAction);
            }
        }
    }
}