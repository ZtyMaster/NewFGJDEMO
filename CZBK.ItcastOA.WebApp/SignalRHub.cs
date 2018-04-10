using CZBK.ItcastOA.Model;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CZBK.ItcastOA.WebApp
{
    public class SignalRHub : Hub
    {
        //声明静态变量存储当前在线用户
        public static class UserHandler
        {
            public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
        }

        //声明静态变量存储当前在线用户
        public static class UserLOGIN
        {
            public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
        }

        //获取网站登录人员的MasterID
        public void UserCted(string masterid)
        {
            masterid = HttpUtility.HtmlEncode(masterid);
            Clients.Others.addList(Context.ConnectionId, masterid);
            //保持的缓存中
            UserHandler.ConnectedIds.Add(Context.ConnectionId, masterid);
        }
        //发送信息给自定的MASTER
        public void SenMasterMsg(string masterID, string count)
        {
            count = HttpUtility.HtmlEncode(count);
            //Clients.Client(masterID).senMasterMsg(count);
            var MaxId = UserHandler.ConnectedIds.Where(p => p.Value == masterID).DefaultIfEmpty();
            foreach (var md in MaxId)
            {
                Clients.Client(md.Key).senMasterMsg(count);
            }
        }

        //登录home人员
        public void HomeCted(string masterid,string strip)
        {
            masterid = HttpUtility.HtmlEncode(masterid);
            var ucids= UserLOGIN.ConnectedIds.Where(x => x.Value == masterid).FirstOrDefault();
            if (ucids.Value== null) {

                IBLL.ITLoginbakService IIL = new BLL.TLoginbakService();
                TLoginbak ilb = new TLoginbak();
                ilb.LGUserID = Convert.ToInt32(masterid);
                ilb.intime = DateTime.Now;
                ilb.LGip = strip;
                IIL.AddEntity(ilb);
                Clients.Others.addList(Context.ConnectionId, masterid);
                UserLOGIN.ConnectedIds.Add(Context.ConnectionId, masterid);
            }
           
            //保持的缓存中
         
        }
        //--——————————————————————————————————————————————//
        //用户进入页面时执行的（连接操作）
        public void UserConnected(string name)
        {
            //进行编码，防止XSS攻击
            name = HttpUtility.HtmlEncode(name);
            string message = "用户 " + name + " 登录";

            //发送信息给其他人
            Clients.Others.addList(Context.ConnectionId, name);
            Clients.Others.hello(message);

            //发送信息给自己，并显示上线清单
            Clients.Caller.getList(UserHandler.ConnectedIds.Select(p => new { id = p.Key, name = p.Value }).ToList());

            //新增目前使用者上线清单
            UserHandler.ConnectedIds.Add(Context.ConnectionId, name);
        }
        
        //发送信息给所有人
        public void SendAllMessage(string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var name = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = name + "说：" + message;
            Clients.All.sendAllMessge(message);
        }


        //发送信息给特定人
        public void SendMessage(string ToId, string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var fromName = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = fromName + " <span style='color:red'>悄悄对你说</span>：" + message;
            Clients.Client(ToId).sendMessage(message);
        }

        //当使用者断线时执行
        public override Task OnDisconnected(bool stopCalled)
        {
            //当使用者离开时，移除在清单内的ConnectionId
            Clients.All.removeList(Context.ConnectionId);
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            UserLOGIN.ConnectedIds.Remove(Context.ConnectionId);
            //var uol = UserLOGIN.ConnectedIds.Where(x => x.Key == Context.ConnectionId).FirstOrDefault();
            //IBLL.ITLoginbakService IIL = new BLL.TLoginbakService();
            //var LGUserID = Convert.ToInt32(uol.Value);
            //var fuser= IIL.LoadEntities(x => x.ID == LGUserID).FirstOrDefault();
            //if (fuser != null) {
            //    fuser.outtime = DateTime.Now;
            //    IIL.EditEntity(fuser);
            //}            
            return base.OnDisconnected(stopCalled);
        }
    }
}