using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace TaitoTourismMap
{
    public class ItemTappedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ItemTappedEventArgs;
            var pagename = parameter as Page;
            if (eventArgs == null)
                throw new ArgumentException("Expected TappedEventArgs as value", nameof(value));

            var args = new List<ItemTappedEventArgsData>();
            args.Add(new ItemTappedEventArgsData
            {
                EventArgs = eventArgs.Item as ItemProperty,
                PageName = pagename
            });

            return args;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}