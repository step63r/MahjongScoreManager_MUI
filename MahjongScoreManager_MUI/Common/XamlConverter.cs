namespace MahjongScoreManager_MUI.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

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

    /****************************************************************
     *                                                              *
     * ソース                                                       *
     * http://y-maeyama.hatenablog.com/entry/20101104/1288867106    *
     *                                                              *
     ****************************************************************/
    /// <summary>
    /// 負値を赤色にするConverter
    /// </summary>
    public class BlushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number;
            if (double.TryParse(value as string, out number))
            {
                if (number >= 0)
                {
                    // 負の数だった場合、黒を返す
                    return new SolidColorBrush(Colors.Black);
                }
                else
                {
                    // 負の数だった場合、赤を返す
                    return new SolidColorBrush(Colors.Red);
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // BindingModeがOneWayを想定しているので未実装で問題ない。
            throw new NotImplementedException();
        }
    }
}
