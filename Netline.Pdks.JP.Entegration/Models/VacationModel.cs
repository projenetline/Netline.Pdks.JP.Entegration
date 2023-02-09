using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netline.Pdks.JP.Entegration.Models
{
    public class VacationModel
    {   /// <summary>
        /// İzin Id
        /// </summary>
        public int VacationId { get; set; } = 0;

        /// <summary>
        /// İzin Hareket Durumu 1 : İzin Hareketi  2 : İzin  Talep
        /// </summary>
        public int VacTransType { get; set; } = 0;


        /// <summary>
        /// Personel Sicil KOdu
        /// </summary>
        public string PersonelCode { get; set; } = "";
        /// <summary>
        /// İzin Türü
        /// </summary>
        public string VacationType { get; set; } = "";
        /// <summary>
        /// İzin Kodu
        /// </summary>
        public string VacationCode { get; set; } = "";
        /// <summary>
        /// İzin Başlangıç Tarihi
        /// </summary>
    
        public DateTime? BegDate { get; set; }
        /// <summary>
        /// İzin Bitiş Tarihi
        /// </summary>
    
        public DateTime? EndDate { get; set; }
      
         /// <summary>
        /// İşe DÖnüş Tarihi
        /// </summary>
        public DateTime? ReturnDate { get; set; }
      
        /// <summary>
        /// İzin Tipi (Ücretli / Ücretsiz)
        /// </summary>
        public string WageType { get; set; } = "";
       
        /// 
        /// <summary>
        /// İzin SÜre Türü (Gün /Saat )
        /// </summary>
        /// 
        public string DurationType { get; set; } = "";

        /// <summary>
        /// İzin Kayıt Durumu   I :Insert , D :Delete(İptal) , U :Update
        /// </summary>        
        public string RecordStatus { get; set; } = "";
        /// <summary>
        /// İzin AÇıklaması
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// Son Değişiklik Yapan
        /// </summary>
        public string ModifierName { get; set; } = "";
        /// <summary>
        /// İzin SÜresi
        /// </summary>
        public double Duration { get; set; } = 0;
    }
}