using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Auction> Auctions {
            get { return this.auctions; }

            private set
            {
                this.auctions = value;
                this.OnPropertyChanged();
            }
        }

        private readonly AuctionService auctionService;

        ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();

        public MainWindow()
        {
            var app = Application.Current as App;

            InitializeComponent();

            this.DataContext = this;

            // get list of auctions
            if (app != null)
            {
                this.auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
                this.auctions = new ObservableCollection<Auction>(this.auctionService.GetAll());
            }


        }

        private void newAuctionBtn_Click(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog(); // Blocking

            var allAuctionsFromService = this.auctionService.GetAll();

            /* Option A: Full Update via INotifyPropertyChanged, not performant */
            /* ================================================================ */
            this.Auctions = new ObservableCollection<Auction>(allAuctionsFromService);

            /////* Option B: Let WPF only update the List and detect the additions */
            /////* =============================================================== */
            ////var toAdd = allAuctionsFromService.Where(a => !this.auctions.Contains(a));
            ////foreach (var auction in toAdd)
            ////{
            ////    this.auctions.Add(auction);
            ////}
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
