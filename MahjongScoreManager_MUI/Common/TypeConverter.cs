using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace MahjongScoreManager_MUI.Common
{
    public class TypeConverter
    {
        /// <summary>
        /// String型をInt型に変換する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>Int型（変換に失敗したら0）</returns>
        public static int StringToInt(string str)
        {
            int ret = 0;

            try
            {
                ret = int.Parse(str);
            }
            catch
            {
                // WriteLog
            }

            return ret;
        }

        /// <summary>
        /// String型をDouble型に変換する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>Double型（失敗したら0）</returns>
        public static double StringToDouble(string str)
        {
            double ret = 0.0;

            try
            {
                ret = double.Parse(str);
            }
            catch
            {
                // WriteLog
            }

            return ret;
        }

        /// <summary>
        /// 任意のオブジェクトをString型に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>文字列（失敗したら空文字列）</returns>
        public static string ObjectToString(object obj)
        {
            string ret = "";

            try
            {
                ret = obj.ToString();
            }
            catch
            {
                // WriteLog
            }

            return ret;
        }

        /// <summary>
        /// null許容型をnull非許容型に変換する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="Target">オブジェクト</param>
        /// <returns>null非許容型（失敗したら型の初期値）</returns>
        public static T NullableToUnNullable<T>(Nullable<T> Target) where T : struct
        {
            T ret = default(T);

            try
            {
                ret = (T)Target;
            }
            catch
            {
                // WriteLog
            }

            return ret;
        }

        /// <summary>
        /// ListをDataTableに変換する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="List">リスト</param>
        /// <returns>変換後オブジェクト</returns>
        public static DataTable ListToDataTable<T>(T List) where T : IList
        {
            DataTable ret = null;

            try
            {
                ret = new DataTable(typeof(T).GetGenericArguments()[0].Name);
                typeof(T).GetGenericArguments()[0].GetProperties().ToList().ForEach(p => ret.Columns.Add(p.Name, p.PropertyType));
                foreach (var item in List)
                {
                    var row = ret.NewRow();
                    item.GetType().GetProperties().ToList().ForEach(p => row[p.Name] = p.GetValue(item, null));
                    ret.Rows.Add(row);
                }
            }
            catch
            {
                // WriteLog
            }

            return ret;
        }

        /// <summary>
        /// DataTableをListに変換する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="Table">テーブル</param>
        /// <returns>変換後オブジェクト</returns>
        public static T DataTableToLst<T>(DataTable Table) where T : IList, new()
        {
            var ret = new T();

            try
            {
                foreach (DataRow row in Table.Rows)
                {
                    var item = Activator.CreateInstance(typeof(T).GetGenericArguments()[0]);
                    ret.GetType().GetGenericArguments()[0].GetProperties().ToList().ForEach(p => p.SetValue(item, row[p.Name], null));
                    ret.Add(item);
                }
            }
            catch
            {
                // WriteLog
            }

            return ret;
        }
    }
}
