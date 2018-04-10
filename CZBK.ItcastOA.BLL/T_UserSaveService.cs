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
    public partial class T_UserSaveService : BaseService<T_UserSave>,IT_UserSaveService
    {
        /// <summary>
        /// 多条件搜索用户信息
        /// </summary>
        /// <param name="userInfoSearchParam"></param>
        /// <returns></returns>
        public IQueryable<T_UserSave> LoadSearchEntities(Model.SearchParam.UserInfoParam userInfoSearchParam)
        {
            short Delflag = (short)DelFlagEnum.Normarl;
            
            var temp = this.GetCurrentDbSession.T_UserSaveDal.LoadEntities(x => x.UserID == userInfoSearchParam.C_id && x.delflag == Delflag);
            if (!userInfoSearchParam.IsMaster)
            {
                temp=this.GetCurrentDbSession.T_UserSaveDal.LoadEntities(x=>x.UserInfo.MasterID== userInfoSearchParam.C_id && x.delflag == Delflag);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Items))
            {
                int thiid = int.Parse(userInfoSearchParam.Items);
                temp = temp.Where<T_UserSave>(u => u.Items== thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Str))
            {
                temp = temp.Where<T_UserSave>(u => u.TextName.Contains(userInfoSearchParam.Str) || u.Address.Contains(userInfoSearchParam.Str)||u.PresonPhoto.Contains(userInfoSearchParam.Str));
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Zhuanxiu))
            {
                int thiid = int.Parse(userInfoSearchParam.Zhuanxiu);
                temp = temp.Where<T_UserSave>(u => u.ZhuanxiuStrID== thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Pingmu) && userInfoSearchParam.Pingmu.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.Pingmu);

                temp = temp.Where<T_UserSave>(u => u.MianjiID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.money) && userInfoSearchParam.money.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.money);

                temp = temp.Where<T_UserSave>(u => u.SumMoneyID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.Tingshi) && userInfoSearchParam.Tingshi.Trim() != "0")
            {
                int thiid = int.Parse(userInfoSearchParam.Tingshi);

                temp = temp.Where<T_UserSave>(u => u.HuxingStrID == thiid);
            }
            userInfoSearchParam.TotalCount = temp.Count();
            return temp.OrderByDescending<T_UserSave, DateTime>(u => u.AddTime).Skip<T_UserSave>((userInfoSearchParam.PageIndex - 1) * userInfoSearchParam.PageSize).Take<T_UserSave>(userInfoSearchParam.PageSize);

        }

    }
}
