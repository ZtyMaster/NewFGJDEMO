using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class HomeTipController : BaseController
    {
        //
        // GET: /HomeTip/
        IBLL.IT_ScehMiShuService T_ScehMiShuService { get; set; }
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IScrherSAVEService ScrherSAVEService { get; set; }
        IBLL.IT_FGJHtmlDataService T_FGJHtmlDataService { get; set; }
        IBLL.IGongGaoService GongGaoService { get; set; }
        public ActionResult Index()
        {
            var ThisId=T_ScehMiShuService.LoadEntities(x => x.UserId == LoginUser.ID).DefaultIfEmpty();
           
            var counts = 0;
            var ListC = 0;
            if (ThisId.ToList()[0] != null) {
                foreach (var td in ThisId)
                {
                    
                    int count = GetSeachdata((long)td.ScrherSAVEId, 1, 80);
                    if (count > td.ScehCount)
                    {
                        counts++;
                        ListC = ListC + (count - td.ScehCount);
                    }
                }
            }

           
            
            ViewBag.XmsCount = counts;
            ViewBag.ListC = ListC;
            return View();
        }
        public int GetSeachdata(long XmsID, int pageIndex, int pageSize)
        {
            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault();
            var totalCount = 0;
            UserInfoParam Uxms = new UserInfoParam();
            var xms = ScrherSAVEService.LoadEntities(x => x.ID == XmsID && x.DEL == 0).FirstOrDefault();

            Uxms.PageIndex = pageIndex;
            Uxms.PageSize = pageSize;
            Uxms.TotalCount = totalCount;
            Uxms.ssve = xms;
            Uxms.C_id = CityID.T_City_ID;

            T_FGJHtmlDataService.LoadSearchPM(Uxms);
            return Uxms.TotalCount;
        }

        public ActionResult GetGglist() {
            var GongG = GongGaoService.LoadEntities(x => x.Items == 7 && x.del == false).DefaultIfEmpty();
            var temp = from a in GongG
                       select new {
                           a.text,
                           a.Addtime,
                           a.ID
                       };
            temp = temp.OrderByDescending(x => x.Addtime);
            return Json(new { ret = temp }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GundongGG()
        {
            var GongG = GongGaoService.LoadEntities(x => x.Items == 8 ).DefaultIfEmpty();
            var temp = from a in GongG
                       select new
                       {
                           a.text,
                           a.Addtime,
                           a.ID
                       };
            temp = temp.OrderByDescending(x => x.Addtime);
            return Json(new { ret = temp }, JsonRequestBehavior.AllowGet);

        }

    }
}
