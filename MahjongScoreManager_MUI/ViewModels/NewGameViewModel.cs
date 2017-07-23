namespace MahjongScoreManager_MUI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Models;
    using MahjongScoreManager_MUI.Common;

    /// <summary>
    /// NewGameのViewModelクラス
    /// </summary>
    public class NewGameViewModel : ViewModelBase
    {
        #region コマンド・プロパティ
        /// <summary>
        /// 「保存」コマンド
        /// </summary>
        private DelegateCommand generateGameCommand;
        public DelegateCommand GenerateGameCommand
        {
            get
            {
                if (generateGameCommand == null)
                {
                    generateGameCommand = new DelegateCommand(GenerateGame, CanGenerateGame);
                }
                return generateGameCommand;
            }
        }
        /// <summary>
        /// 対局者コレクション
        /// </summary>
        public ObservableCollection<Person> ColPerson { get; set; }
        /// <summary>
        /// 対局設定コレクション
        /// </summary>
        public ObservableCollection<GameSettingType4> ColRule { get; set; }
        /// <summary>
        /// 現在選択されている対局設定
        /// </summary>
        private GameSettingType4 selectedRule;
        public GameSettingType4 SelectedRule
        {
            get
            {
                return selectedRule;
            }
            set
            {
                selectedRule = value;
                RaisePropertyChanged("SelectedRule");
            }
        }
        /// <summary>
        /// 対局日付
        /// </summary>
        public DateTime GameDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 選択中の対局者（東家）
        /// </summary>
        public Person SelectedPersonEast { get; set; }
        public int EastBaseScore { get; set; }
        public int EastPriseScore { get; set; }
        /// <summary>
        /// 選択中の対局者（南家）
        /// </summary>
        public Person SelectedPersonSouth { get; set; }
        public int SouthBaseScore { get; set; }
        public int SouthPriseScore { get; set; }
        /// <summary>
        /// 選択中の対局者（西家）
        /// </summary>
        public Person SelectedPersonWest { get; set; }
        public int WestBaseScore { get; set; }
        public int WestPriseScore { get; set; }
        /// <summary>
        /// 選択中の対局者（北家）
        /// </summary>
        public Person SelectedPersonNorth { get; set; }
        public int NorthBaseScore { get; set; }
        public int NorthPriseScore { get; set; }
        /// <summary>
        /// 対局クラス
        /// </summary>
        public GameType4 ThisGame { get; set; }
        /// <summary>
        /// 設定オブジェクト
        /// </summary>
        public GameSettingType4 Setting { get; set; }
        private static string filePathPlayers = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathPlayers);
        private static string filePathSettings = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathSettings_Type4);
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NewGameViewModel()
        {
            var displayTuple = Load();
            ColPerson = displayTuple.Item1;
            ColRule = displayTuple.Item2;
        }

        /// <summary>
        /// 対局保存のコマンド実行
        /// </summary>
        public void GenerateGame()
        {

        }

        /// <summary>
        /// 対局保存が実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool CanGenerateGame()
        {
            return false;
        }

        /// <summary>
        /// 対局者をXMLから読み込む
        /// </summary>
        /// <returns></returns>
        private Tuple<ObservableCollection<Person>, ObservableCollection<GameSettingType4>> Load()
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

            var ret = Tuple.Create(ret1, ret2);

            return ret;
        }
    }
}
