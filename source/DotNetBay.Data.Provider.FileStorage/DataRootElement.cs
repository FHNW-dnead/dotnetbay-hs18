using System.Collections.Generic;
using DotNetBay.Data.Entity;

namespace DotNetBay.Data.Provider.FileStorage
{
    internal class DataRootElement
    {
        public DataRootElement()
        {
            this.Auctions = new List<Auction>();
            this.Bids = new List<Bid>();
            this.Members = new List<Member>();
        }

        public List<Auction> Auctions { get; set; }

        public List<Bid> Bids { get; set; }

        public List<Member> Members { get; set; }
    }
}
