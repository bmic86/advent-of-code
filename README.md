# Advent of Code

My Advent of Code puzzles solutions implemented in C#.

I'm using [AoCHelper](https://github.com/eduherminio/AoCHelper) support library to organize and run my code.

## Current progress

[[2022]](https://adventofcode.com/2022) &nbsp;&nbsp;&nbsp; 4 ⭐

[[2021]](https://adventofcode.com/2021) &nbsp;&nbsp;&nbsp; 8 ⭐

[[2020]](https://adventofcode.com/2020) &nbsp;&nbsp;&nbsp; 8 ⭐

[[2019]](https://adventofcode.com/2019) &nbsp;&nbsp;&nbsp; 8 ⭐

[[2018]](https://adventofcode.com/2018) &nbsp;&nbsp;&nbsp; 8 ⭐

[[2017]](https://adventofcode.com/2017) &nbsp;&nbsp;&nbsp; 8 ⭐

[[2016]](https://adventofcode.com/2016) &nbsp;&nbsp;&nbsp; 8 ⭐

[[2015]](https://adventofcode.com/2015) &nbsp; 37 ⭐

**Total** &nbsp;&nbsp;&nbsp; 89 ⭐

---

## How to run it?

Every event year has its own `.csproj` which builds a separate console application.

Executing one of them without any parameters, like this:
```bat
.\AdventOfCode.Year2022.exe
```
Runs only results for the latest solved day from corresponding year event (in this example: 2022).

<br>

To show results of all solved days in a year use `all` parameter.
```bat
.\AdventOfCode.Year2022.exe all
```

<br>

There is also possible to run solutions only for particular days, by passing day numbers as a separate parametes.

For example, to run only solutions for day 1 and 3 from 2022 year event:
```bat
.\AdventOfCode.Year2022.exe 1 3
```
