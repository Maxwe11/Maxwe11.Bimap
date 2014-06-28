namespace Maxwe11.Bimap.Numbers
{
    interface INumber
    {
        int BitsCount { get; }

        object Value { get; }

        void Apply(INumbersVisitor visitor);
    }
}
