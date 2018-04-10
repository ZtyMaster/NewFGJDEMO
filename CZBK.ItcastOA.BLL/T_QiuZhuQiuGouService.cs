using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CZBK.ItcastOA.Model.Enum;
using System.Threading.Tasks;
using CZBK.ItcastOA.Model.SearchParam;

namespace CZBK.ItcastOA.BLL
{
    public partial class T_QiuZhuQiuGouService : BaseService<T_QiuZhuQiuGou>, IT_QiuZhuQiuGouService
    {
        public IQueryable<T_QiuZhuQiuGou> LoadSearchEntities(UserInfoParam uip)
        {
            var temp = this.GetCurrentDbSession.T_QiuZhuQiuGouDal.LoadEntities(x => x.CityID==uip.CityID);
            if (!string.IsNullOrEmpty(uip.Str))
            {
                temp = temp.Where<T_QiuZhuQiuGou>(u => u.GuiShuDi.Contains(uip.Str)|| u.Hname.Contains(uip.Str) || u.QuYu.Contains(uip.Str));                
            }
            if (uip.Isee)
            {
                var czdata = this.GetCurrentDbSession.SeeQzCzDal.LoadEntities(x => x.UserID == uip.C_id && x.QiuZhuID != null);
                temp = czdata.Select(x => x.T_QiuZhuQiuGou);
            }
            uip.TotalCount = temp.Count();
            return temp.OrderByDescending<T_QiuZhuQiuGou, DateTime>(u => u.FBtime).Skip<T_QiuZhuQiuGou>((uip.PageIndex - 1) * uip.PageSize).Take<T_QiuZhuQiuGou>(uip.PageSize);
        }
    }
      
}
