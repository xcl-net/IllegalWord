using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mgc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入带敏感词的句子：");
            var str = Console.ReadLine();
            Console.WriteLine("请输入脏字替换符号：");
            var holer = Console.ReadLine();
            if(holer==null) return;
            var placeholder =Convert.ToChar(holer);
            var cons = CheckDirtyWord(str, placeholder);
            Console.WriteLine("过滤后的词：");
            Console.WriteLine(cons);

            Console.ReadKey();

        }

        /// <summary>
        /// 检查脏字
        /// </summary>
        /// <param name="message">字符创</param>
        /// <param name="placeholder">替换符</param>
        /// <returns></returns>
        static string CheckDirtyWord(string message,char placeholder)
        {
            Dictionary<char, List<string>> dicList = new Dictionary<char, List<string>>();

            //替换后的文本输出内容
            StringBuilder sb = new StringBuilder(message.Length);

            //读取脏字源文件，存到字典中
            using (StreamReader file = new StreamReader("bannerWoed1.txt", Encoding.UTF8, true))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    char value = line[0];
                    if (dicList.ContainsKey(value))
                        dicList[value].Add(line);
                    else
                        dicList.Add(value, new List<string>() { line });
                }
            }
            //从到一个字符串数组中读取脏字；
            //string filterText = "需要过滤的脏字 以|分开";
            ////脏字 可根据自己的方式用分隔符
            //string[] filterData = filterText.Split('|');
            //foreach (var item in filterData)
            //{
            //    char value = item[0];
            //    if (dicList.ContainsKey(value))
            //        dicList[value].Add(item);
            //    else
            //        dicList.Add(value, new List<string>() {item});
            //}

            //检查输入的文本是否包含脏字
            int count = message.Length;
            for (int i = 0; i < count; i++)
            {
                char word = message[i];
                //如果在字典表中存在这个key
                if (dicList.ContainsKey(word))
                {
                    //是否找到匹配的关键字 1找到 0未找到
                    int num = 0;
                    //把该key的字典集合按 字符数排序(方便下面从少往多截取字符串查找)
                    var data = dicList[word].OrderBy(g => g.Length);
                    foreach (var wordbook in data)
                    {
                        //如果需截取的字符串的索引小于总长度 则执行截取
                        if (i + wordbook.Length <= count)
                        {
                            //根据关键字长度往后截取相同的字符数进行比较
                            string result = message.Substring(i, wordbook.Length);
                            if (result == wordbook)
                            {
                                num = 1;
                                sb.Append(GetString(result, placeholder));
                                //比较成功 同时改变i的索引
                                i = i + wordbook.Length - 1;
                                break;
                            }
                        }
                    }
                    if (num == 0)
                        sb.Append(word);
                }
                else
                    sb.Append(word);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 替换星号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        private static string GetString(string value,char placeholder)
        {
            string starNum = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                starNum += placeholder;
            }
            return starNum;
        }
    }
}
