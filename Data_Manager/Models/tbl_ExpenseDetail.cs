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
    
    public partial class tbl_ExpenseDetail
    {
        public int ExpDetail_ID { get; set; }
        public Nullable<int> ExpMstID { get; set; }
        public string ExpType { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IsDelete { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string Description { get; set; }
        public Nullable<int> Org_Id { get; set; }
    
        public virtual tbl_MstExpense tbl_MstExpense { get; set; }
    }
}
