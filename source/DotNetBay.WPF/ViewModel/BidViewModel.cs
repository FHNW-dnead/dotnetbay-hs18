using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF.ViewModel
{
    public class BidViewModel : ViewModelBase
    {
        private readonly IMemberService memberService;

        private readonly IAuctionService auctionService;

        private readonly Auction selectedAuction;

        public BidViewModel(Auction selectedAuction, IMemberService memberService, IAuctionService auctionService)
        {
            this.memberService = memberService;
            this.auctionService = auctionService;
            this.selectedAuction = selectedAuction;

            this.CloseDialogCommand = new RelayCommand<Window>(this.CloseAction);
            this.AddBidAndCloseCommand = new RelayCommand<Window>(this.AddBidAndCloseAction);

            // Default Values
            this.YourBid = Math.Max(this.selectedAuction.CurrentPrice, this.selectedAuction.StartPrice);
        }

        public ICommand CloseDialogCommand { get; private set; }

        public ICommand AddBidAndCloseCommand { get; private set; }

        public double YourBid { get; set; }

        public string AuctionTitle
        {
            get { return this.selectedAuction.Title; }
        }

        public string AuctionDescription
        {
            get { return this.selectedAuction.Description; }
        }

        public byte[] AuctionImage
        {
            get { return this.selectedAuction.Image; }
        }

        public double AuctionStartPrice
        {
            get { return this.selectedAuction.StartPrice; }
        }

        public double AuctionCurrentPrice
        {
            get { return this.selectedAuction.CurrentPrice; }
        }

        public DateTime AuctionCloseDateTimeLocal
        {
            get { return this.selectedAuction.CloseDateTimeUtc.ToLocalTime(); }
        }

        private void CloseAction(Window window)
        {
            window.Close();
        }

        private void AddBidAndCloseAction(Window window)
        {
            this.auctionService.PlaceBid(this.selectedAuction, this.YourBid);

            window.Close();
        }
    }
}
