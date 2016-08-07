using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConvertStringToBase64ToStoreInSqlServer
{
    class Program
    {
        private static string conStr = "Data Source=db.frllk.com;Initial Catalog=Mealtime;Persist Security Info=True;User ID=sa;Password=***";
        /// <summary>
        /// 打开数据库链接方法
        /// </summary>
        /// <param name="constr"></param>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection(string constr)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            return conn;
        }
        static void Main(string[] args)
        {
            //1. 拼接sql
            var builder=new StringBuilder();
            using (var file=new StreamReader("smsBanned1.txt", Encoding.UTF8,true))
            {
                string line;
                while ((line = file.ReadLine()) !=null)
                {
                    byte[] bytes = Encoding.Default.GetBytes(line);
                    var afterStr = Convert.ToBase64String(bytes);
                    builder.Append($"INSERT INTO IllegalWord ( Name, Type ) VALUES  ( '{afterStr}', '广告'  )\r\n");
                }
            }
            //2. 记录生成的sql
            File.AppendAllText("2.txt",builder.ToString(),Encoding.UTF8);
            //3. sql执行
            using (var conn=GetSqlConnection(conStr))
            {
               var effect= conn.Execute(builder.ToString());
                Console.WriteLine($"导入：{effect}条敏感词");
            }
            Console.ReadKey();
        }
    }
}
