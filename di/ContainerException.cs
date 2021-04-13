using System;

namespace ComputerGraphics.di
{
    public class ContainerException : Exception
    {
        public ContainerException(string message) : base(message)
        {
        }
    }
}