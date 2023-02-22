using AdventOfCode.Common;
using AoCHelper;
using System.Collections.Specialized;

namespace AdventOfCode.Year2021
{
    public class Day03 : BaseDay
    {
        private readonly int _width;
        private readonly int _height;
        private readonly bool[,] _binaryData;

        public Day03()
        {
            (_binaryData, _width, _height)
                = DataLoader.LoadBoolTable2D(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            BitVector32 gammaRate = new();
            BitVector32 epsilonRate = new();

            int bitIndex = 1 << (_width - 1);

            for (int x = 0; x < _width; ++x)
            {
                bool gammaRateBit = GetMostCommonBit(x);

                gammaRate[bitIndex] = gammaRateBit;
                epsilonRate[bitIndex] = !gammaRateBit;

                bitIndex >>= 1;
            }

            int result = gammaRate.Data * epsilonRate.Data;
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            (int oxygenRatingIndex, int co2RatingIndex)
                = FindRatingsIndices();

            int bitIndex = 1 << (_width - 1);

            BitVector32 oxygenRating = new();
            BitVector32 co2Rating = new();
            for (int x = 0; x < _width; ++x)
            {
                oxygenRating[bitIndex] = _binaryData[oxygenRatingIndex, x];
                co2Rating[bitIndex] = _binaryData[co2RatingIndex, x];

                bitIndex >>= 1;
            }

            int result = oxygenRating.Data * co2Rating.Data;
            return new(result.ToString());
        }

        private (int oxygenIndex, int co2Index) FindRatingsIndices()
        {
            int oxygenIndex = -1;
            int co2Index = -1;

            var oxygenIndices = Enumerable
                .Range(0, _height)
                .ToHashSet();

            var co2Indices = Enumerable
                .Range(0, _height)
                .ToHashSet();

            for (int x = 0; x < _width; ++x)
            {
                bool oxygenBit = GetMostCommonBit(x, oxygenIndices);
                bool co2Bit = GetLeastCommonBit(x, co2Indices);

                for (int y = 0; y < _height; ++y)
                {
                    QueryForRatingIndex(
                        oxygenIndices,
                        oxygenBit,
                        x,
                        y,
                        ref oxygenIndex);

                    QueryForRatingIndex(
                        co2Indices,
                        co2Bit,
                        x,
                        y,
                        ref co2Index);
                }
            }

            return (oxygenIndex, co2Index);
        }

        private void QueryForRatingIndex(
            HashSet<int> indices,
            bool bitToCompare,
            int x,
            int y,
            ref int resultIndex)
        {
            if (indices.Contains(y) && _binaryData[y, x] != bitToCompare)
            {
                indices.Remove(y);
                if (indices.Count == 1)
                {
                    resultIndex = indices.First();
                    indices.Clear();
                }
            }
        }

        private bool GetLeastCommonBit(int x, IReadOnlySet<int> valueIndexes)
            => !GetMostCommonBit(x, valueIndexes);

        private bool GetMostCommonBit(int x, IReadOnlySet<int> valueIndexes)
        {
            int bitsSetCount = 0;

            foreach (int y in valueIndexes)
            {
                if (_binaryData[y, x])
                {
                    bitsSetCount++;
                }
            }

            int bitsUnsetCount = valueIndexes.Count - bitsSetCount;
            return bitsSetCount >= bitsUnsetCount;
        }

        private bool GetMostCommonBit(int x)
        {
            int bitsSetCount = CountBitsThatAreSet(x);
            int bitsUnsetCount = _height - bitsSetCount;

            return bitsSetCount >= bitsUnsetCount;
        }

        private int CountBitsThatAreSet(int x)
        {
            int count = 0;

            for (int y = 0; y < _height; ++y)
            {
                if (_binaryData[y, x])
                {
                    count++;
                }
            }

            return count;
        }
    }
}
