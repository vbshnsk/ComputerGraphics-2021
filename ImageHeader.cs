using System;

namespace ComputerGraphics
{
    public struct BmpHeader
    {
        public string Flag;
        public int FileSize;
        public byte[] Reserved;
        public int HeaderSize;
        public int InfoSize;
        public int Width;
        public int Depth;
        public short BiPlanes;
        public short Bits;
        public int BiCompression;
        public int BiSizeImage;       
        public int BiXPelsPerMeter; 
        public int BiYPelsPerMeter;
        public int BiClrUsed;
        public int BiClrImportant;
    }
}