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
    using System.Web.Mvc;

    public partial class blogTbl
    {
        public int article_id { get; set; }
        [Display(Name = " ")]
        public string article_title { get; set; }
        [AllowHtml]
        [Display(Name = " ")]
        public string article { get; set; }
    }
}
