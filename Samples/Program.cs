namespace Samples
{
    using System;

    using Maxwe11.Bimap;
    using Maxwe11.Bimap.Attributes;

    static class Program
    {
        static void Main()
        {
            BitsReaderSample();
            ObjectReaderSample();
        }

        private static void BitsReaderSample()
        {
            byte[] buffer1 = { 25, 26, 27, 28, 29, 30, 31, 32 };

            var reader = new BitsReader(buffer1);

            sbyte b = reader.ReadInt8();
            sbyte b1 = reader.ReadInt8(1);
            sbyte b3 = reader.ReadInt8(3);
            sbyte b4 = reader.ReadInt8(4);

            short s = reader.ReadInt16();
            short s1 = reader.ReadInt16(1);
            short s2 = reader.ReadInt16(2);
            short s4 = reader.ReadInt16(4);
            short s5 = reader.ReadInt16(5);
            short s8 = reader.ReadInt16(8);
            short s12 = reader.ReadInt16(12);

            //receive new data in buffer1
            reader.Reset(buffer1);

            int i1 = reader.ReadInt32(1);
            int i7 = reader.ReadInt32(7);
            int i24 = reader.ReadInt32(24);
            int i = reader.ReadInt32();

            //receive new data in buffer1
            reader.Reset(buffer1);

            long l1 = reader.ReadInt64(1);
            long l3 = reader.ReadInt64(3);
            long l24 = reader.ReadInt64(24);
            long l36 = reader.ReadInt64(36);

            //receive new data in buffer1
            reader.Reset(buffer1);

            long l = reader.ReadInt64();
        }

        class FooFrame
        {
            [Bimap(OrderId: 1, BitsCount: 24)]
            public int Foo { get; set; }

            [Bimap(OrderId: 2)]
            public ushort Spam { get; set; }

            [Bimap(OrderId: 3, BitsCount: 6)]
            public byte Bar { get; set; }

            [Bimap(OrderId: 4, BitsCount: 2)]
            public sbyte Eggs { get; set; }
        }

        private static void ObjectReaderSample()
        {
            var bytes = new byte[] { 255, 0, 255, 5, 13, 170 };
            var frame = new FooFrame();
            var reader = ObjectReader.Create<FooFrame>(bytes);
            reader.Read(frame);

            Console.WriteLine(frame.Foo);  //-65281
            Console.WriteLine(frame.Spam); //3333
            Console.WriteLine(frame.Bar);  //42
            Console.WriteLine(frame.Eggs); //-2
        }
    }
}
