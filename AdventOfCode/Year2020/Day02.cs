using AoCHelper;

namespace AdventOfCode.Year2020
{
    public class Day02 : BaseDay
    {
        private record PasswordWithPolicy(
            int FirstPolicyValue,
            int SecondPolicyValue,
            char RequiredChar,
            string Password);

        private readonly PasswordWithPolicy[] _passwordsWithPolicies;

        public Day02()
            => _passwordsWithPolicies = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int validPasswordsCount = _passwordsWithPolicies
                .Count(pwp =>
                {
                    int requiredCount = pwp
                        .Password
                        .Count(c => c == pwp.RequiredChar);

                    return (requiredCount >= pwp.FirstPolicyValue)
                        && (requiredCount <= pwp.SecondPolicyValue);
                });

            return new(validPasswordsCount.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int validPasswordsCount = _passwordsWithPolicies
                .Count(pwp =>
                    (pwp.Password[pwp.FirstPolicyValue - 1] == pwp.RequiredChar)
                  ^ (pwp.Password[pwp.SecondPolicyValue - 1] == pwp.RequiredChar));

            return new(validPasswordsCount.ToString());
        }

        private PasswordWithPolicy[] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] splited = line.Split(
                        new char[] { ' ', '-', ':' },
                        StringSplitOptions.RemoveEmptyEntries);

                    return new PasswordWithPolicy(
                        int.Parse(splited[0]),
                        int.Parse(splited[1]),
                        splited[2].First(),
                        splited[3]);
                })
                .ToArray();
    }
}
