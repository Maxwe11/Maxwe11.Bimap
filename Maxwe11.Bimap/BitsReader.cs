namespace Maxwe11.Bimap
{
    using System;
    using System.Collections;

    public sealed class BitsReader
    {
        #region Fields

        private BitArray mBitArray;

        private int mBitsRead;

        #endregion

        #region Constructors

        public BitsReader(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            mBitArray = new BitArray(bytes);
        }

        #endregion

        #region Properties

        public int BitsRead { get { return mBitsRead; } }

        #endregion

        #region Public methods

        public byte ReadUInt8(int bitsCount = BitsCount.BitsPerByte)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerByte);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            var result = (byte)GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        [CLSCompliant(false)]
        public ushort ReadUInt16(int bitsCount = BitsCount.BitsPerWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            var result = (ushort)GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        [CLSCompliant(false)]
        public uint ReadUInt32(int bitsCount = BitsCount.BitsPerDWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerDWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            var result = (uint)GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

        [CLSCompliant(false)]
        public ulong ReadUInt64(int bitsCount = BitsCount.BitsPerQWord)
        {
            BitsCount.EnsureBitsCount(bitsCount, BitsCount.BitsPerQWord);
            EnsureRequestedBitsCount(mBitsRead, bitsCount, mBitArray.Count);

            ulong result = GetUnsigned(bitsCount);

            mBitsRead += bitsCount;

            return result;
        }

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

        public float ReadFloat()
        {
            var byte1 = ReadUInt8();
            var byte2 = ReadUInt8();
            var byte3 = ReadUInt8();
            var byte4 = ReadUInt8();

            return BitConverter.ToSingle(new[] { byte1, byte2, byte3, byte4 }, 0);
        }

        public double ReadDouble()
        {
            var integer = ReadInt64();
            return BitConverter.Int64BitsToDouble(integer);
        }

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
                throw new InvalidOperationException("Can't read " + requestedBits + " bits. Only " + (allBitsCount - bitsRead) + " bits available");
        }

        #endregion
    }
}
