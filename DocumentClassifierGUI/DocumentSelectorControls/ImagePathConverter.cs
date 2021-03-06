﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    /// <summary>
    /// Konwerter pomagajacy w konwersji statusu dokumentu na kolor jego statusu w GUI.
    /// </summary>
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (Document.Status)value;

            return status switch
            {
                Document.Status.New => Brushes.Red,
                Document.Status.Marked => Brushes.Yellow,
                Document.Status.MaskGenerated => Brushes.Green,
                _ => throw new ApplicationException("No Document Status")
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
