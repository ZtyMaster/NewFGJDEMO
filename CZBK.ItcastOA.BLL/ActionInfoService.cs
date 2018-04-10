using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.BLL
{
    public partial class ActionInfoService:BaseService<ActionInfo>, IActionInfoService
    {
        /// <summary>
        /// 为权限分配角色
        /// </summary>
        /// <param name="actionId">权限编号</param>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        public bool SetActionRoleInfo(int actionId, List<int> roleIdList)
        {
            //根据权限编号找到权限信息
            var actionInfo = this.GetCurrentDbSession.ActionInfoDal.LoadEntities(a => a.ID == actionId).FirstOrDefault();
            if (actionInfo != null)//如果权限信息不为空
            {
                actionInfo.RoleInfo.Clear();//现有的权限已有的角色信息删除  actionInfo.RoleInfo导航属性
                foreach (int roleid in roleIdList)//遍历
                {
                    //每遍历一次找到 角色
                    var roleinfo = this.GetCurrentDbSession.RoleInfoDal.LoadEntities(r => r.ID == roleid).FirstOrDefault();
                    //把遍历倒的角色添加倒权限中
                    actionInfo.RoleInfo.Add(roleinfo);
                }
               //最后执行保存
                return this.GetCurrentDbSession.SaveChanges();
            }
            else
            {
                return false;
            }

        }
    }
}
