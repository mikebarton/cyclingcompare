using System;
using System.Collections.Generic;
using System.Text;

namespace bikecompare.imaging.messages
{
    public class ImageHashCalculated
    {
        public string ProductId { get; set; }
        public ulong Hash { get; set; }

    }
}
