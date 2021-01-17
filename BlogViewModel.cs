using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proje_a.Models.EntityFramework;
using proje_a.Models.Component;

namespace proje_a.ViewModel
{
    public class BlogViewModel
    {
        public int article_id { get; set; }
        public string article_title{ get; set; }
        public string article { get; set; }
        public List<blogTbl> articleList { get; set; }
        public List<blogTbl> icerikDetay { get; set; }

    }
}