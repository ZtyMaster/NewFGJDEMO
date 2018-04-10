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
    public class psaveController : BaseController
    {
        //
        // GET: /psave/
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }
        IBLL.IT_ItemsService T_ItemsService { get; set; }
        IBLL.IT_BiaoJiInfoService T_BiaoJiInfoService { get; set; }
        IBLL.IT_UserSaveService T_UserSaveService { get; set; }
        IBLL.IUserInfoService UserInfoService { get; set; }
        short Delflag = (short)DelFlagEnum.Normarl;
        public ActionResult Index()
        {
            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault();
            var City = T_CityService.LoadEntities(x => x.ID == CityID.T_City_ID).FirstOrDefault().T_Quyu.ToList();
            var Items = T_ItemsService.LoadEntities(x => x.DelFlag == null).ToList();
            var ItemsCount = Items != null ? Items.Distinct(new FastPropertyComparer<T_Items>("Bakstr")).ToList() : null;
            ViewBag.City = City;
            ViewBag.Items = Items;
            ViewBag.ItemsCount = ItemsCount.Count;
            //个人存储分配 
            ViewBag.Ftep = UserInfoService.LoadEntities(x => x.MasterID == LoginUser.MasterID).DefaultIfEmpty();           

            return View();
        }
        //分配个人存储信息
        public ActionResult FenPeiUserSave() {
            var Eid = Request["Eid"] != null ? Convert.ToInt64(Request["Eid"]) : 0;
            var UserID = Request["UserID"] != null ? Convert.ToInt32(Request["UserID"]) : 0;
            var temp = T_UserSaveService.LoadEntities(x => x.ID == Eid).FirstOrDefault();
            temp.UserID = UserID;
            if (T_UserSaveService.EditEntity(temp))
            {
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg="分配错误请联系管理员！" }, JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult GetHref()
        {

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 15;
            var totalCount = int.MaxValue;
            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault();
            string Str = Request["Str"];
            string Items = Request["Item"] == null ? null : Request["Item"] == "all" ? null : Request["Item"];

            #region 构建搜索条件
            UserInfoParam userInfoParam = new UserInfoParam()
            {
                C_id = LoginUser.ID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Str = Str,
                Tval = Request["Tval"],
                Items = Items,
                IsMaster = false

            };

            if (Request["Tval"] != null)
            {
                string str = userInfoParam.Tval.ToString();
                string[] list = str.Split(',');
                string[] ti = list[3].Split('>');
                userInfoParam.Zhuanxiu = ti[0] == "33" ? string.Empty : ti[0].ToString();

                string[] ti2 = list[2].Split('>');
                userInfoParam.Tingshi = ti2[0] == "26" ? string.Empty : ti2[0].ToString();

                string[] ti0 = list[0].Split('>');
                int imid = int.Parse(ti0[0]);
                userInfoParam.money = T_ItemsService.LoadEntities(x => x.ID == imid).FirstOrDefault().StrID.ToString();
                string[] ti1 = list[1].Split('>');
                int imid1 = int.Parse(ti1[0]);
                userInfoParam.Pingmu = T_ItemsService.LoadEntities(x => x.ID == imid1).FirstOrDefault().StrID.ToString();
            }

            #endregion
            var usersaveinfo = T_UserSaveService.LoadSearchEntities(userInfoParam);


            //找到所有该人员信息
            var savelist = T_UserSaveService.LoadPageEntities<DateTime>(pageIndex, pageSize, out totalCount, x => x.delflag == Delflag && x.UserInfo.MasterID==LoginUser.ID, x => x.AddTime, false).DefaultIfEmpty();
            
            var Iitems = T_ItemsService.LoadEntities(x => x.Icons == 2 || x.Icons == 3).DefaultIfEmpty();
            var temp = from a in usersaveinfo
                       from b in Iitems
                       where b.ID == a.ZhuanxiuStrID
                       from c in Iitems
                       where c.ID == a.HuxingStrID
                       select new
                       {
                           ID = a.ID,
                           TextName = a.TextName,
                           //Image_str = (a.Image_str.IndexOf("有") >= 0 ? "有" : "无"),
                           AddTime = a.AddTime,
                           PresonName = a.PresonName,
                           Address = a.Address,
                           PresonPhoto = a.PresonPhoto,
                           Money = a.Money,
                           HuxingStrID = c != null ? c.Str : string.Empty,
                           LouCheng = a.LouCheng,
                           ZhuanxiuStrID = b != null ? b.Str : string.Empty,
                           Chaoxiang = a.Chaoxiang,
                           Nianxian = a.Nianxian,
                           Mianji = a.Mianji,
                           BiaoJiID = a.BiaoJiID,
                           FwColors = a.T_BiaoJiInfo.Colors,
                           Items = a.Items,
                           Image = a.UserSaveImages.Count,
                           Guishu =a.UserInfo.UName
                       };

            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }

    }
}
