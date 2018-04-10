using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class AddCityHtmlController : BaseController
    {
        //
        // GET: /AddCityHtml/
        IBLL.IT_provinceService T_provinceService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }
        IBLL.IT_UpHerfCityService T_UpHerfCityService { get; set; }
        IBLL.IGongGaoService GongGaoService { get; set; }
        IBLL.IT_QuyuService T_QuyuService { get; set; }
        public ActionResult Index()
        {
            ViewBag.province = T_provinceService.LoadEntities(x => x.ID > 0).DefaultIfEmpty().ToList();
            ViewBag.City = T_CityService.LoadEntities(x => x.DelFlag == 0).DefaultIfEmpty().ToList();
            return View();
        }
        // 获取省份
        public ActionResult Getprovince() {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_provinceService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.ID>0, x => x.ID, true);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           ppp= a.ppp,
                           str= a.str
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);

        }
        //添加省份
        public ActionResult Addprovince(T_province tpe)
        {
            if (tpe.ID <= 0)
            {
               // var ts = T_provinceService.LoadEntities(x => x.str == tpe.str).FirstOrDefault();
                if (T_provinceService.LoadEntities(x => x.str == tpe.str).FirstOrDefault() == null)
                {
                    T_provinceService.AddEntity(tpe);
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "不可添加重复的省份！~", JsonRequestBehavior.AllowGet });
                }
              
            }
            else
            {
                return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
            }
            
        }
        //删除省份
        public ActionResult delprovince()
        {
            var id = Convert.ToInt64(Request["IDs"]);
            var tdata = T_provinceService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (tdata != null)
            {
                if (T_provinceService.DeleteEntity(tdata))
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

        // 获取城市
        public ActionResult GetCity()
        {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_CityService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.DelFlag==0, x => x.province_id, true);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           City = a.City,
                           str = a.T_province.str
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);

        }
        //添加城市
        public ActionResult AddCity(T_City tpe)
        {
            if (tpe.ID <= 0)
            {
                if (T_CityService.LoadEntities(x => x.City == tpe.City).FirstOrDefault()== null)
                {
                    tpe.DelFlag = 0;
                    T_CityService.AddEntity(tpe);
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "不可添加重复的城市！~", JsonRequestBehavior.AllowGet });
                }

               
            }
            else
            {
                return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
            }

        }
        //删除城市
        public ActionResult delCity()
        {
            var id = Convert.ToInt64(Request["IDs"]);
            var tdata = T_CityService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (tdata != null)
            {
                if (T_CityService.DeleteEntity(tdata))
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
        //添加区域
        public ActionResult AddQuyu(T_Quyu tpe)
        {
            if (tpe.ID <= 0)
            {
                //if (T_QuyuService.LoadEntities(x => x.QY_connet == tpe.QY_connet).FirstOrDefault() != null)
                //{

                //}
                //else
                //{
                //    return Json(new { msg = "不可添加重复的区域！~", JsonRequestBehavior.AllowGet });
                //}
                tpe.DelFlag = false;
                T_QuyuService.AddEntity(tpe);
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
            }

        }
        //删除区域
        public ActionResult delQuyu()
        {
            var id = Convert.ToInt64(Request["IDs"]);
            var tdata = T_QuyuService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (tdata != null)
            {
                if (T_QuyuService.DeleteEntity(tdata))
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
        //获取区域
        public ActionResult GetHrefquyu() {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_QuyuService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.ID>0, x => x.ID, false);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           Cityid = a.T_CityID,
                           City=a.T_City.City,
                           str = a.QY_connet,
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);
        }


        // 获取网站更新地址
        public ActionResult GetHrefCity()
        {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = T_UpHerfCityService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.Del == 0, x => x.AddTime, true);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           City = a.T_City.City,
                           Province=a.T_City.T_province.str,
                           Items = a.Items,
                           Href=a.Href,
                           textbak=a.textbak,
                           AddTime=a.AddTime,

                       };
            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);
        }
        //添加网站更新地址
        public ActionResult AddHrefCity(T_UpHerfCity tpe)
        {
            if (tpe.ID <= 0)
            {
                if (T_UpHerfCityService.LoadEntities(x => x.Items == tpe.Items&&x.City_ID==tpe.City_ID).FirstOrDefault() == null)
                {
                    tpe.Del = 0;
                    tpe.AddTime = MvcApplication.GetT_time();
                    tpe.AddUser = LoginUser.ID;
                    T_UpHerfCityService.AddEntity(tpe);
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "不可添加重复的更新地址！~", JsonRequestBehavior.AllowGet });
                }


            }
            else
            {
                return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
            }

        }
        //删除网站更新地址
        public ActionResult delHrefCity()
        {
            var id = Convert.ToInt64(Request["IDs"]);
            var tdata = T_UpHerfCityService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (tdata != null)
            {
                if (T_UpHerfCityService.DeleteEntity(tdata))
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

        //获取下拉城市列表
        public ActionResult GetCitylist()
        {
            var id = Convert.ToInt64(Request["id"]);
            var tdata = T_CityService.LoadEntities(x => x.province_id==id).DefaultIfEmpty();
            if (tdata.ToList()[0] == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var temp = from a in tdata
                       select new {
                           ID=a.ID,
                           City=a.City
                       };          
            return Json(temp, JsonRequestBehavior.AllowGet);
        }


        #region 公告通知

        //获取公告信息
        public ActionResult GetGongGao()
        {
            int pageIdex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 35;
            int totalcount = int.MaxValue;
            var mydata = GongGaoService.LoadPageEntities(pageIdex, pageSize, out totalcount, x => x.ID > 0, x => x.ID, true);
            var temp = from a in mydata
                       select new
                       {
                           ID = a.ID,
                           Items = a.Items,
                           text = a.text,
                           bak = a.bak
                       };

            return Json(new { rows = temp, total = totalcount }, JsonRequestBehavior.AllowGet);
        }
        //删除公告信息
        public ActionResult DelGongGao()
        {
            var id = Convert.ToInt64(Request["IDs"]);
            var tdata = GongGaoService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (tdata != null)
            {
                if (GongGaoService.DeleteEntity(tdata))
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
        //添加公告信息
        public ActionResult AddGongGao(GongGao tqz)
        {
           
            if (tqz.ID <= 0)
            {
                var isnull = GongGaoService.LoadEntities(x => x.Items == tqz.Items).FirstOrDefault();
                if (isnull != null)
                {
                    return Json(new { msg = "文本分类必须保证唯一性~", JsonRequestBehavior.AllowGet });
                }
                GongGaoService.AddEntity(tqz);
                return Json(new { ret = "ok", JsonRequestBehavior.AllowGet });
            }
            else
            {
                GongGaoService.EditEntity(tqz);
                return Json(new { ret = "editok",msg = "修改成功！~", JsonRequestBehavior.AllowGet });
            }

        }
        //修改公告信息
        public ActionResult editGongGao(GongGao tqz)
        {
            if (tqz.ID <= 0)
            {
                GongGaoService.AddEntity(tqz);
                return Json(new { ret = "ok", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
            }

        }
        //获取单条修改公告信息
        public ActionResult GeteditGongGao()
        {
            return Json(new { msg = "添加出错，请联系管理员！~", JsonRequestBehavior.AllowGet });
        }
        #endregion
    }
}
