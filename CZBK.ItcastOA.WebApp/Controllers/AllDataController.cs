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
    public class AllDataController : BaseController
    {
        //
        // GET: /AllData/

        IBLL.IT_FGJHtmlDataService T_FGJHtmlDataService { get; set; }
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IT_UserSaveService T_UserSaveService { get; set; }
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IT_ItemsService T_ItemsService { get; set; }
        IBLL.IT_CityService T_CityService { get; set;}
        IBLL.IT_BiaoJiInfoService T_BiaoJiInfoService { get; set; }
        IBLL.IT_SaveHtmlDataService T_SaveHtmlDataService { get; set; }
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
            return View();
        }
        public ActionResult GetHref()
        {
            

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 15;
            var totalCount = int.MaxValue;
            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault();
            string Str = Request["Str"];
            //构建搜索条件          
            UserInfoParam userInfoParam = new UserInfoParam()
            {
                C_id =Convert.ToInt32( LoginUser.ID),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Str = Str,
                Tval = Request["Tval"]
            };
            MyhtmlInfoController.SetInfoParam(userInfoParam, T_ItemsService);
            //MyhtmlInfoController mhc = new MyhtmlInfoController();
            //mhc.SetInfoParam
            //找到所有该人员信息
            var savelist = T_SaveHtmlDataService.LoadPageEntities<DateTime>(pageIndex, pageSize, out totalCount, x =>x.UserInfo.MasterID == LoginUser.ID && x.DelFlag == Delflag, x => x.SaveTime, false).DefaultIfEmpty();
            var ActionInfoList = T_FGJHtmlDataService.LoadSearchFrist(userInfoParam, false,false);

            var temp = from a in ActionInfoList
                       from b in savelist
                       where b.HtmldataID == a.ID
                       select new
                       {
                           ID = a.ID,
                           HLName = a.HLName,
                           Image_str = (a.Image_str.IndexOf("有") >= 0 ? "有" : "无"),
                           FbTime = a.FbTime,
                           PersonName = a.PersonName,
                           Address = a.Address,
                           photo = a.photo,
                           Laiyuan = a.Laiyuan,
                           FwSumMoney = a.FwSumMoney,
                           FwHuXing = a.FwHuXing,
                           FwLoucheng = a.FwLoucheng,
                           FwZhuangxiu = a.FwZhuangxiu,
                           FwChaoxiang = a.FwChaoxiang,
                           FwNianxian = a.FwNianxian,
                           FwMianji = a.FwMianji,
                           FwBiaoJi = b.BiaoJiId,
                           FwBiaoJiID = b.ID,
                           FwColors = b.T_BiaoJiInfo.Colors,
                           FwUserName=b.UserInfo.UName
                       };

            return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
        }

    }
}
