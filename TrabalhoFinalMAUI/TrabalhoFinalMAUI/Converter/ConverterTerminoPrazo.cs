using System.Globalization;

namespace TrabalhoFinalMAUI.Converter
{
    public class ConverterTerminoPrazo : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool situacao = new bool();
            DateTime termino = new DateTime();
            foreach (var value in values)
            {
                if (value != null)
                {
                    if (value is bool)
                    {
                        situacao = (bool)value;
                    }
                    else if (value is DateTime)
                    {
                        termino = (DateTime)value;
                    }
                }
            }

            if (!situacao)
            {

                termino = termino;
                DateTime hoje = DateTime.Now;
                TimeSpan prazo = termino.Date - hoje.Date;
                int diasprazo = prazo.Days;
                if (diasprazo > 0)
                {
                    if (diasprazo == 1)
                    {
                        return diasprazo + " dia";
                    }
                    else
                    {
                        return diasprazo + " dias";
                    }
                }
                else if (diasprazo == 0)
                {
                    if (termino >= hoje)
                    {
                        return "Hoje";
                    }
                    else 
                    {
                        return "Em atraso";
                    }
                }
                else
                {
                    return "Em Atraso";
                }
            }
            else
            {
                return "Concluída";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
