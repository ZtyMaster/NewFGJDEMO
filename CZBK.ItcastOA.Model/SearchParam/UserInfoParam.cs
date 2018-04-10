using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.Model.SearchParam
{
   public class UserInfoParam:BaseParam
    {
       public string UserName { get; set; }
       public string Remark { get; set; }
        public string Str { get; set; }
        public int C_id { get; set; }
        public string money { get; set; }
        public string Tingshi { get; set; }
        public string Pingmu { get; set; }
        public string Zhuanxiu { get; set; }
        public string zhgl { get; set; }
        public object Tval { get; set; }
        public string Items { get; set; }
        public bool IsMaster { get; set; }
        public int MasterID { get; set; }

        public int Mon1 { get; set; }
        public int Mon2 { get; set; }
        public int Pm1 { get; set; }
        public int Pm2 { get; set; }
        public string quyu { get; set; }

        public int? CityID { get; set; }
        public ScrherSAVE ssve { get; set; }

        public bool Isee { get; set; }

    }
}
