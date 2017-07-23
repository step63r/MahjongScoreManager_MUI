﻿/****************************************************************
 *                                                              *
 * ソース                                                       *
 * https://code.msdn.microsoft.com/windowsdesktop/MVVM-d8261534 *
 *                                                              *
 ****************************************************************/
namespace MahjongScoreManager_MUI.Common
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private Action execute;

        private Func<bool> canExecute;

        /// <summary> 
        /// コマンドのExecuteメソッドで実行する処理を指定してDelegateCommandのインスタンスを
        /// 作成します。 
        /// </summary> 
        /// <param name="execute">Executeメソッドで実行する処理</param> 
        public DelegateCommand(Action execute) : this(execute, () => true)
        {
        }

        /// <summary> 
        /// コマンドのExecuteメソッドで実行する処理とCanExecuteメソッドで実行する処理を指定して
        /// DelegateCommandのインスタンスを作成します。 
        /// </summary> 
        /// <param name="execute">Executeメソッドで実行する処理</param> 
        /// <param name="canExecute">CanExecuteメソッドで実行する処理</param> 
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        public void Execute()
        {
            execute();
        }

        /// <summary>
        /// コマンドが実行可能な状態化どうか問い合わせます。
        /// </summary>
        /// <returns>実行可能な場合はtrue</returns>
        public bool CanExecute()
        {
            return canExecute();
        }

        /// <summary>
        /// ICommand.CanExecuteの明示的な実装。CanExecuteメソッドに処理を委譲する。
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary> 
        /// CanExecuteの結果に変更があったことを通知するイベント。
        /// WPFのCommandManagerのRequerySuggestedイベントをラップする形で実装しています。 
        /// </summary> 
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// ICommand.Executeの明示的な実装。Executeメソッドに処理を委譲する。
        /// </summary>
        /// <param name="parameter"></param>
        void ICommand.Execute(object parameter)
        {
            Execute();
        }
    }
}
