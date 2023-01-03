using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day15 : BaseDay
    {
        private record Ingredient(
            int Capacity,
            int Durability,
            int Flavor,
            int Texture,
            int Calories);

        private const int MaxTeaspoonsAmount = 100;
        private readonly IReadOnlyList<Ingredient> _ingredients;

        public Day15() => _ingredients = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int score = CalculateBestCookieScore();
            return new(score.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int score = CalculateBestCookieScore(500);
            return new(score.ToString());
        }

        private int CalculateBestCookieScore()
        {
            int score = 0;

            // Initializing recipe with one teaspoon of each ingredient.
            int[] quantites = _ingredients.Select(ing => 1).ToArray();

            for (int totalQuantity = _ingredients.Count; totalQuantity < MaxTeaspoonsAmount; ++totalQuantity)
            {
                var (index, totalScore) = FindBestScoredIngredient(quantites);
                quantites[index] += 1;
                score = totalScore;
            }

            return score;
        }

        private int CalculateBestCookieScore(int calories)
        {
            int[] quantites = new int[_ingredients.Count];
            int maxScore = 0;

            while (quantites.Last() <= MaxTeaspoonsAmount)
            {
                IncrementQuantities(quantites);

                if (quantites.Sum() == MaxTeaspoonsAmount)
                {
                    int currentCalories = CalculateCookieCalories(quantites);
                    if (currentCalories == calories)
                    {
                        int score = CalculateCookieScore(quantites);
                        maxScore = Math.Max(maxScore, score);
                    }
                }

            }

            return maxScore;
        }

        private int CalculateCookieCalories(int[] quantites)
        {
            int currentCalories = 0;
            for (int i = 0; i < _ingredients.Count; ++i)
            {
                currentCalories += _ingredients[i].Calories * quantites[i];
            }

            return currentCalories;
        }

        private static void IncrementQuantities(int[] quantites)
        {
            quantites[0]++;
            for (int i = 0; i < quantites.Length - 1; ++i)
            {
                if (quantites[i] <= MaxTeaspoonsAmount)
                {
                    break;
                }

                quantites[i] = 0;
                quantites[i + 1] += 1;
            }
        }

        private (int BestIngredientIndex, int RecipeScore) FindBestScoredIngredient(int[] quantites)
        {
            int maxScore = 0;
            int bestIngredientIndex = 0;
            int[] quantitesToTest = new int[_ingredients.Count];

            for (int i = 0; i < _ingredients.Count; ++i)
            {
                quantites.CopyTo(quantitesToTest, 0); 
                quantitesToTest[i] += 1;

                int score = CalculateCookieScore(quantitesToTest);
                if (score > maxScore)
                {
                    maxScore = score;
                    bestIngredientIndex = i;
                }
            }

            return (bestIngredientIndex, maxScore);
        }

        private int CalculateCookieScore(int[] quantites)
        {
            int capacity = 0;
            int durability = 0;
            int flavor = 0;
            int texture = 0;

            for (int i = 0; i < quantites.Length; ++i)
            {
                capacity += _ingredients[i].Capacity * quantites[i];
                durability += _ingredients[i].Durability * quantites[i];
                flavor += _ingredients[i].Flavor * quantites[i];
                texture += _ingredients[i].Texture * quantites[i];
            }

            return Math.Max(capacity, 0) *
                Math.Max(durability, 0) *
                Math.Max(flavor, 0) *
                Math.Max(texture, 0);
        }

        private List<Ingredient> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] splited = line.Split(
                        new[] { ' ', ',' },
                        StringSplitOptions.RemoveEmptyEntries);

                    return new Ingredient(
                        int.Parse(splited[2]),
                        int.Parse(splited[4]),
                        int.Parse(splited[6]),
                        int.Parse(splited[8]),
                        int.Parse(splited[10]));
                })
            .ToList();
    }
}
