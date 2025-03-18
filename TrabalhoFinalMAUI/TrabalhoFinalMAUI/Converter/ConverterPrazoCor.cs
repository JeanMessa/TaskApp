using System.Globalization;

namespace TrabalhoFinalMAUI.Converter
{
    public class ConverterPrazoCor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string prazo = (string)value;
            AppTheme TemaAtual = Application.Current.RequestedTheme;
            if (prazo == "Em Atraso")
            {
                return Color.FromArgb("#c90404");
            }
            else if (prazo == "Hoje")
            {
                return Color.FromArgb("#dea712");
            }
            else if (prazo == "Concluída")
            {
                if (TemaAtual == AppTheme.Dark)
                {
                    return Color.FromArgb("#46A832");
                }
                else
                {
                    return Color.FromArgb("#1A5E03");
                }

            }
            else
            {

                if (TemaAtual == AppTheme.Dark)
                {
                    return Color.FromArgb("#FFFFFF");
                }
                else
                {
                    return Color.FromArgb("#000000");
                }
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

    }
}
