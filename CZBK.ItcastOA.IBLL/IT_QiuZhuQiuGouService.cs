using System;
using CZBK.ItcastOA.Model;
using System.Collections.Generic;
using System.Linq;
using CZBK.ItcastOA.Model.SearchParam;
using System.Text;
using System.Threading.Tasks;


namespace CZBK.ItcastOA.IBLL
{
    public partial interface IT_QiuZhuQiuGouService : IBaseService<T_QiuZhuQiuGou>
    {
        
        IQueryable<T_QiuZhuQiuGou> LoadSearchEntities(UserInfoParam uip);
    }
}
