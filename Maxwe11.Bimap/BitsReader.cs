namespace Maxwe11.Bimap
{
    using System;
    using System.Collections;

    /// <summary>
    /// Reads primitive data types as binary values with a specific amount of bits
    /// </summary>
    /// <remarks>
    /// For now <see cref="BitsReader"/> supports following types: 
    /// * <see cref="Byte"/>,
    /// * <see cref="UInt16"/>,
    /// * <see cref="UInt32"/>, 
    /// * <see cref="UInt64"/>,
    /// * <see cref="SByte"/>,
    /// * <see cref="Int16"/>,
    /// * <see cref="Int32"/>, 
    /// * <see cref="Int64"/>,
    /// * <see cref="Single"/>, 
    /// * <see cref="Double"/>,
    /// </remarks>

    public sealed class BitsReader
    {
        #region Fields

        private BitArray mBitArray;

        private int mBitsRead;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BitsReader"/> class
        /// </summary>
        /// <param name="bytes">The array of bytes from which to read</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BitsReader(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            mBitArray = new BitArray(bytes);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current bit-position within the byte array
        /// </summary>
        public int BitsRead { get { return mBitsRead; } }

        #endregion

        #region Public methods

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="Byte"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 8-bit unsigned integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 8]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        public byte ReadUInt8(int bitsCount = BitsCount.BitsPerByte)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerByte);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            var result = (byte)GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="UInt16"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 16-bit unsigned integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 16]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        [CLSCompliant(false)]
        public ushort ReadUInt16(int bitsCount = BitsCount.BitsPerWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            var result = (ushort)GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="UInt32"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 32-bit unsigned integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 32]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        [CLSCompliant(false)]
        public uint ReadUInt32(int bitsCount = BitsCount.BitsPerDWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerDWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            var result = (uint)GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="UInt64"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 64-bit unsigned integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 64]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        [CLSCompliant(false)]
        public ulong ReadUInt64(int bitsCount = BitsCount.BitsPerQWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerQWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            ulong result = GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="SByte"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 8-bit signed integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 8]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        [CLSCompliant(false)]
        public sbyte ReadInt8(int bitsCount = BitsCount.BitsPerByte)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerByte);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            sbyte result = (sbyte)GetSigned(bitsCount);

            if (bitsCount == BitsCount.BitsPerByte)
            {
                mBitsRead += bitsCount;
                return result;
            }

            var mask = (sbyte)((1 << bitsCount) - 1);
            var signMask = (sbyte)(1 << (bitsCount - 1));

            if ((result & signMask) != 0)
                result |= (sbyte)~mask;

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="Int16"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 16-bit signed integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 16]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        public short ReadInt16(int bitsCount = BitsCount.BitsPerWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            short result = (short)GetSigned(bitsCount);

            if (bitsCount == BitsCount.BitsPerWord)
            {
                mBitsRead += bitsCount;
                return result;
            }

            var mask = (short)((1 << bitsCount) - 1);
            var signMask = (short)(1 << (bitsCount - 1));

            if ((result & signMask) != 0)
                result |= (short)~mask;

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="Int32"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 32-bit signed integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 32]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        public int ReadInt32(int bitsCount = BitsCount.BitsPerDWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerDWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            int result = (int)GetSigned(bitsCount);

            if (bitsCount == BitsCount.BitsPerDWord)
            {
                mBitsRead += bitsCount;
                return result;
            }

            int mask = (1 << bitsCount) - 1;
            int signMask = 1 << (bitsCount - 1);

            if ((result & signMask) != 0)
                result |= ~mask;

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next <paramref name="bitsCount"/> bits from byte array as <see cref="Int64"/>
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns>The 64-bit signed integer value of the specified amount of bits</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="bitsCount"/> is not in inclusive range [1, 64]</exception>
        /// <exception cref="InvalidOperationException">Thrown if requested <paramref name="bitsCount"/> amount of bits couldn't be read</exception>
        public long ReadInt64(int bitsCount = BitsCount.BitsPerQWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerQWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            long result = GetSigned(bitsCount);

            if (bitsCount == BitsCount.BitsPerQWord)
            {
                mBitsRead += bitsCount;
                return result;
            }

            long mask;

            if (bitsCount < 63)
                mask = (1L << bitsCount) - 1;
            else if (bitsCount == 63)
                mask = long.MaxValue;
            else
                mask = -1;

            long signMask = 1L << (bitsCount - 1);

            if ((result & signMask) != 0)
                result |= ~mask;

            mBitsRead += bitsCount;

            return result;
        }

        /// <summary>
        /// Reads next 32 bits from byte array as <see cref="Single"/>
        /// </summary>
        /// <returns>The single-precision floating point number</returns>
        public float ReadFloat()
        {
            var byte1 = ReadUInt8();
            var byte2 = ReadUInt8();
            var byte3 = ReadUInt8();
            var byte4 = ReadUInt8();

            return BitConverter.ToSingle(new[] { byte1, byte2, byte3, byte4 }, 0);
        }

        /// <summary>
        /// Reads next 64 bits from byte array as <see cref="Double"/>
        /// </summary>
        /// <returns>The double-precision floating point number</returns>
        public double ReadDouble()
        {
            var integer = ReadInt64();
            return BitConverter.Int64BitsToDouble(integer);
        }

        /// <summary>
        /// Sets new byte array for reading objects
        /// </summary>
        /// <param name="bytes">The array of unsigned bytes from which to read</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Reset(byte[] bytes)
        {
            if (bytes == null)
                throw  new ArgumentNullException("bytes");

            var bitArray = new BitArray(bytes);
            mBitArray = bitArray;
            mBitsRead = 0;
        }

        #endregion

        #region Private methods

        private long GetSigned(int bitsCount)
        {
            long result = 0;

            for (int i = 0; i < bitsCount; ++i)
            {
                if (mBitArray.Get(mBitsRead + i))
                    result |= 1L << i;
            }

            return result;
        }

        private ulong GetUnsigned(int bitsCount)
        {
            ulong result = 0;

            for (int i = 0; i < bitsCount; ++i)
            {
                if (mBitArray.Get(mBitsRead + i))
                    result |= 1UL << i;
            }

            return result;
        }

        private static void EnsureRequestedBitsCount(int bitsRead, int requestedBits, int allBitsCount)
        {
            if (bitsRead + requestedBits > allBitsCount)
            {
            	var first = requestedBits.ToString();
            	var second = (allBitsCount - bitsRead).ToString();
                throw new InvalidOperationException("Can't read " + first + " bits. Only " + second + " bits available");
            }
        }

        #endregion
    }
}
