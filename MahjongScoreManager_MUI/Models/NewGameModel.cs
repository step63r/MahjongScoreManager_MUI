namespace MahjongScoreManager_MUI.Models
{
    using MahjongScoreManager_MUI.Common;
    using System;

    /// <summary>
    /// NewGameのModelクラス
    /// </summary>
    public class NewGameModel
    {
        /// <summary>
        /// 東家
        /// </summary>
        public int EastBaseScore;
        public int EastPriseScore;
        /// <summary>
        /// 南家
        /// </summary>
        public int SouthBaseScore;
        public int SouthPriseScore;
        /// <summary>
        /// 西家
        /// </summary>
        public int WestBaseScore;
        public int WestPriseScore;
        /// <summary>
        /// 北家
        /// </summary>
        public int NorthBaseScore;
        public int NorthPriseScore;
        /// <summary>
        /// 設定オブジェクト
        /// </summary>
        public GameSettingType4 Setting;

        /// <summary>
        /// スコアを計算する
        /// </summary>
        /// <returns>要素の小さい方から [東, 南, 西, 北] のスコア</returns>
        public double[] Execute()
        {
            // 返しとの差を取る（端数計算を考慮する）
            var ret = new double[4] {
                Setting.ReturnPoint - EastBaseScore,
                Setting.ReturnPoint - SouthBaseScore,
                Setting.ReturnPoint - WestBaseScore,
                Setting.ReturnPoint - NorthBaseScore
            };

            ret = CalcFraction(ret);

            // 順位を算出する（同率は上家が上位と見なす）
            var rank = new int[4];
            for (int i = 0; i <= 4; i++)
            {
                // 順位を保持しておく変数
                int p = 1;

                for (int j = 0; j <= 4; j++)
                {
                    // 自分と比較しない
                    if (i == j)
                    {
                        continue;
                    }
                    // 比較先が自分より大きかったら
                    if (ret[i] < ret[j])
                    {
                        // 順位を1下げる
                        p++;
                        continue;
                    }
                    // 得点が等しく、かつ自分の方が順番が後だったら
                    if (ret[i] == ret[j] && i > j)
                    {
                        // やっぱり順位を1下げる
                        p++;
                        continue;
                    }
                }

                // 順位を登録
                rank[i] = p;
            }

            // 2～4位を計算してから1位を計算させる
            // 【1位でない場合】
            // ウマの配給
            // 3位→2位
            ret[Array.IndexOf(rank, 2)] += Setting.Prise3To2;
            ret[Array.IndexOf(rank, 3)] -= Setting.Prise3To2;
            // 4位→1位
            ret[Array.IndexOf(rank, 4)] -= Setting.Prise4To1;

            // 1000で割る
            for (int i = 2; i <= 4; i++)
            {
                ret[Array.IndexOf(rank, i)] /= 1000;
            }

            // 【1位の場合】
            // 2～4位の得点を-1倍する
            ret[Array.IndexOf(rank, 1)] = -1 * (ret[Array.IndexOf(rank, 2)] + ret[Array.IndexOf(rank, 3)] + ret[Array.IndexOf(rank, 4)]);
            // 1000で割る
            ret[Array.IndexOf(rank, 1)] /= 1000;

            // 【共通処理】
            // PriseScoreを足す
            ret[0] += EastPriseScore;
            ret[1] += SouthPriseScore;
            ret[2] += WestPriseScore;
            ret[3] += NorthPriseScore;

            return ret;
        }

        /// <summary>
        /// 端数を調整する
        /// </summary>
        /// <param name="score">「返し - 得点」の配列</param>
        /// <returns>調整後得点</returns>
        private double[] CalcFraction(double[] score)
        {
            var ret = new double[4];

            switch (Setting.Fraction)
            {
                case 0:
                    // 四捨五入
                    for (int i = 0; i <= 4; i++)
                    {
                        ret[i] = TypeConverter.Round(score[i], -3);
                    }
                    break;
                case 1:
                    // 五捨六入
                    for (int i = 0; i <= 4; i++)
                    {
                        ret[i] = TypeConverter.RoundExtra(score[i], -3);
                    }
                    break;
                case 2:
                    // 返しと得点の差に依拠
                    for (int i = 0; i <= 4; i++)
                    {
                        ret[i] = TypeConverter.RoundBase(score[i], -3);
                    }
                    break;
                case 3:
                    // 切り捨て
                    for (int i = 0; i <= 4; i++)
                    {
                        ret[i] = TypeConverter.Floor(score[i], -3);
                    }
                    break;
                case 4:
                    // 切り上げ
                    for (int i = 0; i <= 4; i++)
                    {
                        ret[i] = TypeConverter.Ceiling(score[i], -3);
                    }
                    break;
            }

            return ret;
        }
    }
}
