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
    
    public partial class Klient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klient()
        {
            this.Faktury = new HashSet<Faktury>();
            this.LogData = new HashSet<LogData>();
            this.Samochody = new HashSet<Samochody>();
        }
    
        public int KlientID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Nip { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string KodPocztowy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faktury> Faktury { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogData> LogData { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Samochody> Samochody { get; set; }
    }
}
