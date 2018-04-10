 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IBumenInfoSetDal BumenInfoSetDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IGongGaoDal GongGaoDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		IScrherSAVEDal ScrherSAVEDal{get;set;}
	
		ISeeQzCzDal SeeQzCzDal{get;set;}
	
		IT_AppOpenDal T_AppOpenDal{get;set;}
	
		IT_BiaoJiInfoDal T_BiaoJiInfoDal{get;set;}
	
		IT_BoolItemDal T_BoolItemDal{get;set;}
	
		IT_ChuZhuInfoDal T_ChuZhuInfoDal{get;set;}
	
		IT_CityDal T_CityDal{get;set;}
	
		IT_FGJHtmlDataDal T_FGJHtmlDataDal{get;set;}
	
		IT_ItemsDal T_ItemsDal{get;set;}
	
		IT_provinceDal T_provinceDal{get;set;}
	
		IT_QiuZhuQiuGouDal T_QiuZhuQiuGouDal{get;set;}
	
		IT_QuyuDal T_QuyuDal{get;set;}
	
		IT_SaveHtmlDataDal T_SaveHtmlDataDal{get;set;}
	
		IT_ScehMiShuDal T_ScehMiShuDal{get;set;}
	
		IT_SeeClickPhotoDal T_SeeClickPhotoDal{get;set;}
	
		IT_UpHerfCityDal T_UpHerfCityDal{get;set;}
	
		IT_UserClickDal T_UserClickDal{get;set;}
	
		IT_UserSaveDal T_UserSaveDal{get;set;}
	
		IT_UserSave_ItemsDal T_UserSave_ItemsDal{get;set;}
	
		IT_YxPersonDal T_YxPersonDal{get;set;}
	
		IT_ZhuaiJiaBakDal T_ZhuaiJiaBakDal{get;set;}
	
		ITLoginbakDal TLoginbakDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	
		IUserInfo_CityDal UserInfo_CityDal{get;set;}
	
		IUserSaveImageDal UserSaveImageDal{get;set;}
	
		IWxUserDal WxUserDal{get;set;}
	}	
}