//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tuoterekisteri.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Loan
    {
        public int loan_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<System.DateTime> loaned_date { get; set; }
        public Nullable<int> spec_id { get; set; }
    
        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual User User { get; set; }
    }
}
