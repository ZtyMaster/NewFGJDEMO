using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using CZBK.ItcastOA.Model.SearchParam;
using CZBK.ItcastOA.WebApp.Models;
using Ivony.Html;
using Ivony.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class HrefInfoController : BaseController
    {
        //
        // GET: /HerfInfo/
       
        IBLL.IT_FGJHtmlDataService T_FGJHtmlDataService { get; set; }
      
        IBLL.IUserInfo_CityService UserInfo_CityService { get; set; }
        IBLL.IT_SaveHtmlDataService T_SaveHtmlDataService { get; set; }
        IBLL.IT_ItemsService T_ItemsService { get; set; }
        IBLL.IT_CityService T_CityService { get; set; }


        IBLL.IActionInfoService ActionInfoService { get; set; }
       
        IBLL.IT_SeeClickPhotoService T_SeeClickPhotoService { get; set; }
        IBLL.IT_UserClickService T_UserClickService { get; set; }
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }
        IBLL.IT_ZhuaiJiaBakService T_ZhuaiJiaBakService { get; set; }
        IBLL.IScrherSAVEService ScrherSAVEService { get; set; }
        IBLL.IT_ScehMiShuService T_ScehMiShuService { get; set; }
        short Delflag = (short)DelFlagEnum.Normarl;

        public ActionResult Index()
        {
            var Items = T_ItemsService.LoadEntities(x => x.DelFlag == null).ToList();
            var ItemsCount = Items != null ? Items.Distinct(new FastPropertyComparer<T_Items>("Bakstr")).ToList() : null;
            ViewBag.AllClick = LoginUser.Click;
            // 获取点击次数           
            ViewBag.OnClic = UserInfoService.GetMaxClick(LoginUser.ID);
            ViewBag.City = UserInfo_CityService.LoadMyCity_Quyu(LoginUser.ID);
            ViewBag.Items = Items;
            ViewBag.ItemsCount = ItemsCount.Count;
            ViewBag.LMasterID = LoginUser.MasterID;
            return View();
        }
        public ActionResult GetQuyu()
        {
            var City = UserInfo_CityService.LoadMyCity_Quyu(LoginUser.ID);
            var Quyu = from a in City
                       select new {
                           id=a.QY_connet,
                           text=a.QY_connet
                       };
            return Json(Quyu , JsonRequestBehavior.AllowGet);
        }      
        #region 获取网站信息
        public ActionResult GetHref()
        {


            var CityID = UserInfo_CityService.LoadEntities(x => x.UserInfo_ID == LoginUser.ID).FirstOrDefault(); 
            int pageIndex = Request["page"]!=null? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) :35;

            bool Strb = Request["boot_mp"]!=null? Convert.ToBoolean(Request["boot_mp"]):false;
             
            if (Strb)
            {
                #region MyRegion
                var totalCount = int.MaxValue;
                UserInfoParam Uxms = new UserInfoParam();
                if (Request["XmsId"] != null)
                {
                    #region 小秘书搜索
                    int XmsID = Request["XmsId"] != null ? Convert.ToInt32(Request["XmsId"]) : 0;
                    var xms = ScrherSAVEService.LoadEntities(x => x.ID == XmsID && x.DEL == 0).FirstOrDefault();
                    addsechmishu(xms);
                    Uxms.PageIndex = pageIndex;
                    Uxms.PageSize = pageSize;
                    Uxms.TotalCount = totalCount;
                    Uxms.ssve = xms;
                    Uxms.C_id = CityID.T_City_ID;

                    #endregion
                }
                else {
                    #region 正常搜索
                    Uxms.PageIndex = pageIndex;
                    Uxms.PageSize = pageSize;
                    Uxms.TotalCount = totalCount;
                    Uxms.Mon1 = Request["Mon1"] != null ? Request["Mon1"].Trim().Length > 0 ? int.Parse(Request["Mon1"]) : 0 : 0;
                    Uxms.Mon2 = Request["Mon2"] != null ? Request["Mon2"].Trim().Length > 0 ? int.Parse(Request["Mon2"]) : 999999 : 999999;
                    Uxms.Pm1 = Request["Pm1"] != null ? Request["Pm1"].Trim().Length > 0 ? int.Parse(Request["Pm1"]) : 0 : 0;
                    Uxms.Pm2 = Request["Pm2"] != null ? Request["Pm2"].Trim().Length > 0 ? int.Parse(Request["Pm2"]) : 99999999 : 99999999;
                    Uxms.quyu = Request["SeQuyu"].ToString();
                    Uxms.C_id = CityID.T_City_ID;
                    #endregion
                }                

                var actioninfolist = T_FGJHtmlDataService.LoadSearchPM(Uxms);
                var UscrClick = T_SeeClickPhotoService.LoadEntities(x => x.UserID == LoginUser.ID).DefaultIfEmpty();
                var temp = from a in actioninfolist
                           select new
                           {
                               ID = a.ID,
                               HLName = a.HLName,
                               Image_str = (a.Image_str.IndexOf("有") >= 0 ? "有" : "无"),
                               FbTime = a.FbTime,
                               PersonName = a.PersonName,
                               Address = a.Address,
                               photo = UscrClick.Where(x => x.T_FgjID == a.ID).FirstOrDefault() != null ? a.photo : string.Empty,
                               Laiyuan = a.Laiyuan,
                               FwSumMoney = a.FwSumMoney,
                               FwHuXing = a.FwHuXing,
                               FwLoucheng = a.FwLoucheng,
                               FwZhuangxiu = a.FwZhuangxiu,
                               FwChaoxiang = a.FwChaoxiang,
                               FwNianxian = a.FwNianxian,
                               FwMianji = a.FwMianji
                           };
                return Json(new { rows = temp, total = Uxms.TotalCount }, JsonRequestBehavior.AllowGet);
                #endregion

            }
            else
            {
                string Str = Request["Str"];
                var totalCount = int.MaxValue;
                //构建搜索条件          
                UserInfoParam userInfoParam = new UserInfoParam()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    Str = Str,
                    C_id = CityID.T_City_ID
                };
                if (Request["Tval"] != null)
                {
                    #region MyRegion
                    string str = Request["Tval"];
                    string[] list = str.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        string[] ti = list[i].Split('>');
                        int cc = int.Parse(ti[0]);
                        var this_i = T_ItemsService.LoadEntities(x => x.ID == cc).FirstOrDefault();
                        switch (this_i.Icons)
                        {
                            case 0:
                                userInfoParam.money = ti[1].Trim() == "0" ? null : this_i.StrID.ToString();
                                break;
                            case 1:
                                userInfoParam.Pingmu = ti[1].Trim() == "0" ? null : this_i.StrID.ToString();
                                break;
                            case 2:
                                userInfoParam.Tingshi = ti[1].Trim() == "0" ? null : this_i.StrID.ToString();
                                break;
                            case 3:
                                userInfoParam.Zhuanxiu = ti[1].Trim() == "0" ? null : ti[1].ToString().Trim() == "装修" ? null : ti[1];
                                break;
                        }
                    }
                    #endregion
                }
                var temp = GetJson(userInfoParam);

                return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
            }
           

        }
        public ActionResult GetMap()
        {
           
            ViewBag.Address = Request["Address"].ToString();
            ViewBag.City = T_CityService.LoadEntities(x => x.ID == LoginUser.CityID).FirstOrDefault().City;
            return View();
        }
        public object GetJson(UserInfoParam userInfoParam)
        {
            var actioninfolist = T_FGJHtmlDataService.LoadSearchFrist(userInfoParam,true,true);
            #region 基础信息进行查询
            if (!string.IsNullOrEmpty(userInfoParam.Zhuanxiu))
            {
                actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.FwZhuangxiu.Contains(userInfoParam.Zhuanxiu));
            }
            if (!string.IsNullOrEmpty(userInfoParam.Pingmu) && userInfoParam.Pingmu.Trim() != "0")
            {
                int thiid = int.Parse(userInfoParam.Pingmu);

                actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.MianjiID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoParam.money) && userInfoParam.money.Trim() != "0")
            {
                int thiid = int.Parse(userInfoParam.money);

                actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.SumMoneyID == thiid);
            }
            if (!string.IsNullOrEmpty(userInfoParam.Tingshi) && userInfoParam.Tingshi.Trim() != "0")
            {
                int thiid = int.Parse(userInfoParam.Tingshi);

                actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.HuXingID == thiid);
            }
            #endregion
            #region 根据详细信息进行查询
            //if (!string.IsNullOrEmpty(userInfoParam.Mon1.ToString()))
            //{
            //    int thiid = int.Parse(userInfoParam.Tingshi);

            //    actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.HuXingID == thiid);
            //}
            //if (!string.IsNullOrEmpty(userInfoParam.Mon2.ToString()))
            //{
            //    int thiid = int.Parse(userInfoParam.Tingshi);

            //    actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.HuXingID == thiid);
            //}
            //if (!string.IsNullOrEmpty(userInfoParam.Pm1.ToString()))
            //{
            //    int thiid = int.Parse(userInfoParam.Tingshi);

            //    actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.HuXingID == thiid);
            //}
            //if (!string.IsNullOrEmpty(userInfoParam.Pm.ToString()))
            //{
            //    int thiid = int.Parse(userInfoParam.Tingshi);

            //    actioninfolist = actioninfolist.Where<T_FGJHtmlData>(u => u.HuXingID == thiid);
            //}
            #endregion

            var UscrClick = T_SeeClickPhotoService.LoadEntities(x=>x.UserID==LoginUser.ID).DefaultIfEmpty();
            var temp = from a in actioninfolist
                       select new
                       {
                           ID = a.ID,
                           HLName = a.HLName,
                           Image_str = (a.Image_str.IndexOf("有") >= 0 ? "有" : "无"),
                           FbTime = a.FbTime,
                           PersonName = a.PersonName,
                           Address = a.Address,
                           photo =  UscrClick.Where(x=>x.T_FgjID==a.ID).FirstOrDefault()!=null ? a.photo : string.Empty ,
                           Laiyuan = a.Laiyuan,
                           FwSumMoney = a.FwSumMoney,
                           FwHuXing = a.FwHuXing,
                           FwLoucheng = a.FwLoucheng,
                           FwZhuangxiu = a.FwZhuangxiu,
                           FwChaoxiang = a.FwChaoxiang,
                           FwNianxian = a.FwNianxian,
                           FwMianji = a.FwMianji
                       };
            return temp;
        }
        #endregion
        #region 保存网站信息
        public ActionResult SaveHref()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            String rt = string.Empty;
            try
            {
                foreach (string id in strIds)
                {
                    T_SaveHtmlData savedata = new T_SaveHtmlData();
                    savedata.HtmldataID = long.Parse(id);
                    savedata.SaveTime = DateTime.Now;
                    savedata.UserID = LoginUser.ID;
                    savedata.GongGong = Delflag;
                    //保存的时候进行主号标记
                    savedata.MasterID = Convert.ToBoolean(LoginUser.ThisMastr) ? LoginUser.ID : LoginUser.MasterID;
                    long ids = long.Parse(id);

                    if (T_SaveHtmlDataService.LoadEntities(x => x.HtmldataID == ids && x.UserID == LoginUser.ID).Count() <= 0)
                    { T_SaveHtmlDataService.AddEntity(savedata); }

                }
                rt = "ok";
            }
            catch (Exception ex)
            {
                rt = ex.ToString();
            }
            return Content(rt);

        }
        #endregion

        #region 查看电话方法
        public ActionResult SeePhoto()
        {
            long id = long.Parse(Request["strId"]);
            //检查用户点击中是否有该用户
            var Uclick = T_UserClickService.LoadEntities(x => x.UserInfoId == LoginUser.ID).FirstOrDefault();
            //检查是否点击过查看电话
            var SeeClick = T_SeeClickPhotoService.LoadEntities(x => x.UserID == LoginUser.ID && x.T_FgjID == id).FirstOrDefault();

            if (UserInfoService.GetMaxClick(LoginUser.ID)>=LoginUser.Click)
            {
                return Content(Common.SerializerHelper.SerializeToString(new { msg = "此用户账户查看量已上限！" }));
            }
            if (SeeClick != null)
            {
                return Content(Common.SerializerHelper.SerializeToString(new { msg = "用户查看过该条信息" }));
                //  Content("用户没有点击过该信息，数据库确有这条信息，数据库BUG");
            }
            else
            {
                #region MyRegion
                //是否开启与主号保存冲突
                var t_bool = T_BoolItemService.LoadEntities(x => x.ID == 1).FirstOrDefault();

                //检查当前要查询的信息是否被当前主号下其他小号保存
                if (Convert.ToBoolean(LoginUser.ThisMastr))
                {
                    if (t_bool != null)
                    {
                        if (t_bool.BOLL_)
                        {
                            string temp = GetSelectSmallSave(LoginUser.ID, id);
                            if (temp != "on")
                            {
                                return Content(Common.SerializerHelper.SerializeToString(new { msg = "其他成员已保存该信息,保存人员为【" + temp + "】" }));
                            }
                        }
                    }
                    //当前点击是主号
                    SeeClickPhoto(Uclick, id);
                }
                else
                {
                    if (t_bool != null)
                    {
                        if (t_bool.BOLL_)
                        {
                            var mastr_save = T_SeeClickPhotoService.LoadEntities(x => x.UserID == LoginUser.MasterID && x.T_FgjID == id).FirstOrDefault();
                            if (mastr_save != null)
                            {
                                return Content(Common.SerializerHelper.SerializeToString(new { msg = "该信息已被主号保存！其他人员不可保存!" }));
                            }

                        }
                    }
                    string temp = GetSelectSmallSave(LoginUser.MasterID, id);
                    if (temp != "on")
                    {
                        return Content(Common.SerializerHelper.SerializeToString(new { msg = "其他成员已保存该信息,保存人员为【" + temp + "】" }));
                    }
                    else
                    {
                        string str = SeeClickPhoto(Uclick, id);
                        if (str != "OK")
                        {
                            return Content(Common.SerializerHelper.SerializeToString(new { msg = str }));
                        }

                    }
                }
                #endregion
            }

            // return Content("ok," + (Uclick == null ? 1.ToString() : Uclick.ThisClick.ToString()));1
            //int retUclick = Uclick == null ? 1 :Convert.ToInt32( Uclick.ThisClick);
            return Content(Common.SerializerHelper.SerializeToString(new { Uclick = UserInfoService.GetMaxClick(LoginUser.ID), msg = "ok" , MtrId = LoginUser.MasterID }));

        }
        private string GetSelectSmallSave(int? masterID ,long id)
        {
            var UMaster = UserInfoService.LoadEntities(x => x.MasterID == masterID).DefaultIfEmpty();
            var temp = from a in UMaster
                       from v in a.T_SeeClickPhoto
                       where v.T_FgjID == id
                       select new
                       {
                           ID = v.ID,
                           Name = v.UserInfo.UName
                       };
            var tt = temp.ToArray();
            string str = temp.Count()>0? tt[0].Name:"on";
            return str;
        }
       
        private string SeeClickPhoto(T_UserClick Uclick, long id)
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
            T_SeeClickPhoto tsc = new T_SeeClickPhoto();
            tsc.UserID = LoginUser.ID;
            tsc.T_FgjID = id;
            T_SeeClickPhotoService.AddEntity(tsc);
            Common.MemcacheHelper.SetClickMCH(LoginUser.ID.ToString());
            return "OK";
        }
        #endregion
        //查看电话方法
        #region 查看图片
        public ActionResult SeeImage()
        {
            int id = int.Parse(Request["id"]);
            var temp = T_FGJHtmlDataService.LoadEntities(x => x.ID == id).FirstOrDefault();
            string imageSTR = temp.Image_str;
            string Masimage = imageSTR.Replace("有---", string.Empty);
            Masimage = Masimage.Replace("w=242&h=150&", "w=700&h=480&");
            if (temp != null)
            {
                return Content(Common.SerializerHelper.SerializeToString(new { serverData = Masimage, msg = "ok" }));
            }
            else
            {
                return Content(Common.SerializerHelper.SerializeToString(new { msg = "no" }));
            }
        }
        #endregion
        
        public Class1[] GetHref_(int page)
        {

            System.Threading.Thread.Sleep(1 * 1000);

            string URL = "http://liaoyang.58.com/ershoufang/0";          
            Class1[] RTc = new Class1[160];
            URL = page == 1 ? URL : URL + "/pn" + page.ToString() + "/";
            //抓取关键字对应的url
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            
            string html = client.DownloadString(URL);
            IHtmlDocument document = new JumonyParser().Parse(html);
            GetUrlText_1(RTc, document);
            int b = 0;
            b = GetCount(RTc, b);
            Class1[] rtcc = new Class1[b];
            for (int a = 0; a < rtcc.Length; a++)
            {
                rtcc[a] = RTc[a];
            }
            return rtcc;
        }
        public ActionResult Go_Pages()
        {
          
            return null; 
        }
       
        private void GetUrlText_1(Class1[] RTc, IHtmlDocument document)
        {
            IEnumerable<IHtmlElement> result = document.Find("div").Where(d => d.Identity() == "main");
            IEnumerable<IHtmlElement> t = result.Find("tr");
            Dictionary<string, string> dir = new Dictionary<string, string>();
            int r = 0;
            foreach (var item in t)
            {
                #region MyRegion
                Class1 _class = new Class1();
                IHtmlElement item_a = item.FindFirst(".bthead>a");
                _class.TextName = item_a.InnerText().Trim();
                _class.href = item_a.Attribute("href").Value().Trim();
                _class.Quyu = GetN_value(item, ".a_xq1");
                string st = GetN_value(item, ".qj-lijjrname");
                _class.PersonName = st.Replace("：", "").Trim(); ;
                _class.Laiyuan = GetN_value(item, "label");
                string[] str_ = GetN_value(item, ".qj-listleft").Split(' ');
                List<string> str_1 = LIST_G(str_);
                _class.Address = str_1[2];
                //_class.SumMoney = item.FindFirst(".pri").InnerText();
                string[] ssp = GetN_value(item, ".qj-listright").Split(' ');
                int j = ssp.Length == 10 ? 0 : 10 - ssp.Length;
                _class.SumMoney = ssp[1 - j] + ssp[2 - j];
                _class.PingMoney = ssp[4 - j];
                _class.Allpm = ssp[7 - j] + ssp[9 - j];
                string[] datetime = GetN_value(item, ".qj-listjjr").Split(' ');
                _class.datetime = datetime.Length == 3 ? datetime[2] : "";
                RTc[r] = _class;
                r++;
                #endregion
            }
        }
        private static List<string> LIST_G(string[] str_)
        {
            //string[] ist = null;
            List<string> ist = new List<string>();

            foreach (var s in str_)
            {
                if (s != "")
                { ist.Add(s); }
            }
            return ist;
        }
        private static string GetN_value(IHtmlElement item, string str)
        {
            return item.Exists(str) ? item.FindFirst(str).InnerText().Trim() : string.Empty;
        }
        private static int GetCount(Class1[] RTc, int b)
        {
            for (int a = 0; a < RTc.Length; a++)
            {
                if (RTc[a] == null)
                {
                    b = a;
                    return b;
                }
            }
            return b;

        }

        //追加备注信息
        public ActionResult AddZhuiJia()
        {
            long? id = int.Parse(Request["PID"]);
            string bak = Request["Bak"];
            var zjbak= T_ZhuaiJiaBakService.LoadEntities(x=>x.FGJHTML_id==id).FirstOrDefault(); 
            //如果空那么新增信息
            if (zjbak == null)
            {

                T_ZhuaiJiaBak newT = new T_ZhuaiJiaBak();
                newT.ADDtime = MvcApplication.GetT_time();
                newT.AddUser = LoginUser.ID;
                newT.BAK = bak;
                newT.DEL = 0;
                newT.FGJHTML_id = id;
                T_ZhuaiJiaBakService.AddEntity(newT);
                return Json(new { ret = "ok", msg = "添加成功！" }, JsonRequestBehavior.AllowGet);

            }
            else {
                zjbak.ADDtime = MvcApplication.GetT_time();
                zjbak.BAK = bak;
                if (T_ZhuaiJiaBakService.EditEntity(zjbak))
                {
                    return Json(new { ret = "ok", msg = "修改成功！" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { ret = "no", msg = "修改失败，请联系管理员！" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
        //获取追加的备注信息
        public ActionResult GetZhuiJiaBak() {
            long? id = int.Parse(Request["PID"]);
            var rtzjbak= T_ZhuaiJiaBakService.LoadEntities(x => x.FGJHTML_id == id).FirstOrDefault();
            
            return Json(rtzjbak==null?"": rtzjbak.BAK, JsonRequestBehavior.AllowGet);
        }
        //搜索小秘书  load
        public ActionResult Getsech() {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;
            var TotalCount = 0;
            var mp = ScrherSAVEService.LoadPageEntities(pageIndex, pageSize,out TotalCount, y => y.DEL == 0&&y.addUserID==LoginUser.ID, x => x.Addtime, false);
            var titem=  T_ItemsService.LoadEntities(x => x.Icons ==2);
            List<seach> lss = new List<seach>();
            foreach (var a in mp) {
                seach seh = new seach();
                seh.ID = a.ID;
                seh.Addtime = a.Addtime;
                seh.CName = a.CName;
                seh.Srtmm = a.Srtmm;
                seh.Stpmm = a.Stpmm;
                seh.Stpmoney = a.Stpmoney;
                seh.Strmoney = a.Strmoney;
                seh.Quyu = a.Quyu;
                seh.HxID = a.HxID;
                seh.Zhuangxiu = a.Zhuangxiu;
                seh.hxstr = a.HxID == null ? "" : titem.Where(m => m.StrID == a.HxID).FirstOrDefault().Str;
                seh.maxcount = GetSeachdata(a.ID, pageIndex, pageSize);
                lss.Add(seh);
            }
            lss = lss.OrderByDescending(x => x.Addtime).ToList();
            return Json(new { rows = lss, total = TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteseach() {
            var strId = Convert.ToInt64( Request["strId"]);
            var Deltemp = ScrherSAVEService.LoadEntities(x => x.ID == strId).FirstOrDefault();

            if (ScrherSAVEService.DeleteEntity(Deltemp)) {
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("no", JsonRequestBehavior.AllowGet);

        }
        //搜索小秘书  新增数据
        public ActionResult addseach(ScrherSAVE ssave) {

            ssave.addUserID = LoginUser.ID;
            ssave.DEL = 0;
            if (ssave.ID > 0)
            {
                //修改
                ssave.Edittime = MvcApplication.GetT_time();

                bool bl = ScrherSAVEService.EditEntity(ssave);
                if (bl) {
                    addsechmishu(ssave);
                }
                return Json("okedit", JsonRequestBehavior.AllowGet);
            }
            else { 
                //新增
                ssave.Addtime=MvcApplication.GetT_time();
                ssave = ScrherSAVEService.AddEntity(ssave);
                addsechmishu(ssave);
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
           


        }
        private void addsechmishu(ScrherSAVE ssave) {
           int sco= GetSeachdata(ssave.ID, 1, 40);
            T_ScehMiShu sms = new T_ScehMiShu();
            sms.UserId = LoginUser.ID;
            sms.ScehCount = sco;
           
            sms.ScrherSAVEId = ssave.ID;
            var fsc=T_ScehMiShuService.LoadEntities(x => x.ScrherSAVEId == ssave.ID && x.UserId == LoginUser.ID).FirstOrDefault();
            if (fsc!= null)
            {
                fsc.ScehCount = sco;
                fsc.ScehTime = ssave.Edittime == null? (DateTime)ssave.Addtime:(DateTime)ssave.Edittime;
                T_ScehMiShuService.EditEntity(fsc);
            }
            else {
                sms.ScehTime = (DateTime)ssave.Addtime;
                T_ScehMiShuService.AddEntity(sms);
            }
            
        }

        public int GetSeachdata(long XmsID, int pageIndex, int pageSize ) {
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
    }
    
}
