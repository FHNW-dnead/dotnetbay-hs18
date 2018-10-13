using System;
using System.Globalization;
using System.Windows.Data;

namespace DotNetBay.WPF.Converter
{
    public class IsClosedToStatusTextConverter : IValueConverter
    {
        private const string AuctionStatusOpen = "Open";
        private const string AuctionStatusClosed = "Closed";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && ((bool)value))
            {
                return AuctionStatusClosed;
            }

            return AuctionStatusOpen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
