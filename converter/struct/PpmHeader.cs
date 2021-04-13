namespace ComputerGraphics.converter.@struct
{
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