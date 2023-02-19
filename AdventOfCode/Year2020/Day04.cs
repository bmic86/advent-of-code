using AoCHelper;

namespace AdventOfCode.Year2020
{
    public class Day04 : BaseDay
    {
        private readonly char[] _fieldSeparators =
        {
            '\n', ' ',
        };

        private readonly string[] _requiredFields =
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid",
        };

        private readonly IReadOnlySet<string> _eyeColors = new HashSet<string>
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth",
        };

        private readonly IReadOnlyDictionary<string, string>[] _documents;

        public Day04()
            => _documents = LoadDocuments();

        public override ValueTask<string> Solve_1()
        {
            int count = _documents
                .Where(RequiredFieldsExists)
                .Count();

            return new(count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int count = _documents
                .Where(RequiredFieldsAreValid)
                .Count();

            return new(count.ToString());
        }

        private bool RequiredFieldsExists(IReadOnlyDictionary<string, string> document)
            => _requiredFields.All(document.ContainsKey);

        private bool RequiredFieldsAreValid(IReadOnlyDictionary<string, string> document)
        {
            foreach (var field in _requiredFields)
            {
                if (!document.TryGetValue(field, out var value))
                {
                    return false;
                }

                if (!IsFieldValueValid(field, value))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsFieldValueValid(string field, string value)
            => field switch
            {
                "byr" => ValidateNumber(value, 1920, 2002),
                "iyr" => ValidateNumber(value, 2010, 2020),
                "eyr" => ValidateNumber(value, 2020, 2030),
                "hgt" => ValidateHeight(value),
                "hcl" => ValidateHairColor(value),
                "ecl" => ValidateEyeColor(value),
                "pid" => ValidatePassportId(value),
                _ => true,
            };

        private bool ValidateEyeColor(string value)
            => _eyeColors.Contains(value);

        private static bool ValidatePassportId(string value)
            => value.Length == 9 && value.All(char.IsDigit);

        private static bool ValidateHairColor(string value)
            => value.Length == 7 &&
                value[0] == '#' &&
                value[1..].All(c => char.IsDigit(c) || (c >= 'a' && c <= 'f'));

        private static bool ValidateHeight(string value)
        {
            if (value.Length < 3)
            {
                return false;
            }

            int heightEnd = value.Length - 2;
            string height = value[..heightEnd];
            string units = value[heightEnd..];

            return units switch
            {
                "cm" => ValidateNumber(height, 150, 193),
                "in" => ValidateNumber(height, 59, 76),
                _ => false,
            };
        }

        private static bool ValidateNumber(string value, int min, int max)
            => int.TryParse(value, out int num) &&
                num >= min &&
                num <= max;

        private IReadOnlyDictionary<string, string>[] LoadDocuments()
            => File.ReadAllText(InputFilePath)
                .Split("\n\n")
                .Select(documentData => documentData
                    .Split(_fieldSeparators, StringSplitOptions.RemoveEmptyEntries)
                    .Select(fieldData => fieldData.Split(':'))
                    .ToDictionary(field => field[0], field => field[1]))
                .ToArray();
    }
}
