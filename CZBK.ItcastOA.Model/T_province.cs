//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CZBK.ItcastOA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_province
    {
        public T_province()
        {
            this.T_City = new HashSet<T_City>();
        }
    
        public int ID { get; set; }
        public string ppp { get; set; }
        public string str { get; set; }
    
        public virtual ICollection<T_City> T_City { get; set; }
    }
}