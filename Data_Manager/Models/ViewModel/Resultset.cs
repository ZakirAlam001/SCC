using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_Manager.Models.ViewModel
{
    public class Resultset
    {
        public int TrasactionID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public int ExpenseID { get; set; }
        public string ExpenseName { get; set; }
        public int SaleItemID { get; set; }
        public string SaleItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Wight { get; set; }
        public decimal Amount { get; set; }

        public decimal DR_Amount { get; set; }

        public decimal CR_Amount { get; set; }
        public string Date { get; set; }
       public string Expensetype { get; set; }
        public string Description { get; set; }
        public string Payment_Type { get; set; }
        

    }
}