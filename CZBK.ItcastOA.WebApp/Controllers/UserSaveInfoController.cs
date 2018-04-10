using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class UserSaveInfoController : BaseController
    {
       
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }
        IBLL.IT_ItemsService T_ItemsService { get; set; }
        IBLL.IT_BiaoJiInfoService T_BiaoJiInfoService { get; set; }
        IBLL.IT_UserSaveService T_UserSaveService { get; set; }
        IBLL.IUserSaveImageService UserSaveImageService { get; set; }
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }

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
            var Itemsbool = T_BoolItemService.LoadEntities(x => x.str== "上传张数").FirstOrDefault();
            if (Itemsbool == null)
            {
                return Content("no:数据库[T_BoolItem][str]列没有限制上传张数的数据属性，检查数据库完整性！");
            }
            var imagecount = UserSaveImageService.LoadEntities(x => x.UserSaveID == LoginUser.ID).DefaultIfEmpty();
            ViewBag.imagecount = Itemsbool.@int;
            return View();
        }
        public ActionResult AddUser(T_UserSave tus)
        {
            if (tus.ID == 0)
            {
                T_UserSave userins = T_UserSaveService.LoadEntities(x => x.TextName == tus.TextName && x.UserID == LoginUser.ID).FirstOrDefault();
                if (userins == null)
                {
                    tus.AddTime = MvcApplication.GetT_time();
                    tus.delflag = 0;
                    tus.UserID = LoginUser.ID;
                    tus.Money = tus.Money * 10000;
                    tus.SumMoneyID = GetMoney(tus.Money.ToString());
                    tus.MianjiID = GeiMinji(tus.Mianji.ToString());
                    T_UserSaveService.AddEntity(tus);
                    var Mid = T_UserSaveService.LoadEntities(x => x.TextName == tus.TextName).FirstOrDefault();

                    #region 图片处理
                    List<string> List_str = new List<string>();
                    var Str_list = Request["MenuIcon"];
                    if (Str_list.Trim().Length > 0)
                    {
                        var Lsp = Str_list.Split(',');
                        foreach (string stt in Lsp)
                        {
                            #region 读取存储图片数据
                            HttpPostedFileBase file = Request.Files[stt];
                            if (file != null)
                            {
                                string filename = Path.GetFileName(file.FileName);//获取上传的文件名
                                string fileExt = Path.GetExtension(filename);//获取扩展名       
                                #region MyRegion
                                if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".gif" || fileExt == ".jpeg" || fileExt == ".bmp")
                                {
                                    var user = UserInfoService.LoadEntities(x => x.ID == LoginUser.MasterID).FirstOrDefault();
                                    if (user == null)
                                    {
                                        T_UserSaveService.DeleteEntity(Mid);
                                        return Content("no:主号ID错误,为获取倒主号ID无法建立存储路径！");
                                    }
                                    string dir = "/UserSaveImage/" + user.UName + "/" + LoginUser.UName + "/" + MvcApplication.GetT_time().ToString("yyyy-MM-dd") + "/";
                                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                                    string filenewName = Guid.NewGuid().ToString();
                                    string fulldir = dir + filenewName + fileExt;
                                    file.SaveAs(Request.MapPath(fulldir));
                                    List_str.Add(fulldir);
                                }
                                else
                                {
                                    T_UserSaveService.DeleteEntity(Mid);
                                    return Content("no:文件类型错误，文件扩展名错误！");
                                }
                                #endregion
                            }
                            else
                            {
                                T_UserSaveService.DeleteEntity(Mid);
                                return Content("no:请上传图片文件");
                            }
                            #endregion
                        }
                        //写入数据
                        foreach (string str in List_str)
                        {
                            UserSaveImage usi = new UserSaveImage();
                            usi.UserSaveID = Mid.ID;
                            usi.Image_str = str;
                            UserSaveImageService.AddEntity(usi);
                        }
                    }
                    #endregion

                    return Content("ok");
                }
                else
                {
                    return Content("no:信息名称不可重复添加！");
                }
            }
            else
            {
               var Editdata= T_UserSaveService.LoadEntities(x => x.ID == tus.ID).FirstOrDefault();
                if (Editdata != null)
                {
                    Editdata.Items = tus.Items;
                    Editdata.TextName = tus.TextName;
                    Editdata.PresonName = tus.PresonName;
                    Editdata.PresonPhoto = tus.PresonPhoto;
                    Editdata.Address = tus.Address;
                    Editdata.Money = tus.Money;
                    Editdata.ZhuanxiuStrID = tus.ZhuanxiuStrID;
                    Editdata.Mianji = tus.Mianji;
                    Editdata.LouCheng = tus.LouCheng;
                    Editdata.HuxingStrID = tus.HuxingStrID;
                    Editdata.Chaoxiang = tus.Chaoxiang;
                    Editdata.Bak = tus.Bak;
                    if (T_UserSaveService.EditEntity(Editdata))
                    { return Content("ok"); }
                    else
                    { return Content("no:信息名称不可重复添加！"); }
                }
                else
                {
                    return Content("no:无修改数据！");
                }

            }
            
           
          
        }
        public ActionResult GetHref()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 15;
            var totalCount = int.MaxValue;
            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault();
            string Str = Request["Str"];
            string Items = Request["Item"]==null?null: Request["Item"] == "all" ? null : Request["Item"];
            //构建搜索条件          
            UserInfoParam userInfoParam = new UserInfoParam()
            {
                C_id = LoginUser.ID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Str = Str,
                Tval = Request["Tval"],
                Items = Items,
                IsMaster = true
            };
            //MyhtmlInfoController mhic = new MyhtmlInfoController();            
            //mhic.SetInfoParam(userInfoParam, T_ItemsService);
            if (Request["Tval"] != null)
            {
                #region 123
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
                #endregion
            }            
            var usersaveinfo = T_UserSaveService.LoadSearchEntities(userInfoParam);

            //找到所有该人员信息
            var savelist = T_UserSaveService.LoadPageEntities<DateTime>(pageIndex, pageSize, out totalCount,x=> x.delflag == Delflag && x.UserID==LoginUser.ID, x => x.AddTime, false).DefaultIfEmpty();
            // var ActionInfoList = T_FGJHtmlDataService.LoadSearchEntities(userInfoParam, false);
            //var biaoji = T_BiaoJiInfoService.LoadEntities(x=>x.DelFlag== Delflag).DefaultIfEmpty();
            var Iitems = T_ItemsService.LoadEntities(x=>x.Icons==2||x.Icons==3).DefaultIfEmpty();
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
                           Bak = a.Bak,
                           Image = a.UserSaveImages.Count,
                           Items=a.Items
                       };

            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        #region 金钱ID
        private static int GetMoney(string da)
        {            
            string str = da.Replace("万", string.Empty);
            str = str.Replace("以下", string.Empty);
            str = str.Replace("以上", string.Empty);
            double mj = Convert.ToDouble(str);
            if (mj < 30)
            {
                return 1;
            }
            else if (mj >= 30 && mj < 40)
            {
                return 2;
            }
            else if (mj >= 40 && mj < 50)
            {
                return 3;
            }
            else if (mj >= 50 && mj < 60)
            {
                return 4;
            }
            else if (mj >= 60 && mj < 80)
            {
                return 5;
            }
            else if (mj >= 80 && mj < 100)
            {
                return 6;
            }
            else if (mj >= 100 && mj < 120)
            {
                return 7;
            }
            else if (mj >= 120 && mj < 160)
            {
                return 8;
            }
            else if (mj >= 160 && mj < 200)
            {
                return 9;
            }
            else
            {
                return 10;
            }
        }
        #endregion
        #region 面积ID
        private static int GeiMinji(string da)
        {
           
            string str = da.Replace("以下", string.Empty);
            str = str.Replace("以上", string.Empty);

            double mj = Convert.ToDouble(str);
            if (mj < 50)
            {
                return 1;
            }
            else if (mj >= 50 && mj < 70)
            {
                return 2;
            }
            else if (mj >= 70 && mj < 90)
            {
                return 3;
            }
            else if (mj >= 90 && mj < 110)
            {
                return 4;
            }
            else if (mj >= 110 && mj < 130)
            {
                return 5;
            }
            else if (mj >= 130 && mj < 150)
            {
                return 6;
            }
            else if (mj >= 150 && mj < 200)
            {
                return 7;
            }
            else if (mj >= 200 && mj < 300)
            {
                return 8;
            }
            else if (mj >= 300 && mj < 500)
            {
                return 9;
            }
            else
            {
                return 10;
            }
        }
        #endregion

     
        #region 获取上传图片
        public ActionResult FileUpload()
        {
            return Content(null);

        }
        #endregion

        #region 删除上传图片
        public ActionResult DeletImage()
        {
            var Filestr = Request["deletefile"];

            try {
                if (Directory.Exists(Path.GetDirectoryName(Request.MapPath(Filestr))))
                {
                    System.IO.File.Delete(Request.MapPath(Filestr));
                }
                else
                {
                    return Content("no:未找到要删除的图片！" );
                }
                return Content("yes:成功删除！");
            }
            catch (Exception e)
            {
                return Content("no:"+e.ToString());
            }
          
           

        }
        #endregion

        public ActionResult SeeImage()
        {
            long id = Convert.ToInt64( Request["id"]);
            T_UserSave temp = T_UserSaveService.LoadEntities(x => x.ID == id).FirstOrDefault();
            string ret = string.Empty;
            string retId = string.Empty;
            
            foreach (UserSaveImage usi in  temp.UserSaveImages)
            {
                ret = ret.Length > 0 ? ret + "," + usi.Image_str : usi.Image_str;
                retId = retId.Length > 0 ? retId + "," + usi.ID.ToString() : usi.ID.ToString();

            }
            if (temp != null)
            {
                return Json(new { sdata = ret, retId= retId, msg = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content(Common.SerializerHelper.SerializeToString(new { msg = "no" }));
            }            
        }
        #region 获取修改数据信息
        public ActionResult EditInfo() {
            var id =Convert.ToInt64( Request["id"]);
            var tdata = T_UserSaveService.LoadEntities(x => x.ID == id&&x.delflag==0).DefaultIfEmpty();
            var temp = from a in tdata
                       select new
                       {
                            a.ID, a.TextName,
                           a.PresonName,a.PresonPhoto,a.Address,
                           a.Money,a.HuxingStrID,a.Mianji,a.LouCheng,
                           a.ZhuanxiuStrID,
                           a.Chaoxiang,a.Items,a.Bak
                           
                       };
            return Json(temp, JsonRequestBehavior.AllowGet);
            //return Content(Common.SerializerHelper.SerializeToString(new { sData = tdata, msg = "ok" }));
        }
        #endregion


        #region 追加上传图片
        public ActionResult ZJFileUpload()
        {
            HttpPostedFileBase file = Request.Files["fileIconUp"];
            var Mid = Convert.ToInt64(Request["UserSaveID"]);

            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);//获取上传的文件名
                string fileExt = Path.GetExtension(filename);//获取扩展名       
                #region MyRegion
                if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".gif" || fileExt == ".jpeg" || fileExt == ".bmp")
                {
                    var user = UserInfoService.LoadEntities(x => x.ID == LoginUser.MasterID).FirstOrDefault();
                    if (user == null)
                    {
                        return Content("no:主号ID错误,为获取倒主号ID无法建立存储路径！");
                    }
                    string dir = "/UserSaveImage/" + user.UName + "/" + LoginUser.UName + "/" + MvcApplication.GetT_time().ToString("yyyy-MM-dd") + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                    string filenewName = Guid.NewGuid().ToString();
                    string fulldir = dir + filenewName + fileExt;
                    file.SaveAs(Request.MapPath(fulldir));
                    var TTCot = UserSaveImageService.LoadEntities(x => x.UserSaveID == Mid).Count();
                    if (TTCot >= 6)
                    {
                        return Content("no:图片最多只可存6张！");
                    }
                    UserSaveImage usi = new UserSaveImage();
                    usi.UserSaveID = Mid;
                    usi.Image_str = fulldir;
                    UserSaveImageService.AddEntity(usi);
                    return Content("yes:" + fulldir);
                }
                else
                {
                    
                    return Content("no:文件类型错误，文件扩展名错误！");
                }
                #endregion
            }
            else
            {
                return Content("no:请上传图片文件");
            }

            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);//获取上传的文件名
                string fileExt = Path.GetExtension(filename);//获取扩展名
                if (fileExt == ".jpg" || fileExt == ".png")
                {
                    string dir = "/MenuIcon/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                    string filenewName = Guid.NewGuid().ToString();
                    string fulldir = dir + filenewName + fileExt;
                    file.SaveAs(Request.MapPath(fulldir));
                    return Content("yes:" + fulldir);
                }
                else
                {
                    return Content("no:文件类型错误，文件扩展名错误！");
                }
            }
            else
            {
                return Content("no:请上传图片文件");
            }
        }
        #endregion

        //删除一张图片
        public ActionResult DelImage() {
            var id =Convert.ToInt64( Request["id"]);
            var Saimg = UserSaveImageService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (Saimg == null)
            { return Json(new { ret = "no" }, JsonRequestBehavior.AllowGet); }
            if (Saimg.T_UserSave.UserID == LoginUser.ID)
            {
                try
                {
                    if (Directory.Exists(Path.GetDirectoryName(Request.MapPath(Saimg.Image_str))))
                    {
                        System.IO.File.Delete(Request.MapPath(Saimg.Image_str));
                        if (UserSaveImageService.DeleteEntity(Saimg))
                        { return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet); }
                        else
                        { return Json(new { ret = "no", msg = "删除数据库信息时出错！" }, JsonRequestBehavior.AllowGet); }
                        
                    }
                    else
                    {
                        if (UserSaveImageService.DeleteEntity(Saimg))
                        { return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet); }
                        else
                        { return Json(new { ret = "no", msg = "删除数据库信息时出错！" }, JsonRequestBehavior.AllowGet); }
                       
                    }                   
                }
                catch (Exception e)
                {
                    return Json(new { ret = "no", msg = e.ToString() }, JsonRequestBehavior.AllowGet);                 
                }
            }
            else
            {
                return Json(new { ret = "noAction" }, JsonRequestBehavior.AllowGet);
            }
            

        }

    }
   
}

