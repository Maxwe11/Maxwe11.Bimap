namespace Maxwe11.Bimap
{
    using System;
    using System.Globalization;

    static class BitsCount
    {
        public const int BitsPerByte = 8;

        public const int BitsPerWord = 16;

        public const int BitsPerDWord = 32;

        public const int BitsPerQWord = 64;

        public static void EnsureBitsCount(int bitsCount, int maxBitsCount)
        {
            if (bitsCount <= 0 || bitsCount > maxBitsCount)
            {
            	var first = maxBitsCount.ToString(CultureInfo.InvariantCulture);
                throw new ArgumentException("bits count should be in inclusive range [1, " + first + "]");
            }
        }
    }
}
