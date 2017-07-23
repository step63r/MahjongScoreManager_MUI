﻿using System;
using System.Xml.Serialization;
using System.ComponentModel;

namespace MahjongScoreManager_MUI.Common
{
    /// <summary>
    /// ファイルパス群
    /// </summary>
    public static class FilePath
    {
        /// <summary>
        /// 基底ディレクトリ
        /// </summary>
        public const string BaseDir = "./xml";
        /// <summary>
        /// 対局者XMLファイルパス
        /// </summary>
        public const string XmlPathPlayers = "players.xml";
        /// <summary>
        /// 試合データ（4人打ち）XMLファイルパス
        /// </summary>
        public const string XmlPathGames_Type4 = "games_type4.xml";
        /// <summary>
        /// 対局設定（4人打ち）XMLファイルパス
        /// </summary>
        public const string XmlPathSettings_Type4 = "settings_type4.xml";
        /// <summary>
        /// 試合データ（3人打ち）XMLファイルパス
        /// </summary>
        public const string XmlPathGames_Type3 = "games_type3.xml";
        /// <summary>
        /// 対局設定（3人打ち）XMLファイルパス
        /// </summary>
        public const string XmlPathSettings_Type3 = "settings_type3.xml";
    }

    /// <summary>
    /// 対局者クラス
    /// </summary>
    [XmlRoot("Person")]
    public class Person : INotifyPropertyChanged
    {
        // イベントだけ実装しておく。OnPropertyChangedは使わない
        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        private int _id;
        /// <summary>
        /// 対局者ID
        /// </summary>
        [XmlElement("ID")]
        public int ID
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value))
                {
                    return;
                }
                _id = value;
                PropertyChanged.Raise(() => ID);
            }
        }

        [XmlIgnore]
        private string _name;
        /// <summary>
        /// 対局者名
        /// </summary>
        [XmlElement("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }
                _name = value;
                PropertyChanged.Raise(() => Name);
            }
        }
    }

    /// <summary>
    /// 試合クラス（4人打ち）
    /// </summary>
    [XmlRoot("GameType4")]
    public class GameType4
    {
        /// <summary>
        /// 対局日付
        /// </summary>
        [XmlElement("GameDate")]
        public DateTime GameDate { get; set; }
        /// <summary>
        /// 対局設定
        /// </summary>
        [XmlElement("SettingID")]
        public int SettingID { get; set; }
        /// <summary>
        /// 東家ID
        /// </summary>
        [XmlElement("East")]
        public int East { get; set; }
        /// <summary>
        /// 東家（得点）
        /// </summary>
        public int EastBaseScore { get; set; }
        /// <summary>
        /// 東家（ボーナス）
        /// </summary>
        public int EastPriseScore { get; set; }
        /// <summary>
        /// 南家ID
        /// </summary>
        [XmlElement("South")]
        public int South { get; set; }
        /// <summary>
        /// 南家（得点）
        /// </summary>
        public int SouthBaseScore { get; set; }
        /// <summary>
        /// 南家（ボーナス）
        /// </summary>
        public int SouthPriseScore { get; set; }
        /// <summary>
        /// 西家ID
        /// </summary>
        [XmlElement("West")]
        public int West { get; set; }
        /// <summary>
        /// 西家（得点）
        /// </summary>
        public int WestBaseScore { get; set; }
        /// <summary>
        /// 西家（ボーナス）
        /// </summary>
        public int WestPriseScore { get; set; }
        /// <summary>
        /// 北家ID
        /// </summary>
        [XmlElement("North")]
        public int North { get; set; }
        /// <summary>
        /// 北家（得点）
        /// </summary>
        public int NorthBaseScore { get; set; }
        /// <summary>
        /// 北家（ボーナス）
        /// </summary>
        public int NorthPriseScore { get; set; }
    }

    /// <summary>
    /// 対局設定クラス（4人打ち）
    /// </summary>
    [XmlRoot("GameSettingType4")]
    public class GameSettingType4 : INotifyPropertyChanged
    {
        // イベントだけ実装しておく。OnPropertyChangedは使わない
        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        private int _id;
        /// <summary>
        /// 対局設定ID
        /// </summary>
        [XmlElement("ID")]
        public int ID
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value))
                {
                    return;
                }
                _id = value;
                PropertyChanged.Raise(() => ID);
            }
        }


        [XmlIgnore]
        private string _name;
        /// <summary>
        /// 対局設定名称
        /// </summary>
        [XmlElement("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }
                _name = value;
                PropertyChanged.Raise(() => Name);
            }
        }

        [XmlIgnore]
        private double _rate;
        /// <summary>
        /// レート
        /// </summary>
        [XmlElement("Rate")]
        public double Rate
        {
            get { return _rate; }
            set
            {
                if (Equals(_rate, value))
                {
                    return;
                }
                _rate = value;
                PropertyChanged.Raise(() => Rate);
            }
        }

        [XmlIgnore]
        private int _basepoint;
        /// <summary>
        /// 配給原点
        /// </summary>
        [XmlElement("BasePoint")]
        public int BasePoint
        {
            get { return _basepoint; }
            set
            {
                if (Equals(_basepoint, value))
                {
                    return;
                }
                _basepoint = value;
                PropertyChanged.Raise(() => BasePoint);
            }
        }

        [XmlIgnore]
        private int _returnpoint;
        /// <summary>
        /// 返し
        /// </summary>
        [XmlElement("ReturnPoint")]
        public int ReturnPoint
        {
            get { return _returnpoint; }
            set
            {
                if (Equals(_returnpoint, value))
                {
                    return;
                }
                _returnpoint = value;
                PropertyChanged.Raise(() => ReturnPoint);
            }
        }

        [XmlIgnore]
        private int _prise3to2;
        /// <summary>
        /// ウマ（3位→2位）
        /// </summary>
        [XmlElement("Prise3To2")]
        public int Prise3To2
        {
            get { return _prise3to2; }
            set
            {
                if (Equals(_prise3to2, value))
                {
                    return;
                }
                _prise3to2 = value;
                PropertyChanged.Raise(() => Prise3To2);
            }
        }

        [XmlIgnore]
        private int _prise4to1;
        /// <summary>
        /// ウマ（4位→1位）
        /// </summary>
        [XmlElement("Prise4To1")]
        public int Prise4To1
        {
            get { return _prise4to1; }
            set
            {
                if (Equals(_prise4to1, value))
                {
                    return;
                }
                _prise4to1 = value;
                PropertyChanged.Raise(() => Prise4To1);
            }
        }

        [XmlIgnore]
        private int _fraction;
        /// <summary>
        /// 端数の取り扱い
        /// </summary>
        [XmlElement("Fraction")]
        public int Fraction
        {
            get { return _fraction; }
            set
            {
                if (Equals(_fraction, value))
                {
                    return;
                }
                _fraction = value;
                PropertyChanged.Raise(() => Fraction);
            }
        }
    }

    /// <summary>
    /// 端数の取り扱い
    /// </summary>
    public enum HandleFraction
    {
        /// <summary>
        /// 0. 四捨五入
        /// </summary>
        Round,
        /// <summary>
        /// 1. 五捨六入
        /// </summary>
        RoundExtra,
        /// <summary>
        /// 2. 返し - 得点がプラスなら切り捨て、マイナスなら切り上げ
        /// </summary>
        RoundBase,
        /// <summary>
        /// 3. 切り捨て
        /// </summary>
        Floor,
        /// <summary>
        /// 4. 切り上げ
        /// </summary>
        Ceiling
    }
}
