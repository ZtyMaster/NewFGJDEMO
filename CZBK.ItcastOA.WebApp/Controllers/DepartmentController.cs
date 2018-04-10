using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CZBK.ItcastOA.Model;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class DepartmentController : BaseController
    {
        //
        // GET: /Department/
        IBLL.IDepartmentService DepartmentService { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        //获取部门数据
        public ActionResult GetDepartment()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            //构建搜索条件
            //int totalCount = 0;
            //UserInfoParam userInfoParam = new UserInfoParam()
            //{

            //    PageIndex = pageIndex,
            //    PageSize = pageSize,
            //    TotalCount = totalCount,
            //    UserName = name,
            //    Remark = remark
            //};
            short delFlag = (short)DelFlagEnum.Normarl;
            int totalCount;


            var Departmet = DepartmentService.LoadPageEntities(pageIndex, pageSize, out totalCount,m=>m.DelFlag==delFlag, a => a.ID, true);

            var temp = from u in Departmet
                       select new { ID = u.ID, DepName = u.DepName, IsLeaf = u.IsLeaf, Level = u.Level, ParentId = u.ParentId };

            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #region 角色部门
        public ActionResult AddDepartment(Department department)
        {
            department.DelFlag = 0;
            DepartmentService.AddEntity(department);
            return Content("ok");

        }
        #endregion

    }
}
