using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);
        IQueryable<UserInfo> LoadSearchEntities(UserInfoParam userInfoSearchParam);
        bool setuserorderidnfo(int userid, List<int> list);
        bool SetUserActionInfo(int userId, int actionId, bool ispass);
        bool DelUserinfo_actioninfo(int userID, int actionID);
        bool EditUserinfoMIN(UserInfo userinfo);
        //获取该用户点当前点击量
        int GetMaxClick(int? id);
       



    }
}
