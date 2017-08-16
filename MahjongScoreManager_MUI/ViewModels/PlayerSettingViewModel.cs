namespace MahjongScoreManager_MUI.ViewModels
{
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Models;
    using MahjongScoreManager_MUI.Common;

    /// <summary>
    /// 対局者設定のViewModelクラス
    /// </summary>
    public class PlayerSettingViewModel : ViewModelBase
    {
        #region コマンド・プロパティ
        /// <summary>
        /// 「追加」コマンド
        /// </summary>
        private DelegateCommand addCommand;
        public DelegateCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new DelegateCommand(AddPlayer, CanAddPlayerExecute);
                }
                return addCommand;
            }
        }
        /// <summary>
        /// 「削除」コマンド
        /// </summary>
        private DelegateCommand removeCommand;
        public DelegateCommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new DelegateCommand(RemovePlayer, CanRemovePlayerExecute);
                }
                return removeCommand;
            }
        }
        /// <summary>
        /// 対局者コレクション
        /// </summary>
        public ObservableCollection<Person> ColPerson { get; set; }
        /// <summary>
        /// 入力名
        /// </summary>
        public string InputName { get; set; } = "";
        /// <summary>
        /// 選択中の対局者オブジェクト
        /// </summary>
        public Person SelectedPlayer { get; set; }
        /// <summary>
        /// XMLファイルパス
        /// </summary>
        private static string filePath = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathPlayers);
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="colPerson"></param>
        public PlayerSettingViewModel() => ColPerson = Load();

        /// <summary>
        /// 対局者追加のコマンド実行
        /// </summary>
        public void AddPlayer()
        {
            // クラスを作って
            var playerSettingModel = new PlayerSettingModel()
            {
                // オブジェクトを渡して
                ColPerson = ColPerson
            };
            // 動かして
            playerSettingModel.AddPlayer(InputName);
            // 戻す
            ColPerson = playerSettingModel.ColPerson;

            // XMLファイルに保存
            // ViewModelで読み込むので保存も収まりよくこちらに
            if (XmlConverter.SerializeFromCol(ColPerson, filePath))
            {
                // 成功
            }
            else
            {
                // 失敗
            }
        }

        /// <summary>
        /// 対局者追加が実行可能かどうかを判定
        /// </summary>
        /// <returns>入力名が空でなければtrue</returns>
        private bool CanAddPlayerExecute()
        {
            return InputName != "";
        }

        /// <summary>
        /// 対局者削除のコマンド実行
        /// </summary>
        public void RemovePlayer()
        {
            // クラスを作って
            var playerSettingModel = new PlayerSettingModel()
            {
                // オブジェクトを渡して
                ColPerson = ColPerson
            };
            // 動かして
            playerSettingModel.DeletePlayer(SelectedPlayer);
            // 戻す
            ColPerson = playerSettingModel.ColPerson;

            // XMLファイルに保存
            // ViewModelで読み込むので保存も収まりよくこちらに
            if (XmlConverter.SerializeFromCol(ColPerson, filePath))
            {
                // 成功
            }
            else
            {
                // 失敗
            }
        }

        /// <summary>
        /// 対局者削除が実行可能かどうかを判定
        /// </summary>
        /// <returns>対局者が選択されていればtrue</returns>
        private bool CanRemovePlayerExecute()
        {
            return SelectedPlayer != null;
        }

        /// <summary>
        /// 対局者情報をXMLから読み込む
        /// </summary>
        /// <returns>対局者コレクション</returns>
        private ObservableCollection<Person> Load()
        {
            var ret = XmlConverter.DeSerializeToCol<Person>(filePath);
            if(ret is null)
            {
                ret = new ObservableCollection<Person>();
                // XMLファイルに保存
                // ViewModelで読み込むので保存も収まりよくこちらに
                if (XmlConverter.SerializeFromCol(ColPerson, filePath))
                {
                    // 成功
                }
                else
                {
                    MessageBox.Show("XMLファイルの保存に失敗しました");
                }

            }

            return ret;
        }
    }
}
