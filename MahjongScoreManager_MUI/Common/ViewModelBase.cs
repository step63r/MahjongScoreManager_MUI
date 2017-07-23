﻿/****************************************************************
 *                                                              *
 * ソース                                                       *
 * https://code.msdn.microsoft.com/windowsdesktop/MVVM-d8261534 *
 *                                                              *
 ****************************************************************/
namespace MahjongScoreManager_MUI.Common
{
    using System.ComponentModel;

    /// <summary>
    /// ViewModelの基本クラス。INotifyPropertyChangedの実装を提供します。
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティの変更があったときに発行されます。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChangedイベントを発行します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}