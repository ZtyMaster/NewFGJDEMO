﻿using System;
using CZBK.ItcastOA.Model;
using System.Collections.Generic;
using System.Linq;
using CZBK.ItcastOA.Model.SearchParam;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IBLL
{
    public partial interface IT_FGJHtmlDataService :IBaseService<T_FGJHtmlData>
    {
       
        IQueryable<T_FGJHtmlData> LoadSearchFrist(UserInfoParam userInfoSearchParam,bool b,bool a);
        IQueryable<T_FGJHtmlData> LoadSearchPM(UserInfoParam userInfoParam);
    }
}
