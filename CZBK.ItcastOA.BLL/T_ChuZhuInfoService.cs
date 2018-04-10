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
    public partial class T_ChuZhuInfoService : BaseService<T_ChuZhuInfo>, IT_ChuZhuInfoService
    {
        public IQueryable<T_ChuZhuInfo> LoadSearchEntities(UserInfoParam uip)
        {
            var temp = this.GetCurrentDbSession.T_ChuZhuInfoDal.LoadEntities(x => x.CityID == uip.CityID);
            if (!string.IsNullOrEmpty(uip.Str))
            {
                temp = temp.Where<T_ChuZhuInfo>(u => u.Addess.Contains(uip.Str)|| u.ChuZhuName.Contains(uip.Str) || u.Bak.Contains(uip.Str));
            }
            if (uip.Isee) {
                var czdata = this.GetCurrentDbSession.SeeQzCzDal.LoadEntities(x => x.UserID == uip.C_id&&x.ChuZhuID!=null);
                temp = czdata.Select(x => x.T_ChuZhuInfo);
            }

            uip.TotalCount = temp.Count();
            return temp.OrderByDescending<T_ChuZhuInfo, DateTime?>(u => u.FbTime).Skip<T_ChuZhuInfo>((uip.PageIndex - 1) * uip.PageSize).Take<T_ChuZhuInfo>(uip.PageSize);
        }
    }
}
