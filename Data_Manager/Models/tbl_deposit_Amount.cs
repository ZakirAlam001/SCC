//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data_Manager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_deposit_Amount
    {
        public long CashDepositId { get; set; }
        public string DepositType { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CreateBy { get; set; }
        public string DML_Type { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IsDelete { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<int> Org_Id { get; set; }
    }
}
