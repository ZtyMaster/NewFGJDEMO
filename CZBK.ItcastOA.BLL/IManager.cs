 
using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.ActionInfoDal;
        }
    }   
	
	public partial class BumenInfoSetService :BaseService<BumenInfoSet>,IBumenInfoSetService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.BumenInfoSetDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.DepartmentDal;
        }
    }   
	
	public partial class GongGaoService :BaseService<GongGao>,IGongGaoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.GongGaoDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.RoleInfoDal;
        }
    }   
	
	public partial class ScrherSAVEService :BaseService<ScrherSAVE>,IScrherSAVEService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.ScrherSAVEDal;
        }
    }   
	
	public partial class SeeQzCzService :BaseService<SeeQzCz>,ISeeQzCzService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.SeeQzCzDal;
        }
    }   
	
	public partial class T_AppOpenService :BaseService<T_AppOpen>,IT_AppOpenService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_AppOpenDal;
        }
    }   
	
	public partial class T_BiaoJiInfoService :BaseService<T_BiaoJiInfo>,IT_BiaoJiInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_BiaoJiInfoDal;
        }
    }   
	
	public partial class T_BoolItemService :BaseService<T_BoolItem>,IT_BoolItemService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_BoolItemDal;
        }
    }   
	
	public partial class T_ChuZhuInfoService :BaseService<T_ChuZhuInfo>,IT_ChuZhuInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_ChuZhuInfoDal;
        }
    }   
	
	public partial class T_CityService :BaseService<T_City>,IT_CityService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_CityDal;
        }
    }   
	
	public partial class T_FGJHtmlDataService :BaseService<T_FGJHtmlData>,IT_FGJHtmlDataService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_FGJHtmlDataDal;
        }
    }   
	
	public partial class T_ItemsService :BaseService<T_Items>,IT_ItemsService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_ItemsDal;
        }
    }   
	
	public partial class T_provinceService :BaseService<T_province>,IT_provinceService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_provinceDal;
        }
    }   
	
	public partial class T_QiuZhuQiuGouService :BaseService<T_QiuZhuQiuGou>,IT_QiuZhuQiuGouService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_QiuZhuQiuGouDal;
        }
    }   
	
	public partial class T_QuyuService :BaseService<T_Quyu>,IT_QuyuService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_QuyuDal;
        }
    }   
	
	public partial class T_SaveHtmlDataService :BaseService<T_SaveHtmlData>,IT_SaveHtmlDataService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_SaveHtmlDataDal;
        }
    }   
	
	public partial class T_ScehMiShuService :BaseService<T_ScehMiShu>,IT_ScehMiShuService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_ScehMiShuDal;
        }
    }   
	
	public partial class T_SeeClickPhotoService :BaseService<T_SeeClickPhoto>,IT_SeeClickPhotoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_SeeClickPhotoDal;
        }
    }   
	
	public partial class T_UpHerfCityService :BaseService<T_UpHerfCity>,IT_UpHerfCityService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_UpHerfCityDal;
        }
    }   
	
	public partial class T_UserClickService :BaseService<T_UserClick>,IT_UserClickService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_UserClickDal;
        }
    }   
	
	public partial class T_UserSaveService :BaseService<T_UserSave>,IT_UserSaveService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_UserSaveDal;
        }
    }   
	
	public partial class T_UserSave_ItemsService :BaseService<T_UserSave_Items>,IT_UserSave_ItemsService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_UserSave_ItemsDal;
        }
    }   
	
	public partial class T_YxPersonService :BaseService<T_YxPerson>,IT_YxPersonService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_YxPersonDal;
        }
    }   
	
	public partial class T_ZhuaiJiaBakService :BaseService<T_ZhuaiJiaBak>,IT_ZhuaiJiaBakService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_ZhuaiJiaBakDal;
        }
    }   
	
	public partial class TLoginbakService :BaseService<TLoginbak>,ITLoginbakService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.TLoginbakDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.UserInfoDal;
        }
    }   
	
	public partial class UserInfo_CityService :BaseService<UserInfo_City>,IUserInfo_CityService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.UserInfo_CityDal;
        }
    }   
	
	public partial class UserSaveImageService :BaseService<UserSaveImage>,IUserSaveImageService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.UserSaveImageDal;
        }
    }   
	
	public partial class WxUserService :BaseService<WxUser>,IWxUserService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.WxUserDal;
        }
    }   
	
}