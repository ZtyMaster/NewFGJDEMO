using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class UserInfoController :BaseController //Controller
    {
        //
        // GET: /UserInfo/
        IBLL.IUserInfoService UserInfoService{get;set;}
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        IBLL.IActionInfoService ActionInfoService { get; set; }
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }
        IBLL.IT_YxPersonService T_YxPersonService { get; set; }
        IBLL.ITLoginbakService TLoginbakService { get; set; }
        IBLL.IWxUserService WxUserService { get; set; }
        IBLL.IGongGaoService GongGaoService { get; set; }
        short Delflag = (short)DelFlagEnum.Normarl;
        public ActionResult Index()
        {
            var City = T_CityService.LoadEntities(x=>x.DelFlag== Delflag).DefaultIfEmpty();
            ViewBag.City = City;
            ViewBag.json = Json(new { rows = City }, JsonRequestBehavior.AllowGet);
            ViewBag.YYY = T_YxPersonService.LoadEntities(x=>x.DEL==0).DefaultIfEmpty();
            return View();

        }
        
        public ActionResult GetJson()
        {
            var City = T_CityService.LoadEntities(x => x.DelFlag == Delflag).DefaultIfEmpty();
            var temp = from u in City
                       select new
                       {
                           ID = u.ID,
                           city = u.City                                                  
                       };
            return Json(new { rows = temp }, JsonRequestBehavior.AllowGet);
        }
        #region 企业用户账号管理
        public ActionResult Zhgl()
        {
           return View();
        }
        #region 创建小号
        public ActionResult Addzhgl(UserInfo Uinfo)
        {
            string rt=string.Empty;
            //检查用户是否重复
            if (SelectUserName(Uinfo))
            {
                rt = "IsCongfu";
                return Content("IsCongfu");
            }
            //检查创建用户是否到达上线
            var Ucount = UserInfoService.LoadEntities(x => x.MasterID == LoginUser.ID).DefaultIfEmpty();
            if (Ucount.Count() >= LoginUser.UserXiaoHao)
            {
                if (Ucount.Count() >= LoginUser.UserXiaoHao)
                {
                    rt = "UserUP";
                    return Content("UserUP");
                }
            }
            else
            {
                Uinfo.MasterID = LoginUser.ID;
                Uinfo.ThisMastr = false;
                Uinfo.UPwd = Model.Enum.AddMD5.GaddMD5(Uinfo.UPwd);
                Uinfo.Click = LoginUser.Click;
                Uinfo.OverTime = LoginUser.OverTime;
                Uinfo.SubTime = MvcApplication.GetT_time();
                Uinfo.ModifiedOn = Uinfo.SubTime;
                Uinfo.CityID = LoginUser.CityID;
               
                UserInfoService.AddEntity(Uinfo);
                var Tuserinfo = UserInfoService.LoadEntities(x => x.UName == Uinfo.UName).FirstOrDefault();
                ////父级ID
                //UserInfo userInfo = UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
                //var userRoleIdList = (from r in userInfo.RoleInfo
                //                      select r.ID).ToList();
                //获取区域归属 基础区域
                var Tloginuser = UserInfoService.LoadEntities(x => x.ID == LoginUser.ID).FirstOrDefault();
                UserInfo_City ct = Tloginuser.UserInfo_City.FirstOrDefault();
                ct.UserInfo_ID = Tuserinfo.ID;
                UserInfo_CityService.AddEntity(ct);
                //获取小号权限 小号权限是10
                List<int> list = new List<int>();
                list.Add(10);
                if (UserInfoService.setuserorderidnfo(Tuserinfo.ID, list))
                {
                    rt = "UserUP"; return Content("ok"); }
                else
                { rt = "UserUP"; return Content("NO"); }
            }
            return Content(rt);
          
        }

        private bool SelectUserName(UserInfo Uinfo)
        {
            return UserInfoService.LoadEntities(x => x.UName == Uinfo.UName).FirstOrDefault() != null;
        }
        #endregion

        #region 修改小号
        public ActionResult Editzhgl(UserInfo userInfo)
        {
            var tu = UserInfoService.LoadEntities(x => x.ID == userInfo.ID).FirstOrDefault();
            tu.UName = userInfo.UName;           
            tu.ModifiedOn = DateTime.Now;
            tu.UPwd = Model.Enum.AddMD5.GaddMD5(userInfo.UPwd);
            tu.Sort = userInfo.Sort;
            tu.Remark = userInfo.Remark;
            if (UserInfoService.EditEntity(tu))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #endregion

        #region 获取用户数据
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            string name = Request["name"];
            string remark = Request["remark"];
            string zhgl = Request["zhgl"] != null ? Request["zhgl"] : string.Empty;
            bool bol = Convert.ToBoolean(Request["Mxitems"]);
            //构建搜索条件
            int totalCount=0;
            UserInfoParam userInfoParam = new UserInfoParam() {
                IsMaster = bol,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                UserName = name,
                Remark = remark,
                C_id = bol?0: LoginUser.ID 
            };                     
            var userInfoList = UserInfoService.LoadSearchEntities(userInfoParam);
           var temp = from u in userInfoList
                      select new { ID = u.ID, UserName = u.UName, UserPass = u.UPwd, Remark = u.Remark, RegTime = u.SubTime, OverTime=u.OverTime,
                          UserXiaoHao=u.UserXiaoHao,
                          Click=u.Click,
                          ThisMastr=u.ThisMastr,
                          MasterID=u.MasterID,
                          CityID=u.CityID,
                          Umoney=u.Umoney,
                          GouMayPerson=u.khName,
                          GouMayPhoto=u.KhPhoto,
                          Yxy=u.T_YxPerson1.PersonName
                      };
           return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除用户数据
        public ActionResult DeleteUserInfo()
        {
            string strId=Request["strId"];
           string[]strIds=strId.Split(',');
           List<int> list = new List<int>();
           foreach (string id in strIds)
           {
               list.Add(int.Parse(id));
           }
           if (UserInfoService.DeleteEntities(list))
           {
               return Content("ok");
           }
           else
           {
               return Content("no");
           }
              
        }
        #endregion

        #region 添加用户信息
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            //检查用户是否重复
            if (SelectUserName(userInfo))
            {
                return Content("IsCongfu");
            }
            userInfo.DelFlag = 0;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            userInfo.UPwd = Model.Enum.AddMD5.GaddMD5(userInfo.UPwd);
            userInfo.ThisMastr = true;
            UserInfoService.AddEntity(userInfo);
            var ucinfo = UserInfoService.LoadEntities(x => x.UName == userInfo.UName).FirstOrDefault();
            ucinfo.MasterID = ucinfo.ID;
            UserInfoService.EditEntity(ucinfo);
            UserInfo_City uc = new UserInfo_City();
            uc.UserInfo_ID = ucinfo.ID;
            uc.T_City_ID = (Int32)userInfo.CityID;
            UserInfo_CityService.AddEntity(uc);
            return Content("ok");
        }
        #endregion

        #region 查询要修改的数据
        public ActionResult GetUserInfoModel()
        {
            int id = int.Parse(Request["id"]);
           UserInfo userInfo=UserInfoService.LoadEntities(u=>u.ID==id).FirstOrDefault();
            
           if (userInfo != null)
           {
              // return Json(new{serverData=userInfo,msg="ok"}, JsonRequestBehavior.AllowGet);
               return Content(Common.SerializerHelper.SerializeToString(new { serverData = userInfo, msg = "ok" }));
           }
           else
           {
               return Content(Common.SerializerHelper.SerializeToString(new { msg = "no" }));
           }
        }
        #endregion

        #region  修改密码
        public ActionResult EditPwd(UserInfo userInfo)
        {
            var tu = UserInfoService.LoadEntities(x => x.ID == userInfo.ID).FirstOrDefault();
            tu.UPwd= Model.Enum.AddMD5.GaddMD5(userInfo.UPwd);
            if (UserInfoService.EditEntity(tu))
            { return Content("okedit"); }
            else
            { return Content("no"); }
            
        }
        #endregion

            #region 完成用户信息的修改
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
           
            var userinfos = UserInfoService.LoadEntities(x => x.ID == userInfo.ID).FirstOrDefault();

            userinfos.ModifiedOn = DateTime.Now;
            userinfos.UPwd= userinfos.UPwd;
            userinfos.Remark = userInfo.Remark;
            userinfos.Sort = userInfo.Sort;
            userinfos.OverTime = userInfo.OverTime;
            userinfos.UserXiaoHao = userInfo.UserXiaoHao;
            userinfos.Click = userInfo.Click;
            userinfos.CityID = userInfo.CityID;


            //修改主号
            if (UserInfoService.EditUserinfoMIN(userinfos))
            {
                return Content("ok");
            }
            else
            {
                return Content("on");
            }
        }
        
        #endregion

        #region 为用户分配角色
        public ActionResult SetUserRoleInfo()
        {
            int userId = int.Parse(Request["userId"]);
           UserInfo userInfo=UserInfoService.LoadEntities(u=>u.ID==userId).FirstOrDefault();
           ViewBag.UserInfo = userInfo;
            //查询所有的角色信息
           short delFlag = (short)DelFlagEnum.Normarl;
           var roleInfoList = RoleInfoService.LoadEntities(r=>r.DelFlag==delFlag).ToList();
            //找出用户已经有的角色的编号
           var userRoleIdList = (from r in userInfo.RoleInfo
                                 select r.ID).ToList();
           ViewBag.AllRoleInfo = roleInfoList;
           ViewBag.AllExtRoleId = userRoleIdList;
           return View();
        }
        #endregion
        /// <summary>
        /// 完成对用户角色的分配
        /// </summary>
        /// <returns></returns>
        public ActionResult SetuserRole()
        {
            int userid = int.Parse(Request["userId"]);
            //接受表单中所用的KEY  所用表单NAME属性的值
            //Request.Form[]接受NAME的值

            string[] allkeys= Request.Form.AllKeys;
            List<int> list = new List<int>();
            //只要前缀只包含CBA_
            foreach (string key in allkeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string K = key.Replace("cba_","");
                    list.Add(int.Parse(K));
                }
            }
            if (UserInfoService.setuserorderidnfo(userid, list))
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }
        #region 为特殊用户分配权限

        public ActionResult SetUserAction() {
            int userid = int.Parse(Request["UserId"]);
            //查询要分配权限的用户信息
            var userinfo = UserInfoService.LoadEntities(u => u.ID == userid).FirstOrDefault();
            //获取所有权限信息
            short delflag = (short)DelFlagEnum.Normarl;
            var allActionList = ActionInfoService.LoadEntities(a => a.DelFlag == delflag).ToList();
            //获取用户已有权限
            var allUserActionIDlist = userinfo.R_UserInfo_ActionInfo.ToList();

            ViewBag.userinfo = userinfo;
            ViewBag.allActionList = allActionList;
            ViewBag.allUserActionIDlist = allUserActionIDlist;
            return View();
        }
        #endregion
        #region //异步处理特殊权限信息
        public ActionResult SetActionUser()
        {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
            bool ispass = Request["value"] == "true" ? true:false;
            if (UserInfoService.SetUserActionInfo(userId, actionId, ispass))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #region 清除用户特殊权限信息
        public ActionResult deleteActionUser()
        {
            int userID = int.Parse(Request["userId"]);
            int actionID = int.Parse(Request["action"]);
            if (UserInfoService.DelUserinfo_actioninfo(userID, actionID))
            {
                return Content("ok:"+ actionID);
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        //-------------------------业务员管理分割线-------------------------
        #region 获取营销员列表
        public ActionResult GetListYXY()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount = 0;           
            var userInfoList = T_YxPersonService.LoadPageEntities(pageIndex, pageSize, out totalCount, x => x.DEL == 0, x => x.ID, true);
            var temp = from u in userInfoList
                       select new
                       {
                           ID = u.ID,
                           PersonName = u.PersonName,
                           Photo = u.Photo,
                           Addess = u.Addess,
                           AddTime = u.AddTime,
                           Gender = u.Gender,
                           age = u.age,
                           RuzTime = u.RuzTime
                       };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 获取历史登陆信息
        public ActionResult GetLoginUserIp() {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount = 0;
            var userInfoList = TLoginbakService.LoadPageEntities(pageIndex, pageSize, out totalCount, x => x.del==null, x => x.intime, false);
           // var userInfoList = T_YxPersonService.LoadPageEntities(pageIndex, pageSize, out totalCount, x => x.DEL == 0, x => x.ID, true);
            var temp =from u in userInfoList
                       select new
                       {
                           ID = u.ID,
                           UName = u.UserInfo.UName,
                           khName = u.UserInfo.khName,
                           intime = u.intime,
                           LGip = u.LGip,
                           addess=u.UserInfo.T_City.City
                       };
           

            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }      
        /// <summary>
        /// 获取指定网页源码
        /// </summary>
        /// <param name="url">指定的网页</param>
        /// <returns>网页源码</returns>
        private string getHtml(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("gb2312"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        #endregion
        #region 创建营销员
        public ActionResult NewAddListYXY( T_YxPerson typ)
        {
            typ.AddTime = MvcApplication.GetT_time();
            typ.ADDuserID = LoginUser.ID;
            typ.DEL = 0;
            if (typ.ID<=0)
            {
               
                T_YxPersonService.AddEntity(typ);
            }
            else
            {                
                T_YxPersonService.EditEntity(typ);
            }
            
            return Content("ok");
        }
        #endregion
        #region 获取营销员
        public ActionResult GetYYYone() {
            var id = Request["id"] == null ? 0 : Convert.ToInt64(Request["id"]);
            T_YxPerson temp = T_YxPersonService.LoadEntities(x => x.ID == id).FirstOrDefault();
            return Content(Common.SerializerHelper.SerializeToString(new { serverData = temp, msg = "yes" }));
        }
        #endregion
   
        #region 删除营销员
        public ActionResult DelListYXY()
        {
            var id = Convert.ToInt64(Request["id"]);
            var temp = T_YxPersonService.LoadEntities(x => x.ID == id).FirstOrDefault();
            temp.DEL = 1;
            if (temp != null)
            {
                if (T_YxPersonService.EditEntity(temp))
                {
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("no", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion
        //获取用户列表信息 只取ID 用户名 和没有绑定的用户
        public ActionResult GetNotBandWxUser() {
            var User = UserInfoService.LoadEntities(x => x.DelFlag ==0&&x.WxUsers.Count==0).DefaultIfEmpty();

            var temp = from a in User
                       select new
                       {
                           id = a.ID,
                           text = a.UName
                       };

            return Json(temp, JsonRequestBehavior.AllowGet);

        }
        //绑定微信和用户  解除微信与用户的绑定

        public ActionResult WxuserBandUserinfo()
        {
            int Uid =Convert.ToInt32(Request["Uid"]);
            long Wid = Convert.ToInt32(Request["Wid"]);
            bool IsJieC = Request["Jiechu"] == "null" ? false : true;
            var iwu = WxUserService.LoadEntities(x => x.ID == Wid).FirstOrDefault();
            if (IsJieC)
            {
                iwu.UserInfoID = null;
            }
            else {
                iwu.UserInfoID = Uid;
            }         
            string ret = "ok";
            if (!WxUserService.EditEntity(iwu))
            {
                ret = "no";
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }    

        #region 获取微信账号与绑定

        public ActionResult GetWxAppUser() {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 15;
            string ScName = Request["ScName"] != null ? Request["ScName"].ToString() : string.Empty;
            bool ispd = Request["ScB"] == null ? false : Convert.ToBoolean(Request["ScB"]);
            int totalCount = 0;
            IQueryable<WxUser> wu = WxUserService.LoadPageEntities(pageIndex, pageSize, out totalCount, x => x.Del != null, x => x.AddTime, false).DefaultIfEmpty();
            if (ScName == string.Empty) {
                if (!ispd)
                {
                    wu = wu.Where(x => x.UserInfoID == null).DefaultIfEmpty();
                }
                else
                {
                    wu = wu.Where(x => x.UserInfoID != null).DefaultIfEmpty();
                }
            }
            
            if (ScName != string.Empty) {
                wu = wu.Where(x => x.wxName.Contains(ScName)).DefaultIfEmpty();
            }
            if (wu.ToList()[0] == null)
            {
                return Json(new { rows ="", total = totalCount }, JsonRequestBehavior.AllowGet);
            }
            else {
                var temp = from a in wu
                           select new
                           {
                               a.ID,
                               a.wxName,
                               a.AddTime,
                               a.WxImg,
                               a.Wxgender,
                               a.Wxprovince,
                               a.Wxcity,
                               UserInfo = a.UserInfo == null ? "" : a.UserInfo.UName
                           };

                return Json(new { rows =  temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion

        #region 获取公告
        public ActionResult getGonggao() {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 15;
            int totalCount = 0;
            var Gonggaos = GongGaoService.LoadPageEntities(pageIndex, pageSize, out totalCount, x => x.Items ==7 && x.del ==false,x=>x.Addtime, false).DefaultIfEmpty();
            var ggs = Gonggaos.ToList();
            if (ggs[0] == null)
            {
                return Json(new { rows = "", total = totalCount }, JsonRequestBehavior.AllowGet);
            }
            else {
                var temp = from a in Gonggaos
                           select new
                           {
                               ID =  a.ID,
                               del = a.del,
                               bak = a.bak,
                               text = a.text,
                               a.Items,
                               Addtime = a.Addtime
                           };
                return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [ValidateInput(false)]
        //创建公告
        public ActionResult AddUpGonggao(GongGao gg) {
            if (gg.ID > 0)
            {
                GongGao ggs = GongGaoService.LoadEntities(x => x.ID == gg.ID).FirstOrDefault();
                ggs.text = gg.text;
                if (GongGaoService.EditEntity(ggs))
                {
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json("err", JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                gg.del = false;
                gg.Addtime = MvcApplication.GetT_time();
                gg.Items = 7;
                gg.bak = "通知公告";


                GongGaoService.AddEntity(gg);
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            
        }
        //修改公告 与信息确认
        [ValidateInput(false)]
        public ActionResult editUpGonggao()
        {
           
            Int32 id = Request["strId"] != null ? Convert.ToInt32( Request["strId"]) : 0;
            bool bl = Request["bl"] != null ? Convert.ToBoolean(Request["bl"]) :false;
            if (id == 0) {
                return Json( "noid" , JsonRequestBehavior.AllowGet);
            }
            GongGao gg = GongGaoService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (bl)
            {
                gg.del = bl;
            }          
            if (GongGaoService.EditEntity(gg))
            {
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            else {
                return Json("err", JsonRequestBehavior.AllowGet);
            }          
        }
        //删除公告
        public ActionResult delUpGonggao()
        {
            
            Int32 id = Request["strId"] != null ? Convert.ToInt32(Request["strId"]) : 0;
            if (id == 0)
            {
                return Json(new { ret = "noid" }, JsonRequestBehavior.AllowGet);
            }
            GongGao gg = GongGaoService.LoadEntities(x => x.ID == id).FirstOrDefault();            
            gg.del = true;            
            if (GongGaoService.EditEntity(gg))
            {
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json( "err", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
