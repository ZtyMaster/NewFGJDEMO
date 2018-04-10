using System;
using CZBK.ItcastOA.Model;
using System.Collections.Generic;
using System.Linq;
using CZBK.ItcastOA.Model.SearchParam;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IBLL
{
    public partial interface IUserInfo_CityService : IBaseService<UserInfo_City>
    {
        List<T_Quyu> LoadMyCity_Quyu(int ID);
    }
    
}
