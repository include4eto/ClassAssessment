//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassAssessment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assessments
    {
        public Assessments()
        {
            this.Answers = new HashSet<Answers>();
        }
    
        public int id { get; set; }
        public Nullable<int> From { get; set; }
        public Nullable<int> To { get; set; }
    
        public virtual ICollection<Answers> Answers { get; set; }
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
