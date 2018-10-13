using System;
using System.Windows.Input;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    public class AuctionViewModel : ViewModelBase
    {
        private const double Tolerance = 0.002;

        private Auction auction;

        private double currentPrice;

        private double bidCount;

        private string currentWinner;

        private DateTime? closedDateLocal;

        private string finalWinner;

        private string status;

        private bool isRunning;

        public AuctionViewModel(Auction auction)
        {
            this.auction = auction;

            this.AddBidCommand = new RelayCommand(this.AddBidAction);

            this.Apply();
        }

        public ICommand AddBidCommand { get; private set; }

        public string Title
        {
            get { return this.auction.Title; }
        }

        public byte[] Image
        {
            get { return this.auction.Image; }
        }

        public double StartPrice
        {
            get { return this.auction.StartPrice; }
        }

        public DateTime StartDateTimeLocal
        {
            get { return this.auction.StartDateTimeUtc.ToLocalTime(); }
        }

        public DateTime EndDateTimeLocal
        {
            get { return this.auction.EndDateTimeUtc.ToLocalTime(); }
        }

        public string Seller
        {
            get { return this.auction.Seller.DisplayName; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.Set(() => this.Status, ref this.status, value); }
        }

        public double CurrentPrice
        {
            get
            {
                return this.currentPrice;
            }

            set
            {
                if (Math.Abs(this.currentPrice - value) > Tolerance)
                {
                    this.currentPrice = value;
                    this.RaisePropertyChanged(() => this.CurrentPrice);
                }
            }
        }

        public double BidCount
        {
            get
            {
                return this.bidCount;
            }

            set
            {
                if (Math.Abs(this.bidCount - value) > Tolerance)
                {
                    this.bidCount = value;
                    this.RaisePropertyChanged(() => this.BidCount);
                }
            }
        }

        public string CurrentWinner
        {
            get { return this.currentWinner; }
            set { this.Set(() => this.CurrentWinner, ref this.currentWinner, value); }
        }

        public DateTime? ClosedDateLocal
        {
            get
            {
                return this.closedDateLocal != null ? this.closedDateLocal.Value.ToLocalTime() : (DateTime?)null;
            }

            set
            {
                this.closedDateLocal = value;
            }
        }

        public string FinalWinner
        {
            get { return this.finalWinner; }
            set { this.Set(() => this.FinalWinner, ref this.finalWinner, value); }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    this.RaisePropertyChanged(() => this.IsRunning);
                }
            }
        }

        public Auction Auction
        {
            get
            {
                return this.auction;
            }
        }

        public void Update(Auction modifiedAuction)
        {
            this.auction = modifiedAuction;
            this.Apply();
        }

        private void AddBidAction()
        {
            var view = new BidView(this.auction);
            view.Show();
        }

        private void Apply()
        {
            this.Status = this.auction.IsClosed ? "Closed" : "Valid";
            this.CurrentPrice = this.auction.CurrentPrice;
            this.BidCount = this.auction.Bids.Count;
            this.IsRunning = this.auction.IsRunning;

            if (this.auction.ActiveBid != null)
            {
                this.CurrentWinner = this.auction.ActiveBid.Bidder.DisplayName;
            }

            if (this.auction.CloseDateTimeUtc > DateTime.MinValue)
            {
                this.ClosedDateLocal = this.auction.CloseDateTimeUtc.ToLocalTime();
            }

            if (this.auction.Winner != null)
            {
                this.FinalWinner = this.auction.Winner.DisplayName;
            }
        }
    }
}
