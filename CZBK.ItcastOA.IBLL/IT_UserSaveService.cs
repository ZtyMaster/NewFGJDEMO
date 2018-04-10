using System;
using CZBK.ItcastOA.Model;
using System.Collections.Generic;
using System.Linq;
using CZBK.ItcastOA.Model.SearchParam;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IBLL
{
    public partial interface IT_UserSaveService : IBaseService<T_UserSave>
    {
        IQueryable<T_UserSave> LoadSearchEntities(UserInfoParam userInfoSearchParam);
    }
}
