using System;
using System.Windows;

using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        private readonly Auction selectedAuction;

        private readonly AuctionService auctionService;

        public double YourBid { get; set; }

        public Auction SelectedAuction
        {
            get
            {
                return this.selectedAuction;
            }
        }

        public BidView(Auction selectedAuction)
        {
            this.selectedAuction = selectedAuction;
            this.InitializeComponent();

            this.DataContext = this;

            var app = Application.Current as App;

            if (app != null)
            {
                SimpleMemberService simpleMemberService  = new SimpleMemberService(app.MainRepository);
                this.auctionService = new AuctionService(app.MainRepository, simpleMemberService);
            }

            this.YourBid = Math.Max(this.SelectedAuction.CurrentPrice, this.SelectedAuction.StartPrice);
        }

        private void PlaceBidAuction_Click(object sender, RoutedEventArgs e)
        {
            // store new bid
            this.auctionService.PlaceBid(this.SelectedAuction, this.YourBid);

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
