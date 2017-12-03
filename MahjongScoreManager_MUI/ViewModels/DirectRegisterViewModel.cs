namespace MahjongScoreManager_MUI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Models;
    using MahjongScoreManager_MUI.Common;

    /// <summary>
    /// DirectRegisterのViewModelクラス
    /// </summary>
    public class DirectRegisterViewModel : ViewModelBase
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
        /// 「対局者・設定を更新」コマンド
        /// </summary>
        private DelegateCommand reloadCommand;
        public DelegateCommand ReloadCommand
        {
            get
            {
                if (reloadCommand == null)
                {
                    reloadCommand = new DelegateCommand(Reload, CanReload);
                }
                return reloadCommand;
            }
        }
        /// <summary>
        /// 対局者コレクション
        /// </summary>
        private ObservableCollection<Person> colPerson;
        public ObservableCollection<Person> ColPerson
        {
            get
            {
                return colPerson;
            }
            set
            {
                colPerson = value;
                RaisePropertyChanged("ColPerson");
            }
        }
        /// <summary>
        /// 対局設定コレクション
        /// </summary>
        private ObservableCollection<GameSettingType4> colRule;
        public ObservableCollection<GameSettingType4> ColRule
        {
            get
            {
                return colRule;
            }
            set
            {
                colRule = value;
                RaisePropertyChanged("ColRule");
            }
        }
        /// <summary>
        /// 対局コレクション
        /// </summary>
        private ObservableCollection<GameType4> colGame;
        public ObservableCollection<GameType4> ColGame
        {
            get
            {
                return colGame;
            }
            set
            {
                colGame = value;
                RaisePropertyChanged("ColGame");
            }
        }
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
        /// 計算の不一致を無視する（既定値：true）
        /// </summary>
        public bool IgnoreUnbalance { get; set; } = true;
        /// <summary>
        /// 対局日付
        /// </summary>
        public DateTime GameDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 選択中の対局者（東家）
        /// </summary>
        private Person selectedPersonEast;
        public Person SelectedPersonEast
        {
            get
            {
                return selectedPersonEast;
            }
            set
            {
                selectedPersonEast = value;
                RaisePropertyChanged("SelectedPersonEast");
            }
        }
        public int EastPriseScore { get; set; }
        /// <summary>
        /// 選択中の対局者（南家）
        /// </summary>
        private Person selectedPersonSouth;
        public Person SelectedPersonSouth
        {
            get
            {
                return selectedPersonSouth;
            }
            set
            {
                selectedPersonSouth = value;
                RaisePropertyChanged("SelectedPersonSouth");
            }
        }
        public int SouthPriseScore { get; set; }
        /// <summary>
        /// 選択中の対局者（西家）
        /// </summary>
        private Person selectedPersonWest;
        public Person SelectedPersonWest
        {
            get
            {
                return selectedPersonWest;
            }
            set
            {
                selectedPersonWest = value;
                RaisePropertyChanged("SelectedPersonWest");
            }
        }
        public int WestPriseScore { get; set; }
        /// <summary>
        /// 選択中の対局者（北家）
        /// </summary>
        private Person selectedPersonNorth;
        public Person SelectedPersonNorth
        {
            get
            {
                return selectedPersonNorth;
            }
            set
            {
                selectedPersonNorth = value;
                RaisePropertyChanged("SelectedPersonNorth");
            }
        }
        public int NorthPriseScore { get; set; }
        /// <summary>
        /// 対局クラス
        /// </summary>
        private GameType4 ThisGame;
        private static string filePathPlayers = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathPlayers);
        private static string filePathSettings = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathSettings_Type4);
        private static string filePathGames = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathGames_Type4);
        #endregion
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DirectRegisterViewModel()
        {
            var displayTuple = Load();
            ColPerson = displayTuple.Item1;
            ColRule = displayTuple.Item2;
            ColGame = displayTuple.Item3;
        }

        /// <summary>
        /// 対局保存のコマンド実行
        /// </summary>
        public void GenerateGame()
        {
            var dialogRet = MessageBox.Show("対局を保存します。", "確認", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Information);
            if (dialogRet == System.Windows.MessageBoxResult.Yes)
            {
                // 保存オブジェクトを作る
                ThisGame = new GameType4()
                {
                    GameDate = GameDate,
                    SettingID = SelectedRule.ID,
                    East = SelectedPersonEast.ID,
                    EastScore = EastPriseScore,
                    South = SelectedPersonSouth.ID,
                    SouthScore = SouthPriseScore,
                    West = SelectedPersonWest.ID,
                    WestScore = WestPriseScore,
                    North = SelectedPersonNorth.ID,
                    NorthScore = NorthPriseScore
                };

                // クラスを作って
                var directRegisterModel = new DirectRegisterModel()
                {
                    // オブジェクトを渡して
                    ColGame = ColGame
                };

                // 動かして
                directRegisterModel.ExecuteSave(ThisGame);

                // 戻す
                ColGame = directRegisterModel.ColGame;

                // XMLファイルに保存
                // ViewModelで読み込むので保存も収まりよくこちらに
                if (XmlConverter.SerializeFromCol(ColGame, filePathGames))
                {
                    // 成功
                }
                else
                {
                    // 失敗
                }
            }
        }

        /// <summary>
        /// 対局保存が実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool CanGenerateGame()
        {
            // 計算の不一致を無視するかどうか
            if (IgnoreUnbalance)
            {
                // 計算結果を考慮せず判定
                return SelectedPersonEast != null && SelectedPersonSouth != null && SelectedPersonWest != null && SelectedPersonNorth != null;
            }
            else
            {
                // 計算結果も考慮して判定
                return SelectedPersonEast != null && SelectedPersonSouth != null && SelectedPersonWest != null && SelectedPersonNorth != null && (EastPriseScore + SouthPriseScore + WestPriseScore + NorthPriseScore == 0);
            }
        }

        /// <summary>
        /// 設定更新のコマンド実行
        /// </summary>
        public void Reload()
        {
            var displayTuple = Load();
            ColPerson = displayTuple.Item1;
            ColRule = displayTuple.Item2;
            ColGame = displayTuple.Item3;
        }

        /// <summary>
        /// 設定更新が実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool CanReload()
        {
            return true;
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
