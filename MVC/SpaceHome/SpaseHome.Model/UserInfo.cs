//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpaseHome.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.Gender = 0;
            this.MaritalStatus = "保密";
            this.Address = "北京";
            this.BirthPlace = "北京";
        }
    
        public System.Guid UserId { get; set; }
        public string Nick { get; set; }
        public Nullable<byte> Gender { get; set; }
        public Nullable<System.DateTime> BrithDay { get; set; }
        public string Sign { get; set; }
        public string BloodType { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public string HeadImg { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<int> Age { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
