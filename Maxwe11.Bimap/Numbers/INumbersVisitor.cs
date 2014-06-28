namespace Maxwe11.Bimap.Numbers
{
    interface INumbersVisitor
    {
        void Visit(Integer8 value);

        void Visit(Integer16 value);

        void Visit(Integer32 value);

        void Visit(Integer64 value);

        void Visit(UnsignedInteger8 value);

        void Visit(UnsignedInteger16 value);

        void Visit(UnsignedInteger32 value);

        void Visit(UnsignedInteger64 value);

        void Visit(Float value);

        void Visit(Double value);
    }
}
