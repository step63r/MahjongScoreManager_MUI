﻿namespace MahjongScoreManager_MUI.ViewModels
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
        /// 「計算」コマンド
        /// </summary>
        private DelegateCommand calcGameCommand;
        public DelegateCommand CalcGameCommand
        {
            get
            {
                if (calcGameCommand == null)
                {
                    calcGameCommand = new DelegateCommand(CalcGame, CanCalcGame);
                }
                return calcGameCommand;
            }
        }
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
        /// 対局コレクション
        /// </summary>
        public ObservableCollection<GameType4> ColGame { get; set; }
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
        public int EastBaseScore { get; set; }
        public int EastPriseScore { get; set; }
        private int eastCalcedScore;
        public int EastCalcedScore
        {
            get
            {
                return eastCalcedScore;
            }
            set
            {
                eastCalcedScore = value;
                RaisePropertyChanged("EastCalcedScore");
            }
        }
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
        public int SouthBaseScore { get; set; }
        public int SouthPriseScore { get; set; }
        private int southCalcedScore;
        public int SouthCalcedScore
        {
            get
            {
                return southCalcedScore;
            }
            set
            {
                southCalcedScore = value;
                RaisePropertyChanged("SouthCalcedScore");
            }
        }
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
        public int WestBaseScore { get; set; }
        public int WestPriseScore { get; set; }
        private int westCalcedScore;
        public int WestCalcedScore
        {
            get
            {
                return westCalcedScore;
            }
            set
            {
                westCalcedScore = value;
                RaisePropertyChanged("WestCalcedScore");
            }
        }
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
        public int NorthBaseScore { get; set; }
        public int NorthPriseScore { get; set; }
        private int northCalcedScore;
        public int NorthCalcedScore
        {
            get
            {
                return northCalcedScore;
            }
            set
            {
                northCalcedScore = value;
                RaisePropertyChanged("NorthCalcedScore");
            }
        }
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
        public NewGameViewModel()
        {
            var displayTuple = Load();
            ColPerson = displayTuple.Item1;
            ColRule = displayTuple.Item2;
            ColGame = displayTuple.Item3;
        }

        /// <summary>
        /// 対局計算のコマンド実行
        /// </summary>
        public void CalcGame()
        {
            // クラスを作って
            var newGameModel = new NewGameModel()
            {
                // オブジェクトを渡して
                EastBaseScore = EastBaseScore,
                EastPriseScore = EastPriseScore,
                SouthBaseScore = SouthBaseScore,
                SouthPriseScore = SouthPriseScore,
                WestBaseScore = WestBaseScore,
                WestPriseScore = WestPriseScore,
                NorthBaseScore = NorthBaseScore,
                NorthPriseScore = NorthPriseScore,
                Setting = SelectedRule
            };

            // 動かして戻す
            var ret = newGameModel.ExecuteCalc();

            EastCalcedScore = (int)ret[0];
            SouthCalcedScore = (int)ret[1];
            WestCalcedScore = (int)ret[2];
            NorthCalcedScore = (int)ret[3];
        }

        /// <summary>
        /// 対局計算が実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool CanCalcGame()
        {
            if (!(SelectedRule is null))
            {
                return EastBaseScore + SouthBaseScore + WestBaseScore + NorthBaseScore == SelectedRule.BasePoint * 4;
            }
            else
            {
                return false;
            }
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
                    EastScore = EastCalcedScore,
                    South = SelectedPersonSouth.ID,
                    SouthScore = SouthCalcedScore,
                    West = SelectedPersonWest.ID,
                    WestScore = WestCalcedScore,
                    North = SelectedPersonNorth.ID,
                    NorthScore = NorthCalcedScore
                };

                // クラスを作って
                var newGameModel = new NewGameModel()
                {
                    // オブジェクトを渡して
                    ColGame = ColGame
                };

                // 動かして
                newGameModel.ExecuteSave(ThisGame);

                // 戻す
                ColGame = newGameModel.ColGame;

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
            return Math.Abs(EastCalcedScore) + Math.Abs(SouthCalcedScore) + Math.Abs(WestCalcedScore) + Math.Abs(NorthCalcedScore) > 0;
        }

        /// <summary>
        /// 対局者をXMLから読み込む
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
