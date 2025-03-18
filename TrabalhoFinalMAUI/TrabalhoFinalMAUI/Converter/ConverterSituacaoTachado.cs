using System.Globalization;

namespace TrabalhoFinalMAUI.Converter
{
    public class ConverterSituacaoTachado : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool situacao = (bool)value;
            if (situacao)
            {
                return TextDecorations.Strikethrough;
            }
            else
            {
                return TextDecorations.None;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

    }

}

