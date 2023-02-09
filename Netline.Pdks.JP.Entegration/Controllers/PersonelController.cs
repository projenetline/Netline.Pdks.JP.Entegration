using Netline.Pdks.JP.BusinessLayer;
using Netline.Pdks.JP.Entegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Netline.Pdks.JP.Entegration.Controllers
{
    public class PersonelController : ApiController
    {
        /// <summary>
        /// Tc Kimlik  numarası ile Personel sorgulama metodudur.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("api/Personel/getPersonelByTckNo")]
        public PersonelModel getPersonelByTckNo(string TckNo)
        {
            ProjectUtil util = new ProjectUtil();
            var pers = util.getPersonelByTckNo(TckNo);

            PersonelModel personelModel = new PersonelModel()
            {
                BirthDate = pers.BirthDate,
                Code = pers.Code,
                Division = pers.Division,
                EMail = pers.EMail,
                Firm = pers.Firm,
                Indate = pers.Indate,
                Department = pers.Department,
                Manager = pers.Manager,
                ManagerCode = pers.ManagerCode,
                ManagerEMail = pers.ManagerEMail,
                Name = pers.Name,
                OutDate = pers.OutDate,
                Position = pers.Position,
                Status = pers.Status,
                SubFirm = pers.SubFirm,
                Surname = pers.Surname,

                TckNo = pers.TckNo,
                Wage = pers.Wage,
                WageType = pers.WageType,
                Address = pers.Address,
                BloodType = pers.BloodType,
                Gender = pers.Gender,
                PhoneNumber = pers.PhoneNumber,
                RollCode = pers.RollCode,
                Picture = pers.Picture



            };
            return personelModel;
        }
        /// <summary>
        /// Değişiklik olan Personel listeleme metodudur.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("api/Personel/getPersonels")]
        public List<PersonelModel> getPersonels()
        {
            ProjectUtil util = new ProjectUtil();
            var list = util.getPersonels();

            List<PersonelModel> personelModels = new List<PersonelModel>();
            foreach (var pers in list)
            {
                personelModels.Add(new PersonelModel()
                {
                    BirthDate = pers.BirthDate,
                    Code = pers.Code,
                    Division = pers.Division,
                    EMail = pers.EMail,
                    Firm = pers.Firm,
                    Indate = pers.Indate,
                    Department = pers.Department,
                    Manager = pers.Manager,
                    ManagerCode = pers.ManagerCode,
                    ManagerEMail = pers.ManagerEMail,
                    Name = pers.Name,
                    OutDate = pers.OutDate,
                    Position = pers.Position,
                    Status = pers.Status,
                    SubFirm = pers.SubFirm,
                    Surname = pers.Surname,
                    TckNo = pers.TckNo,
                    Wage = pers.Wage,
                    WageType = pers.WageType,
                    Address = pers.Address,
                    BloodType = pers.BloodType,
                    Gender = pers.Gender,
                    PhoneNumber = pers.PhoneNumber,
                    RollCode = pers.RollCode,
                     Picture=pers.Picture
                });
            }

            return personelModels;

        }

        /// <summary>
        /// Bütün Personeli listeleme metodudur.( Begdate=2020-08-01 , EndDate=2020-08-31 )
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("api/Personel/getPersonels")]
        public List<PersonelModel> getAllPersonels(string BegDate, string EndDate)
        {
            ProjectUtil util = new ProjectUtil();
            var list = util.getAllPersonels(BegDate, EndDate);
            List<PersonelModel> personelModels = new List<PersonelModel>();
            foreach (var pers in list)
            {
                personelModels.Add(new PersonelModel()
                {
                    BirthDate = pers.BirthDate,
                    Code = pers.Code,
                    Division = pers.Division,
                    EMail = pers.EMail,
                    Firm = pers.Firm,
                    Indate = pers.Indate,
                    Department = pers.Department,
                    Manager = pers.Manager,
                    ManagerCode = pers.ManagerCode,
                    ManagerEMail = pers.ManagerEMail,
                    Name = pers.Name,
                    OutDate = pers.OutDate,
                    Position = pers.Position,
                    Status = pers.Status,
                    SubFirm = pers.SubFirm,
                    Surname = pers.Surname,

                    TckNo = pers.TckNo,
                    Wage = pers.Wage,
                    WageType = pers.WageType,
                     Address = pers.Address,
                    BloodType = pers.BloodType,
                    Gender = pers.Gender,
                    PhoneNumber = pers.PhoneNumber,
                    RollCode = pers.RollCode,
                    Picture = pers.Picture


                });
            }

            return personelModels;

        }


        /// <summary>
        /// Personel izinleri listeleme metodudur.( Begdate=2020-08-01 , EndDate=2020-08-31 )
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("api/Personel/getVacations")]
        public List<VacationModel> getVacations(string BegDate, string EndDate)
        {
            ProjectUtil util = new ProjectUtil();
            var list = util.getVactrans(BegDate, EndDate);
            List<VacationModel> vacationModels = new List<VacationModel>();

            foreach (var item in list)
            {
                vacationModels.Add(new VacationModel()
                {
                    BegDate = item.BegDate,
                    Description = item.Description,
                    Duration = item.Duration,
                    DurationType = item.DurationType,
                    EndDate = item.EndDate,
                    ModifierName = item.ModifierName,
                    PersonelCode = item.PersonelCode,
                    ReturnDate = item.ReturnDate,
                    VacationCode = item.VacationCode,
                    VacationType = item.VacationType,
                    WageType = item.WageType,
                    RecordStatus = item.RecordStatus,
                    VacationId = item.VacationId,
                    VacTransType = item.VacTransType
                });
            }
            return vacationModels;
        }
    }
}
