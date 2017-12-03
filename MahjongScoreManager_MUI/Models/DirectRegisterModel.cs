namespace MahjongScoreManager_MUI.Models
{
    using MahjongScoreManager_MUI.Common;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// DirectRegisterのModelクラス
    /// </summary>
    public class DirectRegisterModel
    {
        /// <summary>
        /// 対局コレクション（保存で使用）
        /// </summary>
        public ObservableCollection<GameType4> ColGame;

        /// <summary>
        /// 対局を保存する
        /// </summary>
        public void ExecuteSave(GameType4 game)
        {
            ColGame.Add(game);
        }
    }
}
