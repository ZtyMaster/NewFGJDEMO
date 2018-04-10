using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CZBK.ItcastOA.Model.Enum;
using System.Threading.Tasks;


namespace CZBK.ItcastOA.BLL
{
   

    public partial class T_SaveHtmlDataService : BaseService<T_SaveHtmlData>, IT_SaveHtmlDataService
    {
        /// <summary>
        /// 多条件搜索用户信息
        /// </summary>
        /// <param name="userInfoSearchParam"></param>
        /// <returns></returns>
        public IQueryable<T_SaveHtmlData> LoadSearchEntities(Model.SearchParam.UserInfoParam userInfoSearchParam,bool t)
        {
            short GgEnum = t? (short)GongGEnum.heidden : (short)GongGEnum.Show;
            short Delflag = (short)DelFlagEnum.Normarl;

            var temp = this.GetCurrentDbSession.T_SaveHtmlDataDal.LoadEntities(x => ( x.DelFlag == Delflag && x.GongGong == GgEnum&&x.UserInfo.MasterID==userInfoSearchParam.MasterID)|| (x.DelFlag == Delflag && x.GongGong == GgEnum && x.UserInfo.ID==userInfoSearchParam.MasterID));

            //var t_list = from a in temp
            //             select a.T_FGJHtmlData;

            //根据保存ID查询所有属于该人员的数据
            //var savedata = ActionInfoList.Select(x => x.T_FGJHtmlData);
            //var selectdata = this.GetCurrentDbSession.T_FGJHtmlDataDal.LoadEntities(c => c.CityID == userInfoSearchParam.C_id);
            //var temp = selectdata ;

            if (!string.IsNullOrEmpty(userInfoSearchParam.Str))
            {
                temp = temp.Where<T_SaveHtmlData>(u => u.T_FGJHtmlData.HLName.Contains(userInfoSearchParam.Str) || u.T_FGJHtmlData.Address.Contains(userInfoSearchParam.Str));
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Zhuanxiu))
            {
                temp = temp.Where<T_SaveHtmlData>(u => u.T_FGJHtmlData.FwZhuangxiu.Contains(userInfoSearchParam.Zhuanxiu));
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Pingmu) && userInfoSearchParam.Pingmu.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.Pingmu);

                temp = temp.Where<T_SaveHtmlData>(u => u.T_FGJHtmlData.MianjiID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.money) && userInfoSearchParam.money.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.money);

                temp = temp.Where<T_SaveHtmlData>(u => u.T_FGJHtmlData.SumMoneyID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Tingshi) && userInfoSearchParam.Tingshi.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.Tingshi);

                temp = temp.Where<T_SaveHtmlData>(u => u.T_FGJHtmlData.HuXingID == thiid);
            }
            userInfoSearchParam.TotalCount = temp.Count();
            return temp.OrderByDescending<T_SaveHtmlData, DateTime>(u => u.T_FGJHtmlData.FbTime).Skip<T_SaveHtmlData>((userInfoSearchParam.PageIndex - 1) * userInfoSearchParam.PageSize).Take<T_SaveHtmlData>(userInfoSearchParam.PageSize);

        }

    }
}
