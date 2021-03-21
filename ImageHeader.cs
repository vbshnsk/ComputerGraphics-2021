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
        public static BmpHeader GetBase(int width, int depth)
        {
            return new BmpHeader
            {
                Flag = "BM",
                Reserved = new byte[4],
                FileSize = (width * depth * 4) + 54,
                InfoSize = 40,
                HeaderSize = 54,
                BiXPelsPerMeter = 2834,
                BiYPelsPerMeter = 2834,
                Bits = 32,
                BiPlanes = 1,
                Width = width,
                Depth = depth
            };
        }
        
    }

    public struct PpmHeader
    {
        public string Flag;
        public int Width;
        public int Depth;
        public int Max;

        public static PpmHeader GetBase(int width, int depth)
        {
            return new PpmHeader
            {
                Flag = "P6",
                Width = width,
                Depth = depth,
                Max = 255
            };
        }
    }
}