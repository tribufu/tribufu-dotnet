// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: UNLICENSED

using IdGen;
using System;

namespace Tribufu.Database
{
    public static class FlakeGenerator
    {
        public static readonly DateTime EPOCH = new(2016, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly IdStructure _structure = new(41, 10, 12);

        private static readonly IdGeneratorOptions _options = new(_structure, new DefaultTimeSource(EPOCH));

        private static readonly IdGenerator _generator = new(0, _options);

        public static ulong New()
        {
            //Console.WriteLine("Max. generators       : {0}", _structure.MaxGenerators);
            //Console.WriteLine("Id's/ms per generator : {0}", _structure.MaxSequenceIds);
            //Console.WriteLine("Id's/ms total         : {0}", _structure.MaxGenerators * _structure.MaxSequenceIds);
            //Console.WriteLine("Wraparound interval   : {0}", _structure.WraparoundInterval(_generator.Options.TimeSource));
            //Console.WriteLine("Wraparound date       : {0}", _structure.WraparoundDate(_generator.Options.TimeSource.Epoch, _generator.Options.TimeSource).ToString("O"));

            return (ulong)_generator.CreateId();
        }

        public static ulong FromTimestamp(DateTime timestamp, ushort sequence = 0)
        {
            var diff = (long)(timestamp - EPOCH).TotalMilliseconds;
            if (diff < 0 || diff > (1L << 41) - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp), "Timestamp out of range for flake.");
            }

            ulong timestampPart = (ulong)diff << 22;        // 41 bits << (10+12)
            ulong generatorPart = 0ul << 12;                // Always 0
            ulong sequencePart = (ulong)(sequence & 0xFFF); // 12 bits

            return timestampPart | generatorPart | sequencePart;
        }
    }
}
