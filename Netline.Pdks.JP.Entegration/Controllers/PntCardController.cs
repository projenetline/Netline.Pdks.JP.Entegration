using Netline.Pdks.JP.BusinessLayer;
using Netline.Pdks.JP.Entegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Netline.Pdks.JP.Entegration.Controllers
{
    public class PntCardController 
    {

        /// <summary>
        /// Puantaj gönderme metotudur....
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("api/PntCard/postPntCard")]
        public TransferResult postPntCard(PntCard pntCard)
        {
            ProjectUtil util= new ProjectUtil();
            DateTime activePeriodDate=util.getPntPeriodControl();
            TransferResult result= new TransferResult();
            if (activePeriodDate.Month == pntCard.PntMonth && activePeriodDate.Year == pntCard.PntYear)
            {
                Ntl_PntTransfer pntTransfer = new Ntl_PntTransfer()
                {
                    GTatGun=pntCard.GTatGun,
                    GTatSaat=pntCard.GTatSaat,
                    Gun=pntCard.Gun,
                    HTatGun=pntCard.HTatGun,
                    HTatSaat=pntCard.HTatSaat,
                    PandemiUsizGun=pntCard.PandemiUsizGun,
                    PandemiUsizSaat=pntCard.PandemiUsizSaat,
                    PerCode=pntCard.PerCode,
                    PntMonth=pntCard.PntMonth,
                    PntYear=pntCard.PntYear,
                    Saat=pntCard.Saat,
                    SgkGun=pntCard.SgkGun,
                    UsizIzinGun=pntCard.UsizIzinGun,
                    UsizIzinSaat=pntCard.UsizIzinSaat,
                    YillikIzinGun=pntCard.YillikIzinGun,
                    YillikIzinSaat=pntCard.YillikIzinSaat,
                    Fzm1=pntCard.Fzm1,
                    BayramMesaisi=pntCard.BayramMesaisi,
                    IdariGun=pntCard.IdariGun,
                    IdariSaat=pntCard.IdariSaat,
                    IstrahatGun=pntCard.IstrahatGun,
                    IstrahatSaat=pntCard.IstrahatSaat,
                    SosyalIzinGun=pntCard.SosyalIzinGun,
                    SosyalIzinSaat=pntCard.SosyalIzinSaat,
                    ToplamCalismaGunu=pntCard.ToplamCalismaGunu,
                    VergiOdemeGunu=pntCard.VergiOdemeGunu,
                    KisaCalismaOdenegiGun=pntCard.KisaCalismaOdenegiGun,
                    KisaCalismaOdenegisaat=pntCard.KisaCalismaOdenegisaat
                };




                int id= util.getPntTransferId(pntCard.PerCode,pntCard.PntYear,pntCard.PntMonth);


                pntTransfer.Id = id;

                if (id > 0)
                    util.updatePntTransfer(pntTransfer);
                else
                    util.savePntTransfer(pntTransfer);



                string url = "http://152.141.100.45:8081/logo/services/PntCard?wsdl";
                string credid = "admin";
                string credpassword = "net_123";

                StringBuilder rawSOAP = new StringBuilder();
                rawSOAP.AppendLine("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:cus=\"http://customization.LogoPlatformTailor.com\">");
                rawSOAP.AppendLine(BuildSoapHeader(credid, credpassword));
                rawSOAP.AppendLine(@"<soapenv:Body><cus:upadatePntCard>");
                rawSOAP.AppendLine("<cus:firm>1</cus:firm><cus:period>1</cus:period><cus:language>TRTR</cus:language>");
                rawSOAP.AppendLine(@"</cus:upadatePntCard>");
                rawSOAP.AppendLine(@"</soapenv:Body>");
                rawSOAP.AppendLine(@"</soapenv:Envelope>");
                string SOAPObj = rawSOAP.ToString();
                using (var wb = new WebClient())
                {
                    wb.Credentials = new NetworkCredential(credid, credpassword);
                    var responseVal = wb.UploadString(url, "POST", SOAPObj);
                    result.Message = responseVal;


                }
            }
            else
            {

                result.Success = false;
                result.Message = " Aktif döneme ait puantaj gönderebilirsiiniz.Aktif dönem  " + activePeriodDate.Year + " yılının " + activePeriodDate.Month + ".  ayıdır ..";
            }


            return result;

        }

        private string BuildSoapHeader(string credid, string credpassword)
        {
            StringBuilder rawSOAP= new StringBuilder();


            rawSOAP.AppendLine(@"<soapenv:Header>");
            rawSOAP.AppendLine(@"<wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">");
            rawSOAP.AppendLine(@"<wsse:UsernameToken wsu:Id=""UsernameToken-D1A5C91F8C11FC7F2614479411111111"">");
            rawSOAP.AppendLine(@"<wsse:Username>" + credid + "</wsse:Username>");
            rawSOAP.AppendLine(@"<wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">" + credpassword + "</wsse:Password>");
            rawSOAP.AppendLine(@"</wsse:UsernameToken>");
            rawSOAP.AppendLine(@"</wsse:Security>");
            rawSOAP.AppendLine(@"</soapenv:Header>");
            return rawSOAP.ToString();
        }
    }
}
