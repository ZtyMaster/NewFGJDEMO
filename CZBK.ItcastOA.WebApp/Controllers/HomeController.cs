using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoService UserInfoService { get; set; }
      
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }
        IBLL.IT_SaveHtmlDataService T_SaveHtmlDataService { get; set; }
        IBLL.IGongGaoService GongGaoService { get; set; }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.UName;
            }
            var temp = GongGaoService.LoadEntities(x => x.ID > 0).DefaultIfEmpty();
            ViewData["TopGongG"] = Self(temp, 0);
            ViewData["QQ"] = Self(temp, 4);
            ViewData["banquan"] = Self(temp, 6);
            ViewData["banquan"] = Self(temp, 6);
            ViewData["LogoName"] = Self(temp, 5);
            ViewBag.Uid = LoginUser.ID;
            @ViewBag.Uip = HttpContext.Request.UserHostAddress;
            return View();
        }
        public string Self(IQueryable<GongGao> temp, int t)
        {
            return temp.Where(x => x.Items == t).FirstOrDefault() == null ? "" : temp.Where(x => x.Items == t).FirstOrDefault().text;
        }
        /// <summary>
        /// 传统布局模式
        /// </summary>
        /// <returns></returns>
        public ActionResult HomePage()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.UName;
            }
            return View();
        }

        #region 校验用户权限 完成登陆用户菜单权限的过滤
        public ActionResult GetMenus() {
            //1：根据用户 ——角色——权限 将登陆用户具有的菜单权限查询出来放在一个集合中
           var loginUserInfo= UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
           
           var loginUserRoleInfo = loginUserInfo.RoleInfo;//获取登陆用户的角色信息
            short actionTypeEnum = (short)ActionInfoTypeEnum.MenuActionTypeEnum;//表示菜单权限
            //查询出角色对应的菜单权限
            var loginUserActionInfo = (from r in loginUserRoleInfo
                                       from a in r.ActionInfo
                                       where a.ActionTypeEnum == actionTypeEnum
                                       select a).ToList();
            //2：根据用户——权限

            //根据登陆用户查询o.R_UserInfo_ActionInfo中间表，然后在用导航属性查询权限表
            var r_userInfo_actionInfo = from r in loginUserInfo.R_UserInfo_ActionInfo select r.ActionInfo;

            //判断是否是菜单权限
            var loginUserMenuAction = (from r in r_userInfo_actionInfo
                                       where r.ActionTypeEnum == actionTypeEnum
                                       select r).ToList();
            //将存储登陆用户权限的两个集合合并
            loginUserActionInfo.AddRange(loginUserMenuAction);
            //查询出所有登陆用户禁止的权限的编号
            var loginForbActionInfo =(from r in loginUserInfo.R_UserInfo_ActionInfo
                                      where r.IsPass == false
                                      select r.ActionInfoID).ToList();
            //将禁止的权限从集合中过滤掉
            var loginUserAllowActionlist = loginUserActionInfo.Where(a => !loginForbActionInfo.Contains(a.ID));
            //去除重复的
            var loginUserAllowActionlists= loginUserAllowActionlist.Distinct(new EqualityComparer());
            var returnActionlist = from a in loginUserAllowActionlists
                                   select new { icon = a.MenuIcon, title = a.ActionInfoName, url = a.Url };
            //序列化 权限
            return Json(returnActionlist,JsonRequestBehavior.AllowGet);

        }
        #endregion

        ///获取标记到期时间与条数
        ///
        public ActionResult GetBiaoJiDaoQiTime()
        {
            int Gint = (int)T_BoolItemService.LoadEntities(x => x.ID == 2).FirstOrDefault().@int;

            DateTime NowTime = MvcApplication.GetT_time();
            //int masterID =(Int32)( Convert.ToBoolean(LoginUser.ThisMastr) ? LoginUser.ID : LoginUser.MasterID);
            var tsave = T_SaveHtmlDataService.LoadEntities(x => x.UserID == LoginUser.ID&&x.BiaoJiId==null&&x.GongGong==0).DefaultIfEmpty();

            return Json(new { rows = tsave.ToList()[0] != null?tsave.Count():0,mess="OK" }, JsonRequestBehavior.AllowGet);
            
        }
    }
}
