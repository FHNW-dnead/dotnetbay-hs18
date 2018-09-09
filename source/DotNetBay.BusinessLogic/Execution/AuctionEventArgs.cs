using System;
using DotNetBay.Data.Entity;

namespace DotNetBay.Core.Execution
{
    public class AuctionEventArgs : EventArgs
    {
        public Auction Auction { get; set; }

        public bool IsSuccessful { get; set; }
    }
}