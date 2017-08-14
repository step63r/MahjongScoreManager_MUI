namespace MahjongScoreManager_MUI.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Common;
    using System.Linq;

    /// <summary>
    /// 対局設定のModelクラス
    /// </summary>
    public class RuleSettingModel
    {
        /// <summary>
        /// 対局設定コレクション
        /// </summary>
        public ObservableCollection<GameSettingType4> ColRule;

        /// <summary>
        /// 対局設定を追加する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rate"></param>
        /// <param name="basepoint"></param>
        /// <param name="returnpoint"></param>
        /// <param name="prise3to2"></param>
        /// <param name="prise4to1"></param>
        /// <param name="fraction"></param>
        public void AddRule(string name, double rate, int basepoint, int returnpoint, int prise3to2, int prise4to1, int fraction)
        {
            var retRule = new GameSettingType4
            {
                ID = GetId(),
                Name = name,
                Rate = rate,
                BasePoint = basepoint,
                ReturnPoint = returnpoint,
                Prise3To2 = prise3to2,
                Prise4To1 = prise4to1,
                Fraction = fraction
            };

            ColRule.Add(retRule);
        }

        /// <summary>
        /// 対局設定を更新する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="rate"></param>
        /// <param name="basepoint"></param>
        /// <param name="returnpoint"></param>
        /// <param name="prise3to2"></param>
        /// <param name="prise4to1"></param>
        /// <param name="fraction"></param>
        public void UpdateRule(int id, string name, double rate, int basepoint, int returnpoint, int prise3to2, int prise4to1, int fraction)
        {
            // IDが同一のオブジェクトを探す
            var retRule = ColRule.Where(item => item.ID == id).First();

            {
                retRule.Name = name;
                retRule.Rate = rate;
                retRule.BasePoint = basepoint;
                retRule.ReturnPoint = returnpoint;
                retRule.Prise3To2 = prise3to2;
                retRule.Prise4To1 = prise4to1;
                retRule.Fraction = fraction;
            }
        }

        /// <summary>
        /// 対局設定を削除する（ListBoxのBindingからSelectedItemを取得する想定）
        /// </summary>
        /// <param name="rule"></param>
        public void DeleteRule(GameSettingType4 rule)
        {
            ColRule.Remove(rule);
        }

        /// <summary>
        /// 対局設定コレクションから未使用のIDを取得する
        /// </summary>
        /// <returns>未使用のIDのうち最も小さい数字</returns>
        private int GetId()
        {
            int ret = 1;
            List<int> IDList = new List<int>();

            foreach (var rule in ColRule)
            {
                IDList.Add(rule.ID);
            }

            // 昇順に並び替える（はず）
            IDList.Sort();

            foreach (int id in IDList)
            {
                // IDリストを小さい方から調べてポインタ値より大きければ終了
                if (id > ret)
                {
                    break;
                }
                ret++;
            }

            return ret;
        }
    }
}
