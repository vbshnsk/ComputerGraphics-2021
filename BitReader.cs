using System;
using System.Text;

namespace ComputerGraphics
{
    public class BitReader
    {
        protected byte[] Buffer;
        protected bool HasData => _offset != Buffer.Length;
        private int _offset;

        protected void SetOffset(int value)
        {
            _offset = value;
        }

        protected int Int()
        {
            var res = BitConverter.ToInt32(Buffer, _offset);
            _offset += 4;
            return res;
        }

        protected short Short()
        {
            var res = BitConverter.ToInt16(Buffer, _offset);
            _offset += 2;
            return res;
        }

        protected string String(int length)
        {
            var bytes = Bytes(length);
            return Encoding.UTF8.GetString(bytes);
        }

        protected byte[] Bytes(int length)
        {
            var bytes = new byte[length];
            Array.Copy(Buffer, _offset, bytes, 0, length);
            _offset += length;
            return bytes;
        }

        protected byte Byte()
        {
            return Buffer[_offset++];
        }

    }
}