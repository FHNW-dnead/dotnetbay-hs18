using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;
using DotNetBay.Interfaces;

namespace DotNetBay.Data.EF
{
    public class EFMainRepository : IMainRepository
    {
        private MainDbContext dbContext;

        public EFMainRepository()
        {
            this.dbContext = new MainDbContext();
        }

        public Database Database
        {
            get
            {
                return this.dbContext.Database;
            }
        }
        public IQueryable<Auction> GetAuctions()
        {
            return this.dbContext.Auctions.Include(a => a.Bids).Include(a => a.Seller).Include(a => a.ActiveBid).Include(a => a.Winner);
        }

        public IQueryable<Member> GetMembers()
        {
            return this.dbContext.Members.Include(m => m.Auctions).Include(m => m.Bids);
        }

        public Bid GetBidByTransactionId(Guid transactionId)
        {
            return this.dbContext.Bids.Include(b => b.Auction).Include(b => b.Bidder).FirstOrDefault(b => b.TransactionId == transactionId);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public Member Add(Member member)
        {
            this.dbContext.Members.Add(member);

            return member;
        }

        public Bid Add(Bid bid)
        {
            this.dbContext.Bids.Add(bid);

            return bid;
        }

        public Auction Update(Auction auction)
        {
            return auction;
        }

        public Auction Add(Auction auction)
        {
            this.dbContext.Auctions.Add(auction);

            return auction;
        }
    }
}
