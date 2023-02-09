using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netline.Pdks.JP.Entegration.Models
{
    public class PersonelResult
    {

        /// <summary>
        /// Sicil Kodu
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// Personel Adı 
        /// </summary>
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime? Indate { get; set; }
        public DateTime? OutDate { get; set; } 
        public DateTime? BirthDate { get; set; }
        public string TckNo { get; set; } = "";
        public string EMail { get; set; } = "";
        public string Firm { get; set; } = "";
        public string SubFirm { get; set; } = "";
        public string Division { get; set; } = "";
        public string Position { get; set; } = "";
        public string Department { get; set; } = "";    
        public string ManagerCode { get; set; } = "";
        public string Manager { get; set; } = "";
        public string ManagerEMail { get; set; } = "";
        public int WageType { get; set; } = 0;
        public double Wage { get; set; } = 0;
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string RollCode { get; set; } = "";
        public string Gender { get; set; } = "";
        public string BloodType { get; set; } = "";
        public byte[] Picture { get; set; } 

        

    }
}

