 

using CZBK.ItcastOA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IActionInfoDal CreateActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".ActionInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IActionInfoDal;
        }
		
	    public static IBumenInfoSetDal CreateBumenInfoSetDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".BumenInfoSetDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IBumenInfoSetDal;
        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".DepartmentDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IDepartmentDal;
        }
		
	    public static IGongGaoDal CreateGongGaoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".GongGaoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IGongGaoDal;
        }
		
	    public static IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".R_UserInfo_ActionInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IR_UserInfo_ActionInfoDal;
        }
		
	    public static IRoleInfoDal CreateRoleInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".RoleInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IRoleInfoDal;
        }
		
	    public static IScrherSAVEDal CreateScrherSAVEDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".ScrherSAVEDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IScrherSAVEDal;
        }
		
	    public static ISeeQzCzDal CreateSeeQzCzDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".SeeQzCzDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as ISeeQzCzDal;
        }
		
	    public static IT_AppOpenDal CreateT_AppOpenDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_AppOpenDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_AppOpenDal;
        }
		
	    public static IT_BiaoJiInfoDal CreateT_BiaoJiInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_BiaoJiInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_BiaoJiInfoDal;
        }
		
	    public static IT_BoolItemDal CreateT_BoolItemDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_BoolItemDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_BoolItemDal;
        }
		
	    public static IT_ChuZhuInfoDal CreateT_ChuZhuInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_ChuZhuInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_ChuZhuInfoDal;
        }
		
	    public static IT_CityDal CreateT_CityDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_CityDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_CityDal;
        }
		
	    public static IT_FGJHtmlDataDal CreateT_FGJHtmlDataDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_FGJHtmlDataDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_FGJHtmlDataDal;
        }
		
	    public static IT_ItemsDal CreateT_ItemsDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_ItemsDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_ItemsDal;
        }
		
	    public static IT_provinceDal CreateT_provinceDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_provinceDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_provinceDal;
        }
		
	    public static IT_QiuZhuQiuGouDal CreateT_QiuZhuQiuGouDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_QiuZhuQiuGouDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_QiuZhuQiuGouDal;
        }
		
	    public static IT_QuyuDal CreateT_QuyuDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_QuyuDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_QuyuDal;
        }
		
	    public static IT_SaveHtmlDataDal CreateT_SaveHtmlDataDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_SaveHtmlDataDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_SaveHtmlDataDal;
        }
		
	    public static IT_ScehMiShuDal CreateT_ScehMiShuDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_ScehMiShuDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_ScehMiShuDal;
        }
		
	    public static IT_SeeClickPhotoDal CreateT_SeeClickPhotoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_SeeClickPhotoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_SeeClickPhotoDal;
        }
		
	    public static IT_UpHerfCityDal CreateT_UpHerfCityDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_UpHerfCityDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_UpHerfCityDal;
        }
		
	    public static IT_UserClickDal CreateT_UserClickDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_UserClickDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_UserClickDal;
        }
		
	    public static IT_UserSaveDal CreateT_UserSaveDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_UserSaveDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_UserSaveDal;
        }
		
	    public static IT_UserSave_ItemsDal CreateT_UserSave_ItemsDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_UserSave_ItemsDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_UserSave_ItemsDal;
        }
		
	    public static IT_YxPersonDal CreateT_YxPersonDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_YxPersonDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_YxPersonDal;
        }
		
	    public static IT_ZhuaiJiaBakDal CreateT_ZhuaiJiaBakDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".T_ZhuaiJiaBakDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IT_ZhuaiJiaBakDal;
        }
		
	    public static ITLoginbakDal CreateTLoginbakDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".TLoginbakDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as ITLoginbakDal;
        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".UserInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IUserInfoDal;
        }
		
	    public static IUserInfo_CityDal CreateUserInfo_CityDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".UserInfo_CityDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IUserInfo_CityDal;
        }
		
	    public static IUserSaveImageDal CreateUserSaveImageDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".UserSaveImageDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IUserSaveImageDal;
        }
		
	    public static IWxUserDal CreateWxUserDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["NameSpace"] + ".WxUserDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssemblyPath"], classFulleName);


            return obj as IWxUserDal;
        }
	}
	
}