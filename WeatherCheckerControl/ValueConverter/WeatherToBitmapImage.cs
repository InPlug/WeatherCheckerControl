using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections.Generic;

namespace Vishnu_UserModules.ValueConverter
{

    /// <summary>
    /// Setzt einen Wettertyp als string in ein BitmapImage um.
    /// </summary>
    /// <remarks>
    /// Autor: Erik Nagel
    ///
    /// 18.10.2022 Erik Nagel: erstellt.
    /// </remarks>
    public class WeatherToBitmapImage : IMultiValueConverter
    {
        private static readonly Dictionary<string, string> _weatherToImageName
            = new Dictionary<string, string>() {
                {"clearday", "clearday"},
                {"", "clearday"},
                {"clearnight", "clearnight"},
                {"cloudyday", "cloudyday"},
                {"cloudynight", "cloudynight"},
                {"humidday", "humidday"},
                {"humidnight", "humidnight"},
                {"ishowerday", "ishowerday"},
                {"ishowernight", "ishowernight"},
                {"lightrainday", "lightrainday"},
                {"lightrainnight", "lightrainnight"},
                {"lightsnowday", "lightsnowday"},
                {"lightsnownight", "lightsnownight"},
                {"mcloudyday", "mcloudyday"},
                {"mcloudynight", "mcloudynight"},
                {"oshowerday", "oshowerday"},
                {"oshowernight", "oshowernight"},
                {"pcloudyday", "pcloudyday"},
                {"pcloudynight", "pcloudynight"},
                {"rainday", "rainday"},
                {"rainnight", "rainnight"},
                {"rainsnowday", "rainsnowday"},
                {"rainsnownight", "rainsnownight"},
                {"snowday", "snowday"},
                {"snownight", "snownight"},
                {"tsday", "tsday"},
                {"tsnight", "tsnight"},
                {"tsrainday", "tsrainday"},
                {"tsrainnight", "tsrainnight"},
                {"windy", "windy"}
            };

        /// <summary>
        /// Übersetzt einen Wetter-Typ (string) in ein Bild.
        /// </summary>
        /// <param name="values">Array: [Wettertyp als string][ResourceDictionary mit BitmapImages].</param>
        /// <param name="targetType">Der Zieltyp (BitmapImage).</param>
        /// <param name="parameter">Wird nicht genutzt.</param>
        /// <param name="culture">Sprache, Sonderzeichen.</param>
        /// <returns>BitmapImage.</returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()) || (values[0] == DependencyProperty.UnsetValue))
            {
                return new BitmapImage(new Uri("./Media/clearday.png", UriKind.Relative));
            }
            string weather = (string)values[0];
            ResourceDictionary targets = (ResourceDictionary)(values[1]);
            BitmapImage img = (BitmapImage)(targets[_weatherToImageName[weather]]);
            return img;
        }

        /// <summary>
        /// Ist nicht implementiert.
        /// </summary>
        /// <param name="value">BitmapImage.</param>
        /// <param name="targetTypes">Array: [Wettertyp als string][ResourceDictionary mit BitmapImages].</param>
        /// <param name="parameter">Wird nicht genutzt.</param>
        /// <param name="culture">Sprache, Sonderzeichen.</param>
        /// <returns>Wettertyp als string.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
