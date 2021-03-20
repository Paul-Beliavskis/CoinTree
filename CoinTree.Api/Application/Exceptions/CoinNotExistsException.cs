using System;

namespace CoinTree.Application.Exceptions
{
    public class CoinNotExistsException : Exception
    {
        public CoinNotExistsException() : base("Coin does not exist")
        {
            
        }
    }
}
