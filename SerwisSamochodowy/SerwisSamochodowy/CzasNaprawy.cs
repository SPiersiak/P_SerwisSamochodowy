//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SerwisSamochodowy
{
    using System;
    using System.Collections.Generic;
    
    public partial class CzasNaprawy
    {
        public int NaprawaID { get; set; }
        public Nullable<System.DateTime> Od { get; set; }
        public Nullable<System.DateTime> Do { get; set; }
    
        public virtual Naprawy Naprawy { get; set; }
    }
}
