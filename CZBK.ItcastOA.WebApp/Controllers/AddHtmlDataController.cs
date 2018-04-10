using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class AddHtmlDataController : BaseController
    {
        //
        // GET: /AddHtmlData/
        IBLL.IT_FGJHtmlDataService T_FGJHtmlDataService { get; set; }
        IBLL.IT_ItemsService T_ItemsService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }
        IBLL.IT_QiuZhuQiuGouService T_QiuZhuQiuGouService { get; set; }
        IBLL.IT_ChuZhuInfoService T_ChuZhuInfoService { get; set; }
        public ActionResult Index()
        {
            ViewBag.City = T_CityService.LoadEntities(x => x.DelFlag == 0).DefaultIfEmpty().ToList();
            ViewBag.ZhuangXiu = T_ItemsService.LoadEntities(x => x.Icons == 3).ToList();
            return View();
        }
        //读取我写入的信息
        public ActionResult SelectMyData()
        {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_FGJHtmlDataService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.AddItemsUserID == LoginUser.ID,x=>x.ID,false);
            var temp = from a in mydata
                       select new {
                           ID = a.ID,
                           HLName = a.HLName,
                           Image_str = (a.Image_str.IndexOf("有") >= 0 ? "有" : "无"),
                           FbTime = a.FbTime,
                           PersonName = a.PersonName,
                           Address = a.Address,
                           photo =a.photo,
                           Laiyuan = a.Laiyuan,
                           FwSumMoney = a.FwSumMoney,
                           FwHuXing = a.FwHuXing,
                           FwLoucheng = a.FwLoucheng,
                           FwZhuangxiu = a.FwZhuangxiu,
                           FwChaoxiang = a.FwChaoxiang,
                           FwNianxian = a.FwNianxian,
                           FwMianji = a.FwMianji
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);
        }
        //获取一条我写入的信息
        public ActionResult SelOneMyData()
        {
            var id = int.Parse(Request["IDs"]);
            var DelT = T_FGJHtmlDataService.LoadEntities(x => x.ID == id && x.AddItemsUserID == LoginUser.ID).FirstOrDefault();
            if (DelT == null)
            {
                return Json(new { ret = "no", msg = "没有找到您要修改的数据，请联系管理员！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { temp=DelT }, JsonRequestBehavior.AllowGet);
            }
            
        }
        //添加写入信息//修改我写入的信息
        public ActionResult AddEditData(T_FGJHtmlData tfhd)
        {
            if (tfhd.ID > 0)//修改
            {
                if (T_FGJHtmlDataService.EditEntity(tfhd))
                { return Json(new { ret = "ok",msg="修改成功" }, JsonRequestBehavior.AllowGet); }
                else
                {
                    return Json(new { ret = "no", msg = "在修改过程中出现未知错误，请联系管理员！" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
              
                tfhd.AddItemsUserID = LoginUser.ID;
                tfhd.Laiyuan = "AddDAT";
                tfhd.Pingmi_int =decimal.Parse( Request["FwMianji"]);
                tfhd.Money_int = decimal.Parse(Request["Smoney"]);
                tfhd.FwSumMoney = Request["Amoney"] + "万(单价" + tfhd.Money_int + "元/㎡)";
                tfhd.FwHuXing = Request["HXs"] + "室" + Request["HXt"] + "厅" + Request["HXw"] + "卫";
                tfhd.SumMoneyID = AllClass.GetMoney(Request["Smoney"].ToString());
                tfhd.MianjiID = AllClass.GeiMinji(tfhd.FwMianji);
                tfhd.HuXingID = AllClass.GetHuxing(tfhd.FwHuXing);
                tfhd.AddUserTiem = MvcApplication.GetT_time();
                tfhd.HLhref = "";
                tfhd.Image_str = tfhd.Image_str!=null?tfhd.Image_str.Trim().Length > 0 ? "有---" + tfhd.Image_str : tfhd.Image_str : tfhd.Image_str;

                var DisctD = T_FGJHtmlDataService.LoadEntities(x => x.photo == tfhd.photo && x.HLName == tfhd.HLName).FirstOrDefault();
                if (DisctD != null) {
                    return Json(new { ret = "no", msg = "要新增的信息中电话与信息名称在数据库中的有重复，请核对信息在添加！" }, JsonRequestBehavior.AllowGet);
                }
                T_FGJHtmlDataService.AddEntity(tfhd);
                return Json(new { ret = "ok", msg = "添加成功！" }, JsonRequestBehavior.AllowGet);
            }
        }
        //删除我写入的信息
        //
        public ActionResult delData()
        {
            var id = int.Parse(Request["IDs"]);
            var DelT= T_FGJHtmlDataService.LoadEntities(x => x.ID == id && x.AddItemsUserID == LoginUser.ID).FirstOrDefault();
            if (DelT == null)
            {
                return Json(new { ret = "no", msg = "不是本人创建信息不可删除，请联系管理员！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (T_FGJHtmlDataService.DeleteEntity(DelT))
                {
                    return Json(new { ret = "ok", msg = "删除成功！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { ret = "no", msg = "在删除过程中出现错误，请联系管理员！" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }


        #region 出租信息
        //获取出租信息
        public ActionResult GetChuzhuData() {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_ChuZhuInfoService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.UserID == LoginUser.ID, x => x.AdduserTime, false);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           HLName = a.ChuZhuName,
                           FbTime = a.FbTime,
                           PersonName = a.LianXiPerson,
                           Address = a.Addess,
                           photo = a.LianXiPhoto,
                           Laiyuan = a.LaiYuan,
                           FwSumMoney = a.Money,
                           Pinmi = a.PingMi,
                           Images = a.Images,
                           Bak = a.Bak,
                           FwNianxian = a.Tingshi
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);
        }
        //添加出租信息
        public ActionResult AddChuZhufrist(T_ChuZhuInfo Chuzhu) {
            if (Chuzhu.ID <= 0)
            {
                Chuzhu.UserID = LoginUser.ID;
                Chuzhu.LaiYuan = "NewAdd";
                Chuzhu.AdduserTime = MvcApplication.GetT_time();
                T_ChuZhuInfoService.AddEntity(Chuzhu);
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }           
        }
        //删除添加的数据
        public ActionResult DelAddChuZhufrist() {
            var id =Convert.ToInt64( Request["IDs"]);
            var tdata= T_ChuZhuInfoService.LoadEntities(x => x.ID == id && x.UserID == LoginUser.ID).FirstOrDefault();
            if (tdata != null)
            {
                if (T_ChuZhuInfoService.DeleteEntity(tdata))
                {
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg="没有成功删除信息！" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "数据库中没有找到要删除的项目！" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 求租信息

        //获取添加求租信息
        public ActionResult GetQiuZhu() {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_QiuZhuQiuGouService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.User_ID == LoginUser.ID, x => x.AddUserTime, false);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           HLName = a.Hname,
                           FbTime = a.FBtime,
                           PersonName = a.Hperson,
                           GuiShuDi = a.GuiShuDi,
                           photo = a.Photo,
                           FwSumMoney = a.Money,
                           JuShi = a.JuShi,
                           Address = a.QuYu
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);
        }
        //删除求租信息
        public ActionResult Delqiuzhu() {
            var id = Convert.ToInt64(Request["IDs"]);
            var tdata = T_QiuZhuQiuGouService.LoadEntities(x => x.ID == id && x.User_ID == LoginUser.ID).FirstOrDefault();
            if (tdata != null)
            {
                if (T_QiuZhuQiuGouService.DeleteEntity(tdata))
                {
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "没有成功删除信息！" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "数据库中没有找到要删除的项目！" }, JsonRequestBehavior.AllowGet);
        }
        //添加求租信息
        public ActionResult AddQiuZhu(T_QiuZhuQiuGou tqz)
        {
            if (tqz.ID <= 0)
            {
                tqz.User_ID = LoginUser.ID;
                tqz.AddUserTime = MvcApplication.GetT_time();
                T_QiuZhuQiuGouService.AddEntity(tqz);
                return Json(new { ret = "ok", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
            }
            
        }
        #endregion
     
    }
}
