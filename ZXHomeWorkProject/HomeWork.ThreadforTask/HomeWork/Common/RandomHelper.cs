using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Course_6_HomeWork.ThreadforTask.HomeWork.Common
{
    public class RandomHelper
    {
        /// <summary>
        /// 随机获取数字并等待1~2s
        /// </summary>
        /// <returns></returns>
        public int GetRandomNumberDelay(int min, int max)
        {
            Thread.Sleep(this.GetRandomNumber(500, 1000));//随机休息一下
            return this.GetRandomNumber(min, max);
        }

        /// <summary>
        /// 获取随机数，解决重复问题
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandomNumber(int min, int max)
        {
            Guid guid = Guid.NewGuid();//每次都是全新的ID  全球唯一Id
            string sGuid = guid.ToString();
            int seed = DateTime.Now.Millisecond;
            //保证seed 在同一时刻 是不相同的
            for (int i = 0; i < sGuid.Length; i++)
            {
                switch (sGuid[i])
                {
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                        seed = seed + 1;
                        break;
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                        seed = seed + 2;
                        break;
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                        seed = seed + 3;
                        break;
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        seed = seed + 3;
                        break;
                    default:
                        seed = seed + 4;
                        break;
                }
            }
            Random random = new Random(seed);
            return random.Next(min, max);
        }
    }
}
