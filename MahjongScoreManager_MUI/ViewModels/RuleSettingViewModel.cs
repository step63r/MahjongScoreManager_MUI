namespace MahjongScoreManager_MUI.ViewModels
{
    using System.Collections.ObjectModel;
    using MahjongScoreManager_MUI.Models;
    using MahjongScoreManager_MUI.Common;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 対局設定のViewModelクラス
    /// </summary>
    public class RuleSettingViewModel : ViewModelBase
    {
        #region コマンド・プロパティ
        /// <summary>
        /// 「新規登録」コマンド
        /// </summary>
        private DelegateCommand createNewCommand;
        public DelegateCommand CreateNewCommand
        {
            get
            {
                if (createNewCommand == null)
                {
                    createNewCommand = new DelegateCommand(CreateRule, CanCreateRuleExecute);
                }
                return createNewCommand;
            }
        }
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
                    addCommand = new DelegateCommand(AddRule, CanAddRuleExecute);
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
                    removeCommand = new DelegateCommand(RemoveRule, CanRemoveRuleExecute);
                }
                return removeCommand;
            }
        }
        /// <summary>
        /// 現在選択されている端数の取り扱い
        /// </summary>
        private HandleFractionViewModel selectedHandleFraction;
        public HandleFractionViewModel SelectedHandleFraction
        {
            get
            {
                return selectedHandleFraction;
            }
            set
            {
                selectedHandleFraction = value;
                RaisePropertyChanged("SelectedHandleFraction");
            }
        }
        /// <summary>
        /// 対局設定コレクション
        /// </summary>
        public ObservableCollection<GameSettingType4> ColRule { get; set; }
        #region 入力項目
        #endregion
        /// <summary>
        /// 新規登録実行中フラグ
        /// </summary>
        private bool IsCreatingNew = false;
        /// <summary>
        /// 選択中の対局設定オブジェクト
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
        /// 端数の取り扱い
        /// </summary>
        public IEnumerable<HandleFractionViewModel> HandleFractions { get; private set; }
        /// <summary>
        /// XMLファイルパス
        /// </summary>
        private static string filePath = string.Format("{0}/{1}", FilePath.BaseDir, FilePath.XmlPathSettings_Type4);
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RuleSettingViewModel()
        {
            ColRule = Load();
            HandleFractions = HandleFractionViewModel.Create();
            SelectedHandleFraction = HandleFractions.First();
        }

        /// <summary>
        /// 対局設定新規登録のコマンド実行
        /// </summary>
        public void CreateRule()
        {
            SelectedRule = new GameSettingType4();
            IsCreatingNew = true;
        }

        /// <summary>
        /// 対局設定新規登録が実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        private bool CanCreateRuleExecute()
        {
            return true;
        }

        /// <summary>
        /// 対局設定追加のコマンド実行
        /// </summary>
        public void AddRule()
        {
            // クラスを作って
            var ruleSettingModel = new RuleSettingModel()
            {
                // オブジェクトを渡して
                ColRule = ColRule
            };

            if (SelectedRule.ID == 0)
            {
                // 新規登録
                // 動かして
                ruleSettingModel.AddRule(SelectedRule.Name, SelectedRule.Rate, SelectedRule.BasePoint, SelectedRule.ReturnPoint, SelectedRule.Prise3To2, SelectedRule.Prise4To1, SelectedRule.Fraction);
            }
            else
            {
                // 更新
                ruleSettingModel.UpdateRule(SelectedRule.ID, SelectedRule.Name, SelectedRule.Rate, SelectedRule.BasePoint, SelectedRule.ReturnPoint, SelectedRule.Prise3To2, SelectedRule.Prise4To1, SelectedRule.Fraction);
            }

            // 戻す
            ColRule = ruleSettingModel.ColRule;

            // XMLファイルに保存
            // ViewModelで読み込むので保存も収まりよくこちらに
            if (XmlConverter.SerializeFromCol(ColRule, filePath))
            {
                // 成功
            }
            else
            {
                // 失敗
            }

            if (IsCreatingNew)
            {
                IsCreatingNew = false;
            }
        }

        /// <summary>
        /// 対局設定追加が実行可能かどうかを判定
        /// </summary>
        /// <returns>選択中設定がnullでなければtrue</returns>
        private bool CanAddRuleExecute()
        {
            return SelectedRule != null;
        }

        /// <summary>
        /// 対局設定削除のコマンド実行
        /// </summary>
        public void RemoveRule()
        {
            // クラスを作って
            var ruleSettingModel = new RuleSettingModel()
            {
                // オブジェクトを渡して
                ColRule = ColRule
            };
            // 動かして
            ruleSettingModel.DeleteRule(SelectedRule);
            // 戻す
            ColRule = ruleSettingModel.ColRule;

            // XMLファイルに保存
            // ViewModelで読み込むので保存も収まりよくこちらに
            if (XmlConverter.SerializeFromCol(ColRule, filePath))
            {
                // 成功
            }
            else
            {
                // 失敗
            }
        }

        /// <summary>
        /// 対局設定削除が実行可能かどうかを判定
        /// </summary>
        /// <returns>対局設定が選択されていればtrue</returns>
        private bool CanRemoveRuleExecute()
        {
            return SelectedRule != null && !IsCreatingNew;
        }

        /// <summary>
        /// 対局設定情報をXMLから読み込む
        /// </summary>
        /// <returns>対局者コレクション</returns>
        private ObservableCollection<GameSettingType4> Load()
        {
            var ret = XmlConverter.DeSerializeToCol<GameSettingType4>(filePath);
            if (ret is null)
            {
                ret = new ObservableCollection<GameSettingType4>();
                // XMLファイルに保存
                // ViewModelで読み込むので保存も収まりよくこちらに
                if (XmlConverter.SerializeFromCol(ColRule, filePath))
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
