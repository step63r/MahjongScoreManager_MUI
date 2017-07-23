namespace MahjongScoreManager_MUI.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Common;

    /// <summary>
    /// 対局者設定のModelクラス
    /// </summary>
    public class PlayerSettingModel
    {
        /// <summary>
        /// 対局者コレクション
        /// </summary>
        public ObservableCollection<Person> ColPerson;

        /// <summary>
        /// 対局者を追加する
        /// </summary>
        /// <param name="name">追加する対局者名</param>
        public void AddPlayer(string name)
        {
            var retPerson = new Person
            {
                ID = GetId(),
                Name = name
            };

            ColPerson.Add(retPerson);
        }

        /// <summary>
        /// 対局者を削除する（ListBoxのBindingからSelectedItemを取得する想定）
        /// </summary>
        /// <param name="person"></param>
        public void DeletePlayer(Person person)
        {
            ColPerson.Remove(person);
        }

        /// <summary>
        /// 対局者コレクションから未使用のIDを取得する
        /// </summary>
        /// <returns>未使用のIDのうち最も小さい数字</returns>
        private int GetId()
        {
            int ret = 1;
            List<int> IDList = new List<int>();

            foreach(Person person in ColPerson)
            {
                IDList.Add(person.ID);
            }

            // 昇順に並び替える（はず）
            IDList.Sort();

            foreach(int id in IDList)
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
