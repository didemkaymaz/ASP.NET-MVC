using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proje_a.Models.Component;
using proje_a.Models.EntityFramework;

namespace proje_a.ViewModel
{
    public class kayipTblViewModel
    {
        public int kayip_id { get; set; }
        public string kayip_ad { get; set; }
        public string kayip_turu { get; set; }
        public string kayip_cinsiyet { get; set; }
        public int kayip_yas { get; set; }
        public string kayip_renk { get; set; }
        public string kayip_il { get; set; }
        public string kayip_tarihi { get; set; }
        public string kayip_aciklama { get; set; }
        public string resim { get; set; }
        public int uye_id { get; set; }

        public DüzenlenecekKayit kayitDüzenle { get; set; } 
        public List<kayipListe> kayipList { get; set; }
        public List<kayipDetay> kayipDetay { get; set; }
        public List<kayipTbl> aramaList { get; set; }

    }

}