using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using Microsoft.Win32;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window, INotifyPropertyChanged
    {
        private readonly AuctionService auctionService;

        private readonly Auction newAuction;

        public Auction NewAuction
        {
            get
            {
                return newAuction;
            }
        }

        public string FilePathToImage { get; set; }

        public SellView()
        {
            this.InitializeComponent();

            this.DataContext = this;
            this.FilePathToImage = "<select image with extension jpg>";

            var app = Application.Current as App;

            if (app != null)
            {
                var simpleMemberService = new SimpleMemberService(app.MainRepository);
                this.auctionService = new AuctionService(app.MainRepository, simpleMemberService);
                this.newAuction = new Auction
                {
                    Seller = simpleMemberService.GetCurrentMember(),
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
                };
            }
        }

        private void AddAuctionClick(object sender, RoutedEventArgs e)
        {
            this.auctionService.Save(this.newAuction);

            this.Close();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectImageButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                var fileInfo = new FileInfo(openFileDialog.FileName);

                // allow only for jpg files
                if (fileInfo.Extension.EndsWith("jpg"))
                {
                    this.FilePathToImage = openFileDialog.FileName;
                    this.newAuction.Image = File.ReadAllBytes(this.FilePathToImage);
                    if (this.FilePathToImage != null)
                    {
                        this.OnPropertyChanged(nameof(this.FilePathToImage));
                    }
                }
            }
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
