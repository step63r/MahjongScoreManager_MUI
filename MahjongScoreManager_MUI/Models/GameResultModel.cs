namespace MahjongScoreManager_MUI.Models
{
    using MahjongScoreManager_MUI.Common;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// GameResultのModelクラス
    /// </summary>
    public class GameResultModel
    {
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
        /// 検索日付（自）
        /// </summary>
        public DateTime FindFrom { get; set; }
        /// <summary>
        /// 検索日付（至）
        /// </summary>
        public DateTime FindTo { get; set; }

        public ObservableCollection<DataGridResult> Execute()
        {
            var ColResult = new ObservableCollection<DataGridResult>();

            // 対局情報の編集
            foreach (var oneGame in ColGame)
            {
                if (oneGame.GameDate >= FindFrom && oneGame.GameDate <= FindTo)
                {
                    var oneSetting = ColRule.Where(item => item.ID == oneGame.SettingID).First();
                    var oneData = new DataGridResult();
                    // 対局者を一人ずつ確認
                    // これまでに登場していなければオブジェクトを新規作成
                    #region 東家
                    if (ColResult.Where(item => item.ID == oneGame.East).Count() == 0)
                    {
                        oneData = new DataGridResult()
                        {
                            ID = oneGame.East,
                            Name = ColPerson.Where(item => item.ID == oneGame.East).First().Name,
                            GameCount = 0,
                            TotalScore = 0,
                            TotalScoreRate = 0.0
                        };
                        // コレクションに追加
                        ColResult.Add(oneData);
                    }

                    oneData = ColResult.Where(item => item.ID == oneGame.East).First();
                    oneData.GameCount += 1;
                    oneData.TotalScore += oneGame.EastScore;
                    oneData.TotalScoreRate += oneGame.EastScore * oneSetting.Rate * 1000;
                    #endregion

                    #region 南家
                    if (ColResult.Where(item => item.ID == oneGame.South).Count() == 0)
                    {
                        oneData = new DataGridResult()
                        {
                            ID = oneGame.South,
                            Name = ColPerson.Where(item => item.ID == oneGame.South).First().Name,
                            GameCount = 0,
                            TotalScore = 0,
                            TotalScoreRate = 0.0
                        };
                        // コレクションに追加
                        ColResult.Add(oneData);
                    }

                    oneData = ColResult.Where(item => item.ID == oneGame.South).First();
                    oneData.GameCount += 1;
                    oneData.TotalScore += oneGame.SouthScore;
                    oneData.TotalScoreRate += oneGame.SouthScore * oneSetting.Rate * 1000;
                    #endregion

                    #region 西家
                    if (ColResult.Where(item => item.ID == oneGame.West).Count() == 0)
                    {
                        oneData = new DataGridResult()
                        {
                            ID = oneGame.West,
                            Name = ColPerson.Where(item => item.ID == oneGame.West).First().Name,
                            GameCount = 0,
                            TotalScore = 0,
                            TotalScoreRate = 0.0
                        };
                        // コレクションに追加
                        ColResult.Add(oneData);
                    }

                    oneData = ColResult.Where(item => item.ID == oneGame.West).First();
                    oneData.GameCount += 1;
                    oneData.TotalScore += oneGame.WestScore;
                    oneData.TotalScoreRate += oneGame.WestScore * oneSetting.Rate * 1000;
                    #endregion

                    #region 北家
                    if (ColResult.Where(item => item.ID == oneGame.North).Count() == 0)
                    {
                        oneData = new DataGridResult()
                        {
                            ID = oneGame.North,
                            Name = ColPerson.Where(item => item.ID == oneGame.North).First().Name,
                            GameCount = 0,
                            TotalScore = 0,
                            TotalScoreRate = 0.0
                        };
                        // コレクションに追加
                        ColResult.Add(oneData);
                    }

                    oneData = ColResult.Where(item => item.ID == oneGame.North).First();
                    oneData.GameCount += 1;
                    oneData.TotalScore += oneGame.NorthScore;
                    oneData.TotalScoreRate += oneGame.NorthScore * oneSetting.Rate * 1000;
                    #endregion
                }
            }

            // 総得点の降順でソート
            ColResult = new ObservableCollection<DataGridResult>(ColResult.OrderByDescending(item => item.TotalScore));

            return ColResult;
        }
    }
}
