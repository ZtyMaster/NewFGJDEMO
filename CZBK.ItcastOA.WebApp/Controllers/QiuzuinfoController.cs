using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class QiuzuinfoController : BaseController
    {
        //
        // GET: /Qiuzuinfo/
        IBLL.IT_QiuZhuQiuGouService T_QiuZhuQiuGouService { get; set; }
        IBLL.IT_ChuZhuInfoService T_ChuZhuInfoService { get; set; }
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.ISeeQzCzService SeeQzCzService { get; set; }
        IBLL.IT_UserClickService T_UserClickService { get; set; }
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }

        public ActionResult Index()
        {
            ViewBag.AllClick = LoginUser.Click;
            // 获取点击次数           
            ViewBag.OnClic = UserInfoService.GetMaxClick(LoginUser.ID);
            ViewBag.LMasterID = LoginUser.MasterID;
            ViewBag.City = UserInfo_CityService.LoadMyCity_Quyu(LoginUser.ID);
            return View();
        }
        public ActionResult GetHref()
        {
            
            UserInfoParam uip = new UserInfoParam();
            uip.PageIndex= Request["page"] != null ? int.Parse(Request["page"]) : 1;
            uip.PageSize= Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            uip.TotalCount = 0;
            uip.CityID = LoginUser.CityID;
            uip.Str= Request["Str"] != null ? Request["Str"] : null;
            uip.Isee = Request["Isee"] != null ? Convert.ToBoolean(Request["Isee"]) : false;
            uip.C_id = LoginUser.ID;
            var actioninfolist = T_QiuZhuQiuGouService.LoadSearchEntities(uip);
            var temp = from a in actioninfolist
                       select new
                       {
                           ID = a.ID,
                           Hname = a.Hname,
                           QuYu = a.QuYu,
                           JuShi = a.JuShi,
                           Money = a.Money,
                           FBtime = a.FBtime,
                           Hperson = a.Hperson,
                           Photo = a.Photo,
                           GuiShuDi = a.GuiShuDi,
                           ClickCount= a.SeeQzCzs.Count,
                           IsQZ = "QZ"
                       };
            return Json(new { rows = temp, total = uip.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult saveitem()
        { return Content(""); }
        #region 获取出租信息
        public ActionResult GetChuZhudata() {
            UserInfoParam uip = new UserInfoParam();
            uip.PageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            uip.PageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            uip.TotalCount = 0;
            uip.CityID = LoginUser.CityID;
            uip.Str = Request["Str"] != null ? Request["Str"] : null;
            uip.Isee = Request["Isee"] != null ? Convert.ToBoolean(Request["Isee"]) : false;
            uip.C_id = LoginUser.ID;    
            var temp = T_ChuZhuInfoService.LoadSearchEntities(uip);
            var Rtemo = from a in temp
                        select new
                        {
                            ID = a.ID,
                            TextName = a.ChuZhuName,
                            Pername = a.LianXiPerson,
                            Photo = a.LianXiPhoto,
                            Address = a.Addess,
                            Fbtime = a.FbTime,
                            Money = a.Money,
                            PinMi = a.PingMi,
                            Bak = a.Bak,
                            Images = a.Images,
                            ClickCount = a.SeeQzCzs.Count(),
                            Laiyuan= a.LaiYuan,
                            IsQZ ="CZ",
                            url=a.ChuZhuHref
                        };

            return Json(new {rows= Rtemo, total=uip.TotalCount },JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查看图片
        public ActionResult SeeImage() {
            int id = int.Parse(Request["id"]);
            var temp = T_ChuZhuInfoService.LoadEntities(x => x.ID == id).FirstOrDefault();
            string imageSTR = temp.Images;
            string Masimage = imageSTR.Replace("有---", string.Empty);
            if (temp != null)
            {
                return Content(Common.SerializerHelper.SerializeToString(new { sData = Masimage, msg = "ok" }));
            }
            else
            {
                return Content(Common.SerializerHelper.SerializeToString(new { msg = "no" }));
            }
        }
        #endregion
        public ActionResult SeePhoto() {
            var ID = Convert.ToInt32(Request["ID"]);
            var Items = Request["Items"];
           
            //检查用户点击中是否有该用户
            var Uclick = T_UserClickService.LoadEntities(x => x.UserInfoId == LoginUser.ID).FirstOrDefault();

           //检查是否点击过查看电话
            var SeeClick = Items == "QZ"? SeeQzCzService.LoadEntities(x => x.UserID == LoginUser.ID && x.QiuZhuID==ID).FirstOrDefault(): Items == "CZ"? SeeQzCzService.LoadEntities(x => x.UserID == LoginUser.ID && x.ChuZhuID == ID).FirstOrDefault():null;
            if (SeeClick != null)
            {
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //检查点击是否超出当天点击量
                if (UserInfoService.GetMaxClick(LoginUser.ID) >= LoginUser.Click)
                {
                    return Json(new { msg = "此用户账户查看量已上限！" }, JsonRequestBehavior.AllowGet);
                }

                #region MyRegion
                //是否开启与主号保存冲突
                var t_bool = T_BoolItemService.LoadEntities(x => x.ID == 1).FirstOrDefault();

                //检查当前要查询的信息是否被当前主号下其他小号保存
                if (Convert.ToBoolean(LoginUser.ThisMastr))
                {
                    #region MyRegion
                    if (t_bool != null)
                    {
                        if (t_bool.BOLL_)
                        {
                            string temp = GetSelectSmallSave(LoginUser.ID, ID, Items);
                            if (temp != "on")
                            {
                                return Json(new { msg = "其他成员已保存该信息,保存人员为【" + temp + "】" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    //当前点击是主号
                    string str = SeeClickPhoto(Uclick, ID, Items);
                    if (str == "OK")
                    {
                        return Json(new { ret = "ok", Uclick = UserInfoService.GetMaxClick(LoginUser.ID), MtrId = LoginUser.MasterID }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { msg = "系统出错！请联系管理员！" }, JsonRequestBehavior.AllowGet);
                    #endregion
                }
                else
                {
                    if (t_bool != null)
                    {
                        if (t_bool.BOLL_)
                        {
                            var mastr_save = SeeQzCzService.LoadEntities(x => x.UserID == LoginUser.MasterID && (Items=="QZ"?x.QiuZhuID==ID:x.ChuZhuID==ID)).FirstOrDefault();
                            if (mastr_save != null)
                            {
                                return Json(new { msg = "该信息已被主号保存！其他人员不可保存!" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                    string temp = GetSelectSmallSave(LoginUser.MasterID, ID,Items);
                    if (temp != "on")
                    {
                        return Json(new { msg = "其他成员已保存该信息,保存人员为【" + temp + "】" }, JsonRequestBehavior.AllowGet);                       
                    }
                    else
                    {
                        string str = SeeClickPhoto(Uclick, ID,Items);
                        //SignalRHub srh = new SignalRHub();
                        //srh.SenMasterMsg(LoginUser.MasterID.ToString(), UserInfoService.GetMaxClick(LoginUser.ID).ToString());   
                        if (str == "OK")
                        {
                            return Json(new { ret = "ok", Uclick = UserInfoService.GetMaxClick(LoginUser.ID),MtrId=LoginUser.MasterID }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { msg="系统出错！请联系管理员！" }, JsonRequestBehavior.AllowGet);
                    }
                }
                #endregion
            }
       



        }

        private string GetSelectSmallSave(int? masterID, long id ,string Items)
        {
            var UMaster = UserInfoService.LoadEntities(x => x.MasterID == masterID).DefaultIfEmpty();
            var temp = from a in UMaster
                       from v in a.SeeQzCzs
                       where Items=="QZ"?(v.QiuZhuID==id):(v.ChuZhuID==id)
                       select new
                       {
                           ID = v.ID,
                           Name = v.UserInfo.UName
                       };
            var tt = temp.ToArray();
            string str = temp.Count() > 0 ? tt[0].Name : "on";
            return str;
        }

        private string SeeClickPhoto(T_UserClick Uclick, long id,string Items)
        {
            if (Uclick == null)
            {
                //如果没有添加一条用户点击信息
                T_UserClick tucs = new T_UserClick();
                tucs.UserInfoId = LoginUser.ID;
                tucs.ThisClick = 1;
                tucs.LoginClickTime = Convert.ToDateTime(MvcApplication.GetT_time().ToString("yyyy-MM-dd"));
                T_UserClickService.AddEntity(tucs);
            }
            else
            {

                DateTime logintime = Convert.ToDateTime(Uclick.LoginClickTime);
                if (logintime.ToString("yyyy-MM-dd") == MvcApplication.GetT_time().ToString("yyyy-MM-dd"))
                {
                    Uclick.ThisClick = Uclick.ThisClick + 1;
                    T_UserClickService.EditEntity(Uclick);
                }
                else if (logintime < MvcApplication.GetT_time())
                {
                    Uclick.ThisClick = 1;
                    Uclick.LoginClickTime = Convert.ToDateTime(MvcApplication.GetT_time().ToString("yyyy-MM-dd"));
                    T_UserClickService.EditEntity(Uclick);
                }
            }
            SeeQzCz scp = new SeeQzCz();
            scp.Del = 0;
            scp.UserID = LoginUser.ID;
            if (Items == "QZ")
            {
                scp.QCItems = 0;
                scp.QiuZhuID = id;
            }
            else
            {
                scp.ChuZhuID = 1;
                scp.ChuZhuID = id;
            }
            SeeQzCzService.AddEntity(scp);                  
            return "OK";
        }
    }
}
