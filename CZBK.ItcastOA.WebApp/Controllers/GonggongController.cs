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
    public class GonggongController : BaseController
    {
        //
        // GET: /Gonggong/
        IBLL.IT_FGJHtmlDataService T_FGJHtmlDataService { get; set; }

        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IT_SaveHtmlDataService T_SaveHtmlDataService { get; set; }
        IBLL.IT_ItemsService T_ItemsService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }
        //short Delflag = (short)DelFlagEnum.Normarl;
        //short GongG = (short)GongGEnum.Show;
        //short hidden = (short)GongGEnum.heidden;
        public ActionResult Index()
        {
            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault();
            var City = T_CityService.LoadEntities(x => x.ID == CityID.T_City_ID).FirstOrDefault().T_Quyu.ToList();
            var Items = T_ItemsService.LoadEntities(x => x.DelFlag == null).ToList();
            var ItemsCount = Items != null ? Items.Distinct(new FastPropertyComparer<T_Items>("Bakstr")).ToList() : null;
            ViewBag.ItemsCount = ItemsCount.Count;
            ViewBag.City = City;
            ViewBag.Items = Items;
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
                //C_id =null,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Str = Str,
                MasterID = (bool)LoginUser.ThisMastr ? LoginUser.ID : (int)LoginUser.MasterID,                
                Tval = Request["Tval"]
            };
            MyhtmlInfoController.SetInfoParam(userInfoParam, T_ItemsService);

            var tem = T_SaveHtmlDataService.LoadSearchEntities(userInfoParam,true);
            //var tem = T_SaveHtmlDataService.LoadSearch(userInfoParam);
            //找到所有该人员信息
            // var savelist = T_SaveHtmlDataService.LoadPageEntities<DateTime>(pageIndex, pageSize, out totalCount, x => x.DelFlag == Delflag && x.GongGong == hidden, x => x.SaveTime, false).DefaultIfEmpty();

            // var ActionInfoList = T_FGJHtmlDataService.LoadSearchEntities(userInfoParam, false, true);

            var temp = from a in tem
                       select new
                       {
                           ID = a.ID,
                           HLName = a.T_FGJHtmlData.HLName,
                           Image_str = (a.T_FGJHtmlData.Image_str.IndexOf("有") >= 0 ? "有" : "无"),
                           FbTime = a.T_FGJHtmlData.FbTime,
                           PersonName = a.T_FGJHtmlData.PersonName,
                           Address = a.T_FGJHtmlData.Address,
                           Laiyuan = a.T_FGJHtmlData.Laiyuan,
                           FwSumMoney = a.T_FGJHtmlData.FwSumMoney,
                           FwHuXing = a.T_FGJHtmlData.FwHuXing,
                           FwLoucheng = a.T_FGJHtmlData.FwLoucheng,
                           FwZhuangxiu = a.T_FGJHtmlData.FwZhuangxiu,
                           FwChaoxiang = a.T_FGJHtmlData.FwChaoxiang,
                           FwNianxian = a.T_FGJHtmlData.FwNianxian,
                           FwMianji = a.T_FGJHtmlData.FwMianji,
                           FwBiaoJi = a.BiaoJiId,
                           FwBiaoJiID =a.ID,
                           FwColors = a.T_BiaoJiInfo.Colors,
                           FWsaveName = a.UserInfo.UName
                       };

            return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult saveitem()
        {
            if (Request["strId"] == null)
            {
                return Content("errer");
            }
            var id =Convert.ToInt64( Request["strId"]);
            var savehtml = T_SaveHtmlDataService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (savehtml != null)
            {
                savehtml.UserID = LoginUser.ID;
                savehtml.SaveTime = MvcApplication.GetT_time();
                savehtml.BiaoJiId = null;
                savehtml.GongGong = 0;
                savehtml.MasterID = LoginUser.MasterID;
                T_SaveHtmlDataService.EditEntity(savehtml);
                return Content("ok");
            }
            else
            {
                return Content("数据库中不存在要查询的ID号！");
            }
         
        }
    }
}
