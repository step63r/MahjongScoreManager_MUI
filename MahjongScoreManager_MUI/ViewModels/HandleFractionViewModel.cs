namespace MahjongScoreManager_MUI.ViewModels
{
    using MahjongScoreManager_MUI.Common;
    using System;
    using System.Collections.Generic;

    public class HandleFractionViewModel
    {
        /// <summary>
        /// 端数の取り扱い
        /// </summary>
        public HandleFraction Fraction { get; private set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// HandleFractionと表示名を設定してインスタンスを生成する
        /// </summary>
        /// <param name="fraction">端数の取り扱い</param>
        /// <param name="label">表示名</param>
        public HandleFractionViewModel(HandleFraction fraction, string label)
        {
            Fraction = fraction;
            Label = label;
        }

        /// <summary>
        /// HandleFractionの値と表示用のラベルマップ
        /// </summary>
        private static Dictionary<HandleFraction, string> fractionLabelMap = new Dictionary<HandleFraction, string>
        {
            { HandleFraction.四捨五入, "四捨五入" },
            { HandleFraction.五捨六入, "五捨六入" },
            { HandleFraction.返しと得点の差に依拠, "返しと得点の差に依拠" },
            { HandleFraction.切り捨て, "切り捨て" },
            { HandleFraction.切り上げ, "切り上げ" }
        };

        /// <summary>
        /// HandleFractionからラベル名を自動的に引き当ててHandleFractionViewModelのインスタンスを生成する
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static  HandleFractionViewModel Create(HandleFraction fraction)
        {
            return new HandleFractionViewModel(fraction, fractionLabelMap[fraction]);
        }

        /// <summary>
        /// HandleFractionの全値に対応するHandleFractionViewModelを作成する
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<HandleFractionViewModel> Create()
        {
            foreach(HandleFraction e in Enum.GetValues(typeof(HandleFraction)))
            {
                yield return Create(e);
            }
        }
    }
}
