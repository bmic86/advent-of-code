// See https://aka.ms/new-console-template for more information
using AoCHelper;

if (args.Length > 0)
{
    if (string.Compare(args[0], "all", true) == 0)
    {
        Console.WriteLine("Solving all problems...");
        Solver.SolveAll(new SolverConfiguration
        {
            ShowConstructorElapsedTime = true,
            ShowTotalElapsedTimePerDay = true,
            ClearConsole = false,
        });
    }
}
else
{
    Console.WriteLine("Solving last problem...");
    Solver.SolveLast(new SolverConfiguration
    {
        ShowConstructorElapsedTime = true,
        ShowTotalElapsedTimePerDay = true,
        ClearConsole = false,
    });
}
