namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    class SuccessTest
    {
        [Bimap(1)]
        public byte FooByte { get; set; }

        [Bimap(2)]
        public ushort FooUshort { get; set; }

        [Bimap(3)]
        public uint FooUint { get; set; }

        [Bimap(4)]
        public ulong FooUlong { get; set; }

        [Bimap(5)]
        public byte FooSByte { get; set; }

        [Bimap(6)]
        public ushort FooShort { get; set; }

        [Bimap(7)]
        public uint FooInt { get; set; }

        [Bimap(8)]
        public ulong FooLong { get; set; }

        [Bimap(9)]
        public float FooFloat { get; set; }

        [Bimap(10)]
        public float FooDouble { get; set; }
    }
}
