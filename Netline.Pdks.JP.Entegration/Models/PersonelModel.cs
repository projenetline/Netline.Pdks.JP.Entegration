using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netline.Pdks.JP.Entegration.Models
{
    public class PersonelModel
    {


        /// <summary>
        /// Sicil Kodu
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// Personel Adı 
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Personel Durumu
        /// </summary>
        public string Status { get; set; } = "";
        /// <summary>
        /// Personel Soyadı
        /// </summary>
        public string Surname { get; set; } = "";
        /// <summary>
        /// İşe Giriş Tarihi
        /// </summary>
        public DateTime? Indate { get; set; }
        /// <summary>
        /// İşten Çıkış Tarihi
        /// </summary>
        public DateTime? OutDate { get; set; }
        /// <summary>
        /// Doğum Tarihi
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Tck No
        /// </summary>
        public string TckNo { get; set; } = "";
        /// <summary>
        /// E-Posra Adresi
        /// </summary>
        public string EMail { get; set; } = "";
        /// <summary>
        ///  Kurum
        /// </summary>
        public string Firm { get; set; } = "";
        /// <summary>
        /// İşYeri
        /// </summary>
        public string SubFirm { get; set; } = "";
        /// <summary>
        /// Birim
        /// </summary>
        public string Division { get; set; } = "";
        /// <summary>
        /// Departman
        /// </summary>
        public string Department { get; set; } = "";   
        /// <summary>
        /// Pozisyon
        /// </summary>
        public string Position { get; set; } = "";
        /// <summary>
        /// Yönetici Kodu
        /// </summary>
        public string ManagerCode { get; set; } = "";
        /// <summary>
        /// Yönetici Adı Soyadı
        /// </summary>
        public string Manager { get; set; } = "";
        /// <summary>
        /// Yönetici E-posta Adresş
        /// </summary>
        public string ManagerEMail { get; set; } = "";
        /// <summary>
        /// Ücret Tipi
        /// </summary>
        public int WageType { get; set; } = 0;
        /// <summary>
        /// Ücret
        /// </summary>
        public double Wage { get; set; } = 0;
        /// <summary>
        /// Personel Adresi
        /// </summary>
        public string Address { get; set; } = "";
        /// <summary>
        /// Personel Telefon Numarası
        /// </summary>
        public string PhoneNumber { get; set; } = "";
        /// <summary>
        /// Personel Yaka
        /// </summary>
        public string RollCode { get; set; } = "";
        /// <summary>
        /// Cinsiyet
        /// </summary>
        public string Gender { get; set; } = "";
        /// <summary>
        /// Kan Grubu
        /// </summary>
        public string BloodType { get; set; } = "";


        /// <summary>
        /// Resim
        /// </summary>
        public byte[] Picture { get; set; }

    }
}