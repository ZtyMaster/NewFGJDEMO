using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CZBK.ItcastOA.Model.Enum;
using System.Threading.Tasks;
using System.Data;

namespace CZBK.ItcastOA.BLL
{
    public partial class T_FGJHtmlDataService: BaseService<T_FGJHtmlData>,IT_FGJHtmlDataService
    {
        /// <summary>
        /// 多条件搜索用户信息
        /// </summary>
        /// <param name="userInfoSearchParam"></param>
        /// <returns></returns>
        public IQueryable<T_FGJHtmlData> LoadSearchFrist(Model.SearchParam.UserInfoParam userInfoSearchParam,bool b,bool a)
        {
            short Delflag = (short)DelFlagEnum.Normarl;
            var ActionInfoList = this.GetCurrentDbSession.T_SaveHtmlDataDal.LoadEntities(x => x.UserID == userInfoSearchParam.C_id && x.DelFlag == Delflag && x.GongGong == Delflag);
            if (userInfoSearchParam.C_id <=0)
            {
                ActionInfoList = this.GetCurrentDbSession.T_SaveHtmlDataDal.LoadEntities(x => x.DelFlag == Delflag && x.GongGong == Delflag);
            }
                if (!a)
            {

              ActionInfoList = this.GetCurrentDbSession.T_SaveHtmlDataDal.LoadEntities(x => x.UserInfo.MasterID == userInfoSearchParam.C_id && x.DelFlag == Delflag && x.GongGong == Delflag);

            }

            //根据保存ID查询所有属于该人员的数据
            var savedata = ActionInfoList.Select(x => x.T_FGJHtmlData).Where(x=>x.delflag==null);
           
            var selectdata = this.GetCurrentDbSession.T_FGJHtmlDataDal.LoadEntities(c => c.CityID == userInfoSearchParam.C_id&&c.delflag==null);
            
            var temp = b ? selectdata : savedata;
            if (!string.IsNullOrEmpty(userInfoSearchParam.Str))
            {
                temp = temp.Where<T_FGJHtmlData>(u => u.HLName.Contains(userInfoSearchParam.Str)|| u.Address.Contains(userInfoSearchParam.Str)|| u.photo.Contains(userInfoSearchParam.Str));
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Zhuanxiu))
            {
                temp = temp.Where<T_FGJHtmlData>(u => u.FwZhuangxiu.Contains(userInfoSearchParam.Zhuanxiu));
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Pingmu)&& userInfoSearchParam.Pingmu.Trim()!="0")
            {
                int thiid = int.Parse(userInfoSearchParam.Pingmu);

                temp = temp.Where<T_FGJHtmlData>(u => u.MianjiID== thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.money) && userInfoSearchParam.money.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.money);

                temp = temp.Where<T_FGJHtmlData>(u => u.SumMoneyID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Tingshi) && userInfoSearchParam.Tingshi.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.Tingshi);

                temp = temp.Where<T_FGJHtmlData>(u => u.HuXingID == thiid);
            }


            userInfoSearchParam.TotalCount = temp.Count();
            return temp.OrderByDescending<T_FGJHtmlData, DateTime>(u => u.FbTime).Skip<T_FGJHtmlData>((userInfoSearchParam.PageIndex - 1) * userInfoSearchParam.PageSize).Take<T_FGJHtmlData>(userInfoSearchParam.PageSize);

        }

        public IQueryable<T_FGJHtmlData> LoadSearchPM(Model.SearchParam.UserInfoParam uSP)
        {

            var temp = this.GetCurrentDbSession.T_FGJHtmlDataDal.LoadEntities(c => c.CityID == uSP.C_id&&c.delflag==null);
            if (uSP.ssve != null)//小秘书搜索
            {
                #region MyRegion
                if (uSP.ssve.CName != null && uSP.ssve.Quyu != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.HLName.Contains(uSP.ssve.CName) || x.Address.Contains(uSP.ssve.CName) || x.HLName.Contains(uSP.ssve.Quyu) || x.Address.Contains(uSP.ssve.Quyu));
                }
                else if (uSP.ssve.CName != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.HLName.Contains(uSP.ssve.CName) || x.Address.Contains(uSP.ssve.CName));
                }
                else if (uSP.ssve.Quyu != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.HLName.Contains(uSP.ssve.Quyu) || x.Address.Contains(uSP.ssve.Quyu));
                }

                if (uSP.ssve.Srtmm != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.Pingmi_int >= uSP.ssve.Srtmm);
                }
                if (uSP.ssve.Stpmm != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.Pingmi_int <= uSP.ssve.Stpmm);
                }
                if (uSP.ssve.Strmoney != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => (x.Pingmi_int * x.Money_int) >= uSP.ssve.Strmoney);
                }
                if (uSP.ssve.Stpmoney != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => (x.Pingmi_int * x.Money_int) <= uSP.ssve.Stpmoney);
                }
                if (uSP.ssve.HxID != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.HuXingID == uSP.ssve.HxID);
                }

                if (uSP.ssve.Zhuangxiu != null)
                {
                    temp = temp.Where<T_FGJHtmlData>(x => x.FwZhuangxiu == uSP.ssve.Zhuangxiu);
                }
                #endregion                
            }
            else //正常搜索
            {
                if (uSP.quyu != null) {
                    if (uSP.quyu.Trim().Length > 0)
                    {
                        temp = temp.Where<T_FGJHtmlData>(u => u.HLName.Contains(uSP.quyu) || u.Address.Contains(uSP.quyu));
                    }
                }                
                if (!string.IsNullOrEmpty(uSP.Mon1.ToString()) && !string.IsNullOrEmpty(uSP.Mon2.ToString()))
                {
                    temp = temp.Where<T_FGJHtmlData>(u => u.Money_int >= uSP.Mon1 && u.Money_int <= uSP.Mon2);
                }
                if (!string.IsNullOrEmpty(uSP.Pm1.ToString()) && !string.IsNullOrEmpty(uSP.Pm2.ToString()))
                {
                    temp = temp.Where<T_FGJHtmlData>(u => u.Pingmi_int >= uSP.Pm1 && u.Pingmi_int <= uSP.Pm2);
                }
            }

            

            uSP.TotalCount = temp.Count();
            return temp.OrderByDescending<T_FGJHtmlData, DateTime>(u => u.FbTime).Skip<T_FGJHtmlData>((uSP.PageIndex - 1) * uSP.PageSize).Take<T_FGJHtmlData>(uSP.PageSize);
        }
    }

    class SchoolComparer : EqualityComparer<T_FGJHtmlData>
    {
        public override bool Equals(T_FGJHtmlData x, T_FGJHtmlData y)
        {
            return x.photo == y.photo;
        }
        public override int GetHashCode(T_FGJHtmlData obj)
        {
            return obj.photo.GetHashCode();
        }
    }
}
