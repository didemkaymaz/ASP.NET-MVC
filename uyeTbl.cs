//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proje_a.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class uyeTbl
    {
        public int uye_id { get; set; }
        [Display(Name = " ")]
        public string uye_ad { get; set; }
        [Display(Name = " ")]
        public string uye_soyad { get; set; }
        [Display(Name = " ")]
        public string kullanici_adi { get; set; }
        [Display(Name = " ")]
        public string uye_sehir { get; set; }
        [Display(Name = "Tel No")]
        public int uye_telno { get; set; }
        [Display(Name = " ")]
        public string uye_mail { get; set; }
        [Display(Name = " ")]
        public string rol { get; set; }
        [Display(Name = " ")]
        public string uye_sifre { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}