namespace MahjongScoreManager_MUI.ViewModels
{
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Models;
    using MahjongScoreManager_MUI.Common;
    using System;

    /// <summary>
    /// 対局情報参照のViewModelクラス
    /// </summary>
    public class GameResultViewModel : ViewModelBase
    {
        #region コマンド・プロパティ
        /// <summary>
        /// 「検索」コマンド
        /// </summary>
        private DelegateCommand executeCommand;
        public DelegateCommand ExecuteCommand
        {
            get
            {
                if (executeCommand == null)
                {
                    executeCommand = new DelegateCommand(Execute, CanExecute);
                }
                return executeCommand;
            }
        }
        /// <summary>
        /// 検索日付（自）
        /// </summary>
        public DateTime FindFrom { get; set; } = DateTime.Now;
        /// <summary>
        /// 検索日付（至）
        /// </summary>
        public DateTime FindTo { get; set; } = DateTime.Now;
        /// <summary>
        /// 対局者コレクション
        /// </summary>
        public ObservableCollection<Person> ColPerson { get; set; }
        /// <summary>
        /// 対局設定コレクション
        /// </summary>
        public ObservableCollection<GameSettingType4> ColRule { get; set; }
        /// <summary>
        /// 対局コレクション
        /// </summary>
        public ObservableCollection<GameType4> ColGame { get; set; }
        /// <summary>
        /// 出力用コレクション
        /// </summary>
        private ObservableCollection<DataGridResult> colReulst;
        public ObservableCollection<DataGridResult> ColResult
        {
            get
            {
                return colReulst;
            }
            set
            {
                colReulst = value;
                RaisePropertyChanged("ColResult");
            }
        }
        private static string filePathPlayers = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathPlayers);
        private static string filePathSettings = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathSettings_Type4);
        private static string filePathGames = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathGames_Type4);
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameResultViewModel()
        {
            var displayTuple = Load();
            ColPerson = displayTuple.Item1;
            ColRule = displayTuple.Item2;
            ColGame = displayTuple.Item3;
        }

        /// <summary>
        /// 検索のコマンド実行
        /// </summary>
        public void Execute()
        {
            // 初期化
            ColResult = new ObservableCollection<DataGridResult>();

            // 一応リロード
            var displayTuple = Load();
            ColPerson = displayTuple.Item1;
            ColRule = displayTuple.Item2;
            ColGame = displayTuple.Item3;

            // クラスを作って
            var gameResultModel = new GameResultModel()
            {
                // オブジェクトを渡して
                ColPerson = ColPerson,
                ColRule = ColRule,
                ColGame = ColGame,
                FindFrom = FindFrom,
                FindTo = FindTo
            };

            // 動かして戻す
            ColResult = gameResultModel.Execute();
        }

        /// <summary>
        /// 検索が実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool CanExecute()
        {
            // 日付の自至が逆転していなければOK
            return FindFrom <= FindTo;
        }

        /// <summary>
        /// データをXMLから読み込む
        /// </summary>
        /// <returns></returns>
        private Tuple<ObservableCollection<Person>, ObservableCollection<GameSettingType4>, ObservableCollection<GameType4>> Load()
        {
            var ret1 = XmlConverter.DeSerializeToCol<Person>(filePathPlayers);

            if (ret1 is null)
            {
                ret1 = new ObservableCollection<Person>();
                // XMLファイルに保存
                // ViewModelで読み込むので保存も収まりよくこちらに
                if (XmlConverter.SerializeFromCol(ColPerson, filePathPlayers))
                {
                    // 成功
                }
                else
                {
                    MessageBox.Show("XMLファイルの保存に失敗しました");
                }
            }

            var ret2 = XmlConverter.DeSerializeToCol<GameSettingType4>(filePathSettings);

            if (ret2 is null)
            {
                ret2 = new ObservableCollection<GameSettingType4>();
                // XMLファイルに保存
                // ViewModelで読み込むので保存も収まりよくこちらに
                if (XmlConverter.SerializeFromCol(ColRule, filePathSettings))
                {
                    // 成功
                }
                else
                {
                    MessageBox.Show("XMLファイルの保存に失敗しました");
                }
            }

            var ret3 = XmlConverter.DeSerializeToCol<GameType4>(filePathGames);

            if (ret3 is null)
            {
                ret3 = new ObservableCollection<GameType4>();
                // XMLファイルに保存
                // ViewModelで読み込むので保存も収まりよくこちらに
                if (XmlConverter.SerializeFromCol(ColGame, filePathGames))
                {
                    // 成功
                }
                else
                {
                    MessageBox.Show("XMLファイルの保存に失敗しました");
                }
            }

            var ret = Tuple.Create(ret1, ret2, ret3);

            return ret;
        }
    }
}
