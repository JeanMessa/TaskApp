using System.Globalization;

namespace TrabalhoFinalMAUI.Converter
{
    public class ConverterThemeIcone : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String source = parameter.ToString();
            AppTheme TemaAtual = Application.Current.RequestedTheme;
            if (TemaAtual == AppTheme.Dark)
            {
                source += "_dark";
            }
            return source + ".png";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

    }
}
