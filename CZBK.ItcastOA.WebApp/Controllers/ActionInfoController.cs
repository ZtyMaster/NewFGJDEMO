using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class ActionInfoController : BaseController
    {
        //
        // GET: /ActionInfo/
        IBLL.IActionInfoService ActionInfoService  { get; set; }
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }


        public ActionResult Index()
        {
            return View();
        }
        #region 获取权限信息
        public ActionResult GetActionInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            bool Qx = Request["Qx"] != null ? Convert.ToBoolean(Request["Qx"]) : false;
            short delFlag = (short)DelFlagEnum.Normarl;
           var actioninfolist= ActionInfoService.LoadPageEntities<string>(pageIndex, pageSize, out totalCount, a => a.DelFlag == delFlag, a => a.Url, true);
            if (Qx)
            {
                actioninfolist = ActionInfoService.LoadPageEntities<string>(pageIndex, pageSize, out totalCount, a => a.DelFlag == delFlag&&a.ActionTypeEnum==1, a => a.Url, true);
            }
            var temp = from a in actioninfolist
                       select new
                       {
                           ID = a.ID,
                           ActionInfoName = a.ActionInfoName,
                           Sort = a.Sort,
                           Remark = a.Remark,
                           Url = a.Url,
                           HttpMethod = a.HttpMethod,
                           ActionTypeEnum = a.ActionTypeEnum,
                           SubTime = a.SubTime
                       };
            return Json(new { rows=temp,total=totalCount},JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 获取上传图片
        public ActionResult FileUpload()
        {
            HttpPostedFileBase file = Request.Files["fileIconUp"];
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);//获取上传的文件名
                string fileExt = Path.GetExtension(filename);//获取扩展名
                if (fileExt == ".jpg"|| fileExt == ".png")
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
        #region 展示要修改的角色数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);

            var roleInfo = ActionInfoService.LoadEntities(r => r.ID == id).FirstOrDefault();

            ActionInfo ai = new ActionInfo();
            ai.ActionInfoName = roleInfo.ActionInfoName;
            ai.Url = roleInfo.Url;
            ai.ID = roleInfo.ID;
            ai.HttpMethod = roleInfo.HttpMethod;
            ai.MenuIcon = roleInfo.MenuIcon;

            if (ai != null)
            {               
                return Content(Common.SerializerHelper.SerializeToString(new { serverData = ai, msg = "ok" }));
            }
            else
            {
                return Content(Common.SerializerHelper.SerializeToString(new { msg = "no" }));
            }
            
        }
        #endregion

        #region 完成角色修改
        public ActionResult EditRoleInfo(ActionInfo roleInfo)
        {
            var ai = ActionInfoService.LoadEntities(x => x.ID == roleInfo.ID).FirstOrDefault();
            ai.ActionInfoName = roleInfo.ActionInfoName;
            ai.Url = roleInfo.Url;
            ai.HttpMethod = roleInfo.HttpMethod;
            ai.MenuIcon = roleInfo.MenuIcon;
            
            if (ActionInfoService.EditEntity(ai))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #region 完成权限的添加
        public ActionResult AddActionInfo(ActionInfo actionInfo  )
        {
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now.ToShortTimeString();
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            
            ActionInfoService.AddEntity(actionInfo);
            return Content("ok");

        }
        #region 为权限分配角色
        public ActionResult SetActionRole()
        {
            int id = int.Parse(Request["id"]);//要分配角色的权限编号
            var actioninfo = ActionInfoService.LoadEntities(a => a.ID == id).FirstOrDefault();
            ViewBag.ActionInfo = actioninfo;
            //获取所有角色
            short delflag = (short)DelFlagEnum.Normarl;
            var AllRoleList = RoleInfoService.LoadEntities(a => a.DelFlag == delflag).ToList();
            //获取当前权限已经有的角色信息
            var AllExtRoleIdList = (from r in actioninfo.RoleInfo
                                    select r.ID).ToList();
            ViewBag.RoleList = AllRoleList;
            ViewBag.AllExtRoleIdList = AllExtRoleIdList;
            return View();
        }
        #endregion
        #region 完成对角色权限的分配
        public ActionResult SetActionRoleInfo()
        {
            int RoleinfoId = int.Parse(Request["actionId"]);
            List<int> list = new List<int>();
            string[] allkeys = Request.Form.AllKeys;//获取所有表单中Name属性的值
            foreach (string key in allkeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    list.Add(int.Parse(k));
                }
            }
            if (ActionInfoService.SetActionRoleInfo(RoleinfoId, list))
            {
                return Content("ok");
            }
            else {
                return Content("no");
            }

        }
        #endregion
        #endregion
        #region 获取主次冲突权限
        public ActionResult Setbool()
        {
            var temp = T_BoolItemService.LoadEntities(x => x.ID == 1).FirstOrDefault();
            return Content(temp.BOLL_.ToString());            

        }
        public ActionResult btnbool()
        {
            var temp = T_BoolItemService.LoadEntities(x => x.ID == 1).FirstOrDefault();
            temp.BOLL_ = temp.BOLL_ == true ? false : true;
            T_BoolItemService.EditEntity(temp);
            return Content(temp.BOLL_.ToString());

        }
        #endregion


    }
}
