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
    
    public partial class Czesci
    {
        public int CzescID { get; set; }
        public int NaprawaID { get; set; }
        public string Nazwa { get; set; }
        public Nullable<decimal> Cena { get; set; }
    
        public virtual Naprawy Naprawy { get; set; }
    }
}