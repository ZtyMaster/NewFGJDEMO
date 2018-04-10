using CZBK.ItcastOA.BLL;
using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.WebApp.Models;
using log4net;
using Spring.Context;
using Spring.Context.Support;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CZBK.ItcastOA.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SpringMvcApplication //System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string fileLogPath = Server.MapPath("/Log/");
            //WaitCallback
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                       Exception ex= MyExceptionAttribute.ExceptionQueue.Dequeue();//出队
                       //string fileName = DateTime.Now.ToString("yyyy-MM-dd")+".txt";
                       //File.AppendAllText(fileLogPath + fileName, ex.ToString(), System.Text.Encoding.Default);
                       ILog logger = LogManager.GetLogger("errorMsg");
                       logger.Error(ex.ToString());
                    }
                    else
                    {
                        Thread.Sleep(3000);//如果队列中没有数据，休息避免造成CPU的空转.
                    }
                }


            },fileLogPath);

            //System.Timers.Timer aTimer = new System.Timers.Timer(600*1000);
            //aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            //aTimer.Enabled = true;
            //aTimer.AutoReset = true;
            //GC.KeepAlive(aTimer);//关键点
        }

        public static DateTime GetT_time()
        {
            return DateTime.Now;
        }
        void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            //执行我要执行的事件
            IApplicationContext ctx = ContextRegistry.GetContext();
            IT_BoolItemService T_BoolItemService = (IT_BoolItemService)ctx.GetObject("T_BoolItemService");
            IT_SaveHtmlDataService t_savehtml = (IT_SaveHtmlDataService)ctx.GetObject("T_SaveHtmlDataService");
            //获取规定时长
            int Gint = (int)T_BoolItemService.LoadEntities(x => x.ID == 2).FirstOrDefault().@int;
            //获取要更新的数据
            var T_saverHtml = t_savehtml.LoadEntities(x => x.DelFlag == 0 && x.BiaoJiId == null && x.GongGong == 0).DefaultIfEmpty().ToList();
            for (int i = 0; i < T_saverHtml.Count(); i++)
            {
                if (T_saverHtml[i].SaveTime.AddDays(Gint)< GetT_time())
                {
                    T_saverHtml[i].GongGong = 1;
                    IT_SaveHtmlDataService TThtml = (IT_SaveHtmlDataService)ctx.GetObject("T_SaveHtmlDataService");
                    TThtml.EditEntity(T_saverHtml[i]);
                }
            }
        }
     

       
    }
}