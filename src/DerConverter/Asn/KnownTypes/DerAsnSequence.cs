﻿using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnSequence : DerAsnType<DerAsnType[]>
    {
        internal DerAsnSequence(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnSequence(DerAsnIdentifier identifier, DerAsnType[] value)
            : base(identifier, value)
        {
        }

        public DerAsnSequence(DerAsnType[] value)
            : base(DerAsnIdentifiers.Constructed.Sequence, value)
        {
        }

        protected override DerAsnType[] DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            var items = new List<DerAsnType>();
            while (rawData.Any()) items.Add(decoder.Decode(rawData));
            return items.ToArray();
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, DerAsnType[] value)
        {
            return value
                .Select(x => encoder.Encode(x))
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
