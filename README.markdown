Maxwe11.Bimap
=========
[![Build status](https://ci.appveyor.com/api/projects/status/noke8oadgvuynshn/branch/master?svg=true)](https://ci.appveyor.com/project/Maxwe11/maxwe11-bimap/branch/master)

A library for mapping binary data onto .NET primitive types. Mapping to objects of custom classes also supported.

Install
=======

To install Maxwe11.Bimap, run the following command in the Package Manager Console

    PM> Install-Package Maxwe11.Bimap

Example of usage
=======
For example you have the frame with the following structure:

    |   A   |    B   |    C   |    D   |   E   |
    |:-----:|:------:|:------:|:------:|:-----:|
    | 4 bit | 12 bit | 24 bit | 13 bit | 3 bit |
    |:-----:|:------:|:------:|:------:|:-----:|
    |  byte |  short |   int  | ushort | sbyte |

You can easily retrieve each part of frame separately

    byte[] bytes = //...receive bytes
    var reader = new BitsReader(bytes);
    byte A = reader.ReadUInt8(4);
    short B = reader.ReadInt16(12);
    int C = reader.ReadInt32(24);
    ushort D = reader.ReadUInt16(13);
    sbyte E = reader.ReadInt8(3);
	
or create a frame class

    class Foo
    {
        [Bimap(OrderId: 1, BitsCount: 4)]
        public byte A { get; set; }

        [Bimap(OrderId: 2, BitsCount: 12)]
        public short B { get; set; }

        [Bimap(OrderId: 3, BitsCount: 24)]
        public  int C { get; set; }

        [Bimap(OrderId: 4, BitsCount: 13)]
        public ushort D { get; set; }

        [Bimap(OrderId: 5, BitsCount: 3)]
        public sbyte E { get; set; }
    }

and map it in following way
	
    byte[] bytes = //...receive bytes
    var reader = new ObjectReader<Foo>(bytes);
    var frame = new Foo();
    reader.Read(frame);
	
For now supported properties of following types:
> byte, ushort, uint, ulong, sbyte, short, int, long, float, double

Documentation
=======

XML documentation available in code

License
=======

MIT License (MIT)

http://opensource.org/licenses/MIT
