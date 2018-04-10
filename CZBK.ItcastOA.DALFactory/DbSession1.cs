 

using CZBK.ItcastOA.DAL;
using CZBK.ItcastOA.IDAL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                   // _ActionInfoDal = new ActionInfoDal();
				    _ActionInfoDal =AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IBumenInfoSetDal _BumenInfoSetDal;
        public IBumenInfoSetDal BumenInfoSetDal
        {
            get
            {
                if(_BumenInfoSetDal == null)
                {
                   // _BumenInfoSetDal = new BumenInfoSetDal();
				    _BumenInfoSetDal =AbstractFactory.CreateBumenInfoSetDal();
                }
                return _BumenInfoSetDal;
            }
            set { _BumenInfoSetDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                   // _DepartmentDal = new DepartmentDal();
				    _DepartmentDal =AbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IGongGaoDal _GongGaoDal;
        public IGongGaoDal GongGaoDal
        {
            get
            {
                if(_GongGaoDal == null)
                {
                   // _GongGaoDal = new GongGaoDal();
				    _GongGaoDal =AbstractFactory.CreateGongGaoDal();
                }
                return _GongGaoDal;
            }
            set { _GongGaoDal = value; }
        }
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if(_R_UserInfo_ActionInfoDal == null)
                {
                   // _R_UserInfo_ActionInfoDal = new R_UserInfo_ActionInfoDal();
				    _R_UserInfo_ActionInfoDal =AbstractFactory.CreateR_UserInfo_ActionInfoDal();
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set { _R_UserInfo_ActionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                   // _RoleInfoDal = new RoleInfoDal();
				    _RoleInfoDal =AbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private IScrherSAVEDal _ScrherSAVEDal;
        public IScrherSAVEDal ScrherSAVEDal
        {
            get
            {
                if(_ScrherSAVEDal == null)
                {
                   // _ScrherSAVEDal = new ScrherSAVEDal();
				    _ScrherSAVEDal =AbstractFactory.CreateScrherSAVEDal();
                }
                return _ScrherSAVEDal;
            }
            set { _ScrherSAVEDal = value; }
        }
	
		private ISeeQzCzDal _SeeQzCzDal;
        public ISeeQzCzDal SeeQzCzDal
        {
            get
            {
                if(_SeeQzCzDal == null)
                {
                   // _SeeQzCzDal = new SeeQzCzDal();
				    _SeeQzCzDal =AbstractFactory.CreateSeeQzCzDal();
                }
                return _SeeQzCzDal;
            }
            set { _SeeQzCzDal = value; }
        }
	
		private IT_AppOpenDal _T_AppOpenDal;
        public IT_AppOpenDal T_AppOpenDal
        {
            get
            {
                if(_T_AppOpenDal == null)
                {
                   // _T_AppOpenDal = new T_AppOpenDal();
				    _T_AppOpenDal =AbstractFactory.CreateT_AppOpenDal();
                }
                return _T_AppOpenDal;
            }
            set { _T_AppOpenDal = value; }
        }
	
		private IT_BiaoJiInfoDal _T_BiaoJiInfoDal;
        public IT_BiaoJiInfoDal T_BiaoJiInfoDal
        {
            get
            {
                if(_T_BiaoJiInfoDal == null)
                {
                   // _T_BiaoJiInfoDal = new T_BiaoJiInfoDal();
				    _T_BiaoJiInfoDal =AbstractFactory.CreateT_BiaoJiInfoDal();
                }
                return _T_BiaoJiInfoDal;
            }
            set { _T_BiaoJiInfoDal = value; }
        }
	
		private IT_BoolItemDal _T_BoolItemDal;
        public IT_BoolItemDal T_BoolItemDal
        {
            get
            {
                if(_T_BoolItemDal == null)
                {
                   // _T_BoolItemDal = new T_BoolItemDal();
				    _T_BoolItemDal =AbstractFactory.CreateT_BoolItemDal();
                }
                return _T_BoolItemDal;
            }
            set { _T_BoolItemDal = value; }
        }
	
		private IT_ChuZhuInfoDal _T_ChuZhuInfoDal;
        public IT_ChuZhuInfoDal T_ChuZhuInfoDal
        {
            get
            {
                if(_T_ChuZhuInfoDal == null)
                {
                   // _T_ChuZhuInfoDal = new T_ChuZhuInfoDal();
				    _T_ChuZhuInfoDal =AbstractFactory.CreateT_ChuZhuInfoDal();
                }
                return _T_ChuZhuInfoDal;
            }
            set { _T_ChuZhuInfoDal = value; }
        }
	
		private IT_CityDal _T_CityDal;
        public IT_CityDal T_CityDal
        {
            get
            {
                if(_T_CityDal == null)
                {
                   // _T_CityDal = new T_CityDal();
				    _T_CityDal =AbstractFactory.CreateT_CityDal();
                }
                return _T_CityDal;
            }
            set { _T_CityDal = value; }
        }
	
		private IT_FGJHtmlDataDal _T_FGJHtmlDataDal;
        public IT_FGJHtmlDataDal T_FGJHtmlDataDal
        {
            get
            {
                if(_T_FGJHtmlDataDal == null)
                {
                   // _T_FGJHtmlDataDal = new T_FGJHtmlDataDal();
				    _T_FGJHtmlDataDal =AbstractFactory.CreateT_FGJHtmlDataDal();
                }
                return _T_FGJHtmlDataDal;
            }
            set { _T_FGJHtmlDataDal = value; }
        }
	
		private IT_ItemsDal _T_ItemsDal;
        public IT_ItemsDal T_ItemsDal
        {
            get
            {
                if(_T_ItemsDal == null)
                {
                   // _T_ItemsDal = new T_ItemsDal();
				    _T_ItemsDal =AbstractFactory.CreateT_ItemsDal();
                }
                return _T_ItemsDal;
            }
            set { _T_ItemsDal = value; }
        }
	
		private IT_provinceDal _T_provinceDal;
        public IT_provinceDal T_provinceDal
        {
            get
            {
                if(_T_provinceDal == null)
                {
                   // _T_provinceDal = new T_provinceDal();
				    _T_provinceDal =AbstractFactory.CreateT_provinceDal();
                }
                return _T_provinceDal;
            }
            set { _T_provinceDal = value; }
        }
	
		private IT_QiuZhuQiuGouDal _T_QiuZhuQiuGouDal;
        public IT_QiuZhuQiuGouDal T_QiuZhuQiuGouDal
        {
            get
            {
                if(_T_QiuZhuQiuGouDal == null)
                {
                   // _T_QiuZhuQiuGouDal = new T_QiuZhuQiuGouDal();
				    _T_QiuZhuQiuGouDal =AbstractFactory.CreateT_QiuZhuQiuGouDal();
                }
                return _T_QiuZhuQiuGouDal;
            }
            set { _T_QiuZhuQiuGouDal = value; }
        }
	
		private IT_QuyuDal _T_QuyuDal;
        public IT_QuyuDal T_QuyuDal
        {
            get
            {
                if(_T_QuyuDal == null)
                {
                   // _T_QuyuDal = new T_QuyuDal();
				    _T_QuyuDal =AbstractFactory.CreateT_QuyuDal();
                }
                return _T_QuyuDal;
            }
            set { _T_QuyuDal = value; }
        }
	
		private IT_SaveHtmlDataDal _T_SaveHtmlDataDal;
        public IT_SaveHtmlDataDal T_SaveHtmlDataDal
        {
            get
            {
                if(_T_SaveHtmlDataDal == null)
                {
                   // _T_SaveHtmlDataDal = new T_SaveHtmlDataDal();
				    _T_SaveHtmlDataDal =AbstractFactory.CreateT_SaveHtmlDataDal();
                }
                return _T_SaveHtmlDataDal;
            }
            set { _T_SaveHtmlDataDal = value; }
        }
	
		private IT_ScehMiShuDal _T_ScehMiShuDal;
        public IT_ScehMiShuDal T_ScehMiShuDal
        {
            get
            {
                if(_T_ScehMiShuDal == null)
                {
                   // _T_ScehMiShuDal = new T_ScehMiShuDal();
				    _T_ScehMiShuDal =AbstractFactory.CreateT_ScehMiShuDal();
                }
                return _T_ScehMiShuDal;
            }
            set { _T_ScehMiShuDal = value; }
        }
	
		private IT_SeeClickPhotoDal _T_SeeClickPhotoDal;
        public IT_SeeClickPhotoDal T_SeeClickPhotoDal
        {
            get
            {
                if(_T_SeeClickPhotoDal == null)
                {
                   // _T_SeeClickPhotoDal = new T_SeeClickPhotoDal();
				    _T_SeeClickPhotoDal =AbstractFactory.CreateT_SeeClickPhotoDal();
                }
                return _T_SeeClickPhotoDal;
            }
            set { _T_SeeClickPhotoDal = value; }
        }
	
		private IT_UpHerfCityDal _T_UpHerfCityDal;
        public IT_UpHerfCityDal T_UpHerfCityDal
        {
            get
            {
                if(_T_UpHerfCityDal == null)
                {
                   // _T_UpHerfCityDal = new T_UpHerfCityDal();
				    _T_UpHerfCityDal =AbstractFactory.CreateT_UpHerfCityDal();
                }
                return _T_UpHerfCityDal;
            }
            set { _T_UpHerfCityDal = value; }
        }
	
		private IT_UserClickDal _T_UserClickDal;
        public IT_UserClickDal T_UserClickDal
        {
            get
            {
                if(_T_UserClickDal == null)
                {
                   // _T_UserClickDal = new T_UserClickDal();
				    _T_UserClickDal =AbstractFactory.CreateT_UserClickDal();
                }
                return _T_UserClickDal;
            }
            set { _T_UserClickDal = value; }
        }
	
		private IT_UserSaveDal _T_UserSaveDal;
        public IT_UserSaveDal T_UserSaveDal
        {
            get
            {
                if(_T_UserSaveDal == null)
                {
                   // _T_UserSaveDal = new T_UserSaveDal();
				    _T_UserSaveDal =AbstractFactory.CreateT_UserSaveDal();
                }
                return _T_UserSaveDal;
            }
            set { _T_UserSaveDal = value; }
        }
	
		private IT_UserSave_ItemsDal _T_UserSave_ItemsDal;
        public IT_UserSave_ItemsDal T_UserSave_ItemsDal
        {
            get
            {
                if(_T_UserSave_ItemsDal == null)
                {
                   // _T_UserSave_ItemsDal = new T_UserSave_ItemsDal();
				    _T_UserSave_ItemsDal =AbstractFactory.CreateT_UserSave_ItemsDal();
                }
                return _T_UserSave_ItemsDal;
            }
            set { _T_UserSave_ItemsDal = value; }
        }
	
		private IT_YxPersonDal _T_YxPersonDal;
        public IT_YxPersonDal T_YxPersonDal
        {
            get
            {
                if(_T_YxPersonDal == null)
                {
                   // _T_YxPersonDal = new T_YxPersonDal();
				    _T_YxPersonDal =AbstractFactory.CreateT_YxPersonDal();
                }
                return _T_YxPersonDal;
            }
            set { _T_YxPersonDal = value; }
        }
	
		private IT_ZhuaiJiaBakDal _T_ZhuaiJiaBakDal;
        public IT_ZhuaiJiaBakDal T_ZhuaiJiaBakDal
        {
            get
            {
                if(_T_ZhuaiJiaBakDal == null)
                {
                   // _T_ZhuaiJiaBakDal = new T_ZhuaiJiaBakDal();
				    _T_ZhuaiJiaBakDal =AbstractFactory.CreateT_ZhuaiJiaBakDal();
                }
                return _T_ZhuaiJiaBakDal;
            }
            set { _T_ZhuaiJiaBakDal = value; }
        }
	
		private ITLoginbakDal _TLoginbakDal;
        public ITLoginbakDal TLoginbakDal
        {
            get
            {
                if(_TLoginbakDal == null)
                {
                   // _TLoginbakDal = new TLoginbakDal();
				    _TLoginbakDal =AbstractFactory.CreateTLoginbakDal();
                }
                return _TLoginbakDal;
            }
            set { _TLoginbakDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                   // _UserInfoDal = new UserInfoDal();
				    _UserInfoDal =AbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	
		private IUserInfo_CityDal _UserInfo_CityDal;
        public IUserInfo_CityDal UserInfo_CityDal
        {
            get
            {
                if(_UserInfo_CityDal == null)
                {
                   // _UserInfo_CityDal = new UserInfo_CityDal();
				    _UserInfo_CityDal =AbstractFactory.CreateUserInfo_CityDal();
                }
                return _UserInfo_CityDal;
            }
            set { _UserInfo_CityDal = value; }
        }
	
		private IUserSaveImageDal _UserSaveImageDal;
        public IUserSaveImageDal UserSaveImageDal
        {
            get
            {
                if(_UserSaveImageDal == null)
                {
                   // _UserSaveImageDal = new UserSaveImageDal();
				    _UserSaveImageDal =AbstractFactory.CreateUserSaveImageDal();
                }
                return _UserSaveImageDal;
            }
            set { _UserSaveImageDal = value; }
        }
	
		private IWxUserDal _WxUserDal;
        public IWxUserDal WxUserDal
        {
            get
            {
                if(_WxUserDal == null)
                {
                   // _WxUserDal = new WxUserDal();
				    _WxUserDal =AbstractFactory.CreateWxUserDal();
                }
                return _WxUserDal;
            }
            set { _WxUserDal = value; }
        }
	}	
}