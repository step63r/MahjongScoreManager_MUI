namespace MahjongScoreManager_MUI.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Enumの列挙値から文字列を取得するコンバータ
    /// </summary>
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetName(typeof(HandleFraction), (HandleFraction)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
