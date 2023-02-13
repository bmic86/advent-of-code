using AoCHelper;
using System.Globalization;

namespace AdventOfCode.Year2018
{
    public class Day04 : BaseDay
    {
        private record LogEntry(DateTime Timestamp, string Message);

        private class Sleep
        {
            public int TotalMinutes { get; set; }
            public int[] MinuteLog { get; private set; } = new int[60];
        }

        private readonly IReadOnlyList<LogEntry> _guardsLogs;

        private readonly Dictionary<int, Sleep> _guardsSleep = new();

        public Day04() => _guardsLogs = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int currentGuardId = int.MinValue;
            int sleepStartMinute = int.MinValue;

            int maxTotalMinutes = int.MinValue;
            int maxGuardId = int.MinValue;

            foreach (var logEntry in _guardsLogs)
            {
                if (logEntry.Message.StartsWith("Guard"))
                {
                    currentGuardId = BeginGuardShift(logEntry);
                }
                else if (logEntry.Message.StartsWith("falls asleep"))
                {
                    sleepStartMinute = logEntry.Timestamp.Minute;
                }
                else if (logEntry.Message.StartsWith("wakes up"))
                {
                    var sleep = _guardsSleep[currentGuardId];
                    sleep.TotalMinutes += logEntry.Timestamp.Minute - sleepStartMinute;

                    if (sleep.TotalMinutes > maxTotalMinutes)
                    {
                        maxTotalMinutes = sleep.TotalMinutes;
                        maxGuardId = currentGuardId;
                    }

                    FillMinuteLog(
                        sleepStartMinute,
                        logEntry.Timestamp.Minute,
                        sleep.MinuteLog);
                }
            }

            int minute = GetMaxValueIndex(_guardsSleep[maxGuardId].MinuteLog);
            int result = maxGuardId * minute;
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int maxValue = int.MinValue;
            int minute = int.MinValue;
            int guardId = int.MinValue;

            foreach (var guardSleep in _guardsSleep)
            {
                int index = GetMaxValueIndex(guardSleep.Value.MinuteLog);

                if (guardSleep.Value.MinuteLog[index] > maxValue)
                {
                    maxValue = guardSleep.Value.MinuteLog[index];
                    minute = index;
                    guardId = guardSleep.Key;
                }
            }

            int result = guardId * minute;
            return new(result.ToString());
        }

        private int BeginGuardShift(LogEntry logEntry)
        {
            int guardId = ParseGuardId(logEntry);
            if (!_guardsSleep.ContainsKey(guardId))
            {
                _guardsSleep[guardId] = new Sleep();
            }

            return guardId;
        }

        private static int ParseGuardId(LogEntry logEntry)
        {
            int endIndex = logEntry.Message.IndexOf(' ', 7);
            return int.Parse(logEntry.Message[7..endIndex]);
        }

        private static void FillMinuteLog(int start, int end, int[] minuteLog)
        {
            for (int i = start; i < end; ++i)
            {
                minuteLog[i]++;
            }
        }

        private static int GetMaxValueIndex(int[] array)
        {
            int maxValue = int.MinValue;
            int index = int.MinValue;

            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                    index = i;
                }
            }

            return index;
        }

        private List<LogEntry> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    var timestamp = DateTime.ParseExact(
                        line[..18],
                        "[yyyy-MM-dd HH:mm]",
                        CultureInfo.InvariantCulture);

                    return new LogEntry(timestamp, line[19..]);
                })
                .OrderBy(entry => entry.Timestamp)
                .ToList();
    }
}
