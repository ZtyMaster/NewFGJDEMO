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
    public partial class UserInfo_CityService : BaseService<UserInfo_City>, IUserInfo_CityService
    {
        public List<T_Quyu> LoadMyCity_Quyu(int ID) {
            
          
            var CityID = this.GetCurrentDbSession.UserInfo_CityDal.LoadEntities(x => x.UserInfo_ID == ID).FirstOrDefault();
            if (CityID == null)
            {
                List<T_Quyu> ltu = new List<T_Quyu>();
                return ltu;
            }
            var City = this.GetCurrentDbSession.T_CityDal.LoadEntities(x => x.ID == CityID.T_City_ID).FirstOrDefault().T_Quyu.ToList();
            return City;

        }
    }     
}
