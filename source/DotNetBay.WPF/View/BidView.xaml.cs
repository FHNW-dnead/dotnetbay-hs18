using System;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        public BidView(Auction selectedAuction)
        {
            this.InitializeComponent();

            var app = Application.Current as App;

            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);

            this.DataContext = new BidViewModel(selectedAuction, memberService, auctionService);

        }
    }
}
