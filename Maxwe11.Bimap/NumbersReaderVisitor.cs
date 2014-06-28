namespace Maxwe11.Bimap
{
    using Maxwe11.Bimap.Numbers;

    class NumbersReaderVisitor : INumbersVisitor
    {
        private readonly BitsReader mBitsReader;

        public NumbersReaderVisitor(BitsReader reader)
        {
            if (reader == null)
                throw new System.ArgumentNullException("reader");

            mBitsReader = reader;
        }

        public void Visit(Integer8 value)
        {
            value.TypedValue = mBitsReader.ReadInt8(value.BitsCount);
        }

        public void Visit(Integer16 value)
        {
            value.TypedValue = mBitsReader.ReadInt16(value.BitsCount);
        }

        public void Visit(Integer32 value)
        {
            value.TypedValue = mBitsReader.ReadInt32(value.BitsCount);
        }

        public void Visit(Integer64 value)
        {
            value.TypedValue = mBitsReader.ReadInt64(value.BitsCount);
        }

        public void Visit(UnsignedInteger8 value)
        {
            value.TypedValue = mBitsReader.ReadUInt8(value.BitsCount);
        }

        public void Visit(UnsignedInteger16 value)
        {
            value.TypedValue = mBitsReader.ReadUInt16(value.BitsCount);
        }

        public void Visit(UnsignedInteger32 value)
        {
            value.TypedValue = mBitsReader.ReadUInt32(value.BitsCount);
        }

        public void Visit(UnsignedInteger64 value)
        {
            value.TypedValue = mBitsReader.ReadUInt64(value.BitsCount);
        }

        public void Visit(Float value)
        {
            value.TypedValue = mBitsReader.ReadFloat();
        }

        public void Visit(Double value)
        {
            value.TypedValue = mBitsReader.ReadDouble();
        }
    }
}
