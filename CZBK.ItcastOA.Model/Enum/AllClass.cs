using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.Model.Enum
{
    public  class AllClass
    {
        #region 面积转换ID号方法
        public static int GeiMinji(string da)
        {
            
            string str = da.Replace("㎡", string.Empty);
            str = str.Replace("以下", string.Empty);
            str = str.Replace("以上", string.Empty);

            double mj = Convert.ToDouble(str);
            if (mj < 50)
            {
                return 1;
            }
            else if (mj >= 50 && mj < 70)
            {
                return 2;
            }
            else if (mj >= 70 && mj < 90)
            {
                return 3;
            }
            else if (mj >= 90 && mj < 110)
            {
                return 4;
            }
            else if (mj >= 110 && mj < 130)
            {
                return 5;
            }
            else if (mj >= 130 && mj < 150)
            {
                return 6;
            }
            else if (mj >= 150 && mj < 200)
            {
                return 7;
            }
            else if (mj >= 200 && mj < 300)
            {
                return 8;
            }
            else if (mj >= 300 && mj < 500)
            {
                return 9;
            }
            else
            {
                return 10;
            }
        }
        #endregion
        #region 金额转换ID号方法
        public static int GetMoney(string str)
        {
            str = str.Replace("万", string.Empty);
            str = str.Replace("以下", string.Empty);
            str = str.Replace("以上", string.Empty);
            double mj = Convert.ToDouble(str);
            if (mj < 30)
            {
                return 1;
            }
            else if (mj >= 30 && mj < 40)
            {
                return 2;
            }
            else if (mj >= 40 && mj < 50)
            {
                return 3;
            }
            else if (mj >= 50 && mj < 60)
            {
                return 4;
            }
            else if (mj >= 60 && mj < 80)
            {
                return 5;
            }
            else if (mj >= 80 && mj < 100)
            {
                return 6;
            }
            else if (mj >= 100 && mj < 120)
            {
                return 7;
            }
            else if (mj >= 120 && mj < 160)
            {
                return 8;
            }
            else if (mj >= 160 && mj < 200)
            {
                return 9;
            }
            else
            {
                return 10;
            }
        }
        #endregion
        #region 户型转换ID号方法
        public static int GetHuxing(string da)
        {
            if (da.IndexOf("1室") >= 0)
            {
                return 1;
            }
            else if (da.IndexOf("2室") >= 0)
            { return 2; }
            else if (da.IndexOf("3室") >= 0)
            { return 3; }
            else if (da.IndexOf("4室") >= 0)
            { return 4; }
            else
            {
                return 5;
            }
        }
        #endregion

    }
}
