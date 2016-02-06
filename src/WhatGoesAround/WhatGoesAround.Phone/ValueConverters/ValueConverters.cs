using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace WhatGoesAround.Phone.ValueConverters
{
    // converters
    public class ButtonColorConverter : IValueConverter
    {
        private static SolidColorBrush brush1 = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        private static SolidColorBrush brush2 = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int color = (int)value;
            return color == 1 ? brush1 : brush2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visible = (bool)value;
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
