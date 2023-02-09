using Netline.Pdks.JP.Entegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace Netline.Pdks.JP.BusinessLayer
{
    public class ProjectUtil
    {
        static  string sqlConn ="";
        static  string testsqlConn ="";
        public ProjectUtil()
        {
            sqlConn = ConfigurationManager.ConnectionStrings["APILoggingConnection"].ConnectionString;
            testsqlConn = ConfigurationManager.ConnectionStrings["TestAPILoggingConnection"].ConnectionString;
        }

        public List<PersonelResult> getAllPersonels(string BegDate, string EndDate)
        {
            DateTime begdate=Convert.ToDateTime(BegDate);
            DateTime enddate=Convert.ToDateTime(EndDate);
            string query = $@" SELECT 
                                CASE WHEN p.TYP = 1 then 'Aktive' else 'Pasive' end Status,
                                P.CODE Code,
                                P.NAME Name,
                                P.SURNAME Surname,
                                P.LOCINDATE Indate,
                                CASE WHEN p.TYP = 1 then null else  P.OUTDATE end OutDate,
                                P.BIRTHDATE BirthDate,
                                PR.IDTCNO TckNo,
                                C.EXP1 EMail,                                
                                KURUM.DESCRIPTION Firm,                                
                                ISYERI.DESCRIPTION SubFirm,                       
                                BOLUM.DESCRIPTION Division,
                                 POS.DESCRIPTION Department,
                                Q.DESCRIPTION Position,
                                MANAGERINFO.CODE ManagerCode,
                                MANAGERINFO.NAME+' '+MANAGERINFO.SURNAME Manager,
                                IDARICONTACT.EXP1 ManagerEMail  ,
                                    P.WAGE_OPTYPE WageType
                                ,case wheN P.LOCREF=2 THEN 0 ELSE P.WAGE_WAGE END Wage
                                ,R.LDATA AS Picture    
                                ,ADDRCONTACT.EXP1 +' '+ADDRCONTACT.EXP2 AS Address
								,TELCONTACT.EXP1 AS PhoneNumber
								,P.ROLLCODE AS RollCode
								,CASE WHEN P.SEX = 1 THEN 'ERKEK' WHEN P.SEX = 2 THEN 'KADIN' ELSE 'Kontrol Ediniz' END AS Gender 
								,CASE WHEN PR.BLOODGROUP = 0 THEN 'Boş' WHEN PR.BLOODGROUP = 1 THEN '0 RH +' WHEN PR.BLOODGROUP = 2 THEN '0 RH -' WHEN PR.BLOODGROUP = 3 THEN      
								'A RH +' WHEN PR.BLOODGROUP = 4 THEN 'A RH -' WHEN PR.BLOODGROUP = 5 THEN 'B RH +' WHEN PR.BLOODGROUP = 6 THEN 'B RH -' WHEN PR.BLOODGROUP = 7      
								 THEN 'AB RH +' WHEN PR.BLOODGROUP = 8 THEN 'AB RH -' ELSE 'KONTROL EDİNİZ' END AS BloodType
                                FROM H_001_PERSONS P 
                                LEFT JOIN H_001_COMPANYDOCS R ON R.INFOREF=P.LOGICALREF AND R.DOCTYPE=0 AND R.INFOTYPE=7
                                LEFT JOIN H_001_PERIDINFOS PR ON P.LOGICALREF=PR.CARDREF   AND PR.CARDTYP=1
                                LEFT JOIN H_001_CONTACTS C ON P.LOGICALREF=C.CARDREF AND C.TYP=6
                                LEFT JOIN S_ORGUNITS KURUM  ON P.FIRMREF=KURUM.LOGICALREF
                                LEFT JOIN S_ORGUNITS ISYERI  ON P.LOCREF=ISYERI.LOGICALREF
                                LEFT JOIN S_ORGUNITS BOLUM  ON P.UNITREF=BOLUM.LOGICALREF
                                 LEFT JOIN S_POSITIONS Q ON Q.LOGICALREF=P.POSITIONREF
                                LEFT JOIN S_DEPARTMENTS POS ON P.DEPTREF = POS.LOGICALREF
                                LEFT JOIN H_001_PERSONS MANAGERINFO ON P.MANAGERREF = MANAGERINFO.LOGICALREF
                                LEFT JOIN H_001_CONTACTS IDARICONTACT ON IDARICONTACT.CARDREF=MANAGERINFO.LOGICALREF AND IDARICONTACT.TYP=6
                                LEFT JOIN dbo.H_001_CONTACTS AS ADDRCONTACT  ON P.LOGICALREF = ADDRCONTACT.CARDREF AND ADDRCONTACT.TYP = 1 AND ADDRCONTACT.CARDTYP = 1    
								LEFT JOIN dbo.H_001_CONTACTS AS TELCONTACT  ON P.LOGICALREF = TELCONTACT.CARDREF AND TELCONTACT.TYP = 3 AND TELCONTACT.CARDTYP =1 
                                WHERE ( CASE WHEN p.TYP = 1 then null else  P.OUTDATE end  IS NULL OR P.INDATE >'2021-12-31'  or CASE WHEN p.TYP = 1 then null else  P.OUTDATE end >'2021-12-31' )  
                                and  (P.OUTDATE  between  @begdate and   @enddate or  P.INDATE  between  @begdate and   @enddate ) ";
            SqlConnection sql= new SqlConnection(sqlConn);
            List<PersonelResult> list=   sql.Query<PersonelResult>(query,new { begdate ,enddate}).ToList();
            return list;
        }
        public List<PersonelResult> getPersonels()
        {

            string query = $@" SELECT 
                                CASE WHEN p.TYP = 1 then 'Aktive' else 'Pasive' end Status,
                                P.CODE Code,
                                P.NAME Name,
                                P.SURNAME Surname,
                                P.LOCINDATE Indate,
                                CASE WHEN p.TYP = 1 then null else  P.OUTDATE end OutDate,
                                P.BIRTHDATE BirthDate,
                                PR.IDTCNO TckNo,
                                C.EXP1 EMail,                                
                                KURUM.DESCRIPTION Firm,                                
                                ISYERI.DESCRIPTION SubFirm,                       
                                BOLUM.DESCRIPTION Division,
                                POS.DESCRIPTION Department,
                                Q.DESCRIPTION Position,                              
                                MANAGERINFO.CODE ManagerCode,

                                MANAGERINFO.NAME+' '+MANAGERINFO.SURNAME Manager,
                                IDARICONTACT.EXP1 ManagerEMail  
                                ,P.WAGE_OPTYPE WageType
                                ,case wheN P.LOCREF=2 THEN 0 ELSE P.WAGE_WAGE END Wage 
                                ,R.LDATA AS Picture  
                                ,ADDRCONTACT.EXP1 +' '+ADDRCONTACT.EXP2 AS Address
								,TELCONTACT.EXP1 AS PhoneNumber
								,P.ROLLCODE AS RollCode
								,CASE WHEN P.SEX = 1 THEN 'ERKEK' WHEN P.SEX = 2 THEN 'KADIN' ELSE 'Kontrol Ediniz' END AS Gender 
								,CASE WHEN PR.BLOODGROUP = 0 THEN 'Boş' WHEN PR.BLOODGROUP = 1 THEN '0 RH +' WHEN PR.BLOODGROUP = 2 THEN '0 RH -' WHEN PR.BLOODGROUP = 3 THEN      
								'A RH +' WHEN PR.BLOODGROUP = 4 THEN 'A RH -' WHEN PR.BLOODGROUP = 5 THEN 'B RH +' WHEN PR.BLOODGROUP = 6 THEN 'B RH -' WHEN PR.BLOODGROUP = 7      
								 THEN 'AB RH +' WHEN PR.BLOODGROUP = 8 THEN 'AB RH -' ELSE 'KONTROL EDİNİZ' END AS BloodType
                                FROM H_001_PERSONS P 
                                LEFT JOIN H_001_COMPANYDOCS R ON R.INFOREF=P.LOGICALREF AND R.DOCTYPE=0 AND R.INFOTYPE=7
                                LEFT JOIN H_001_PERIDINFOS PR ON P.LOGICALREF=PR.CARDREF    AND PR.CARDTYP=1
                                LEFT JOIN H_001_CONTACTS C ON P.LOGICALREF=C.CARDREF AND C.TYP=6
                                LEFT JOIN S_ORGUNITS KURUM  ON P.FIRMREF=KURUM.LOGICALREF
                                LEFT JOIN S_ORGUNITS ISYERI  ON P.LOCREF=ISYERI.LOGICALREF
                                LEFT JOIN S_ORGUNITS BOLUM  ON P.UNITREF=BOLUM.LOGICALREF
                                LEFT JOIN S_POSITIONS Q ON Q.LOGICALREF=P.POSITIONREF
                                LEFT JOIN S_DEPARTMENTS POS ON P.DEPTREF = POS.LOGICALREF
                                LEFT JOIN H_001_PERSONS MANAGERINFO ON P.MANAGERREF = MANAGERINFO.LOGICALREF
                                LEFT JOIN H_001_CONTACTS IDARICONTACT ON IDARICONTACT.CARDREF=MANAGERINFO.LOGICALREF AND IDARICONTACT.TYP=6
                                LEFT JOIN dbo.H_001_CONTACTS AS ADDRCONTACT  ON P.LOGICALREF = ADDRCONTACT.CARDREF AND ADDRCONTACT.TYP = 1 AND ADDRCONTACT.CARDTYP = 1    
								LEFT JOIN dbo.H_001_CONTACTS AS TELCONTACT  ON P.LOGICALREF = TELCONTACT.CARDREF AND TELCONTACT.TYP = 3 AND TELCONTACT.CARDTYP =1   
                                WHERE CASE WHEN p.TYP = 1 then null else  P.OUTDATE end  IS NULL OR P.INDATE >'2021-12-31' or CASE WHEN p.TYP = 1 then null else  P.OUTDATE end >'2021-12-31'  ";
            SqlConnection sql= new SqlConnection(sqlConn);
            List<PersonelResult> list=   sql.Query<PersonelResult>(query).ToList();
            return list;
        }
        public PersonelResult getPersonelByTckNo(string Tckno)
        {
            SqlConnection sql= new SqlConnection();
            string query = $@" SELECT 
                                CASE WHEN p.TYP = 1 then 'Aktive' else 'Pasive' end Status,
                                P.CODE Code,
                                P.NAME Name,
                                P.SURNAME Surname,
                                P.LOCINDATE Indate,
                                CASE WHEN p.TYP = 1 then null else  P.OUTDATE end OutDate,
                                P.BIRTHDATE BirthDate,
                                PR.IDTCNO TckNo,
                                C.EXP1 EMail,                                
                                KURUM.DESCRIPTION Firm,                                
                                ISYERI.DESCRIPTION SubFirm,                       
                                BOLUM.DESCRIPTION Division,
                                  POS.DESCRIPTION Department,
                                Q.DESCRIPTION Position,
                               
                                MANAGERINFO.CODE ManagerCode,
                                MANAGERINFO.NAME+' '+MANAGERINFO.SURNAME Manager,
                                IDARICONTACT.EXP1 ManagerEMail ,
                                P.WAGE_OPTYPE WageType
                                ,case wheN P.LOCREF=2 THEN 0 ELSE P.WAGE_WAGE END Wage 
                                ,R.LDATA AS Picture  
                                ,ADDRCONTACT.EXP1 +' '+ADDRCONTACT.EXP2 AS Address
								,TELCONTACT.EXP1 AS PhoneNumber
								,P.ROLLCODE AS RollCode
								,CASE WHEN P.SEX = 1 THEN 'ERKEK' WHEN P.SEX = 2 THEN 'KADIN' ELSE 'Kontrol Ediniz' END AS Gender 
								,CASE WHEN PR.BLOODGROUP = 0 THEN 'Boş' WHEN PR.BLOODGROUP = 1 THEN '0 RH +' WHEN PR.BLOODGROUP = 2 THEN '0 RH -' WHEN PR.BLOODGROUP = 3 THEN      
								'A RH +' WHEN PR.BLOODGROUP = 4 THEN 'A RH -' WHEN PR.BLOODGROUP = 5 THEN 'B RH +' WHEN PR.BLOODGROUP = 6 THEN 'B RH -' WHEN PR.BLOODGROUP = 7      
								 THEN 'AB RH +' WHEN PR.BLOODGROUP = 8 THEN 'AB RH -' ELSE 'KONTROL EDİNİZ' END AS BloodType
                                FROM H_001_PERSONS P 
                                LEFT JOIN H_001_COMPANYDOCS R ON R.INFOREF=P.LOGICALREF AND R.DOCTYPE=0 AND R.INFOTYPE=7
                                LEFT JOIN H_001_PERIDINFOS PR ON P.LOGICALREF=PR.CARDREF    AND PR.CARDTYP=1
                                LEFT JOIN H_001_CONTACTS C ON P.LOGICALREF=C.CARDREF AND C.TYP=6
                                LEFT JOIN S_ORGUNITS KURUM  ON P.FIRMREF=KURUM.LOGICALREF
                                LEFT JOIN S_ORGUNITS ISYERI  ON P.LOCREF=ISYERI.LOGICALREF
                                LEFT JOIN S_ORGUNITS BOLUM  ON P.UNITREF=BOLUM.LOGICALREF
                                LEFT JOIN S_POSITIONS Q ON Q.LOGICALREF=P.POSITIONREF
                                LEFT JOIN S_DEPARTMENTS POS ON P.DEPTREF = POS.LOGICALREF
                                LEFT JOIN H_001_PERSONS MANAGERINFO ON P.MANAGERREF = MANAGERINFO.LOGICALREF
                                LEFT JOIN H_001_CONTACTS IDARICONTACT ON IDARICONTACT.CARDREF=MANAGERINFO.LOGICALREF AND IDARICONTACT.TYP=6
                                LEFT JOIN dbo.H_001_CONTACTS AS ADDRCONTACT  ON P.LOGICALREF = ADDRCONTACT.CARDREF AND ADDRCONTACT.TYP = 1 AND ADDRCONTACT.CARDTYP = 1    
								LEFT JOIN dbo.H_001_CONTACTS AS TELCONTACT  ON P.LOGICALREF = TELCONTACT.CARDREF AND TELCONTACT.TYP = 3 AND TELCONTACT.CARDTYP =1                                 
                                where PR.IDTCNO=@Tckno ";
            return sql.Query<PersonelResult>(query, new { Tckno }).FirstOrDefault();
        }
        public List<VacationResult> getVactrans(string BegDate, string EndDate)
        {
            DateTime begdate=Convert.ToDateTime(BegDate);
            DateTime enddate=Convert.ToDateTime(EndDate);
            string query=$@" SELECT                 	 
                                 P.CODE PersonelCode,
                                 VAC.LOGICALREF VacationId,                                                    
                                 VAC.PLANBEGDATE BegDate,
                                 VAC.PLANENDDATE EndDate,
                                 VAC.RETURNDATE ReturnDate,
                                 VAC.TRTYPE VacTransType,
                                 CASE
                                 WHEN PY.TRTYPE=2 OR  PY.TRTYPE=1 OR  PY.TRTYPE is null THEN 'ÜCRETLİ'
                                 WHEN PY.TRTYPE=3 THEN 'ÜCRETSİZ' END WageType,
                                 CASE
                                 WHEN VAC.PERIODTYPE =0 THEN 'GÜN'
                                 WHEN VAC.PERIODTYPE=1 THEN 'SAAT'
                                 END DurationType,
                                 CASE WHEN  VAC.TRTYPE=1 THEN VAC.PERIOD ELSE  VAC.PLANPERIOD END Duration,
                                 VCD.CODE VacationCode,
                                 VCD.DESCRIPTION Description,
                                 VAC.MODIFIEDBYNAME ModifierName,
								 case when VAC.BOSTATUS=2 THEN 'D' else	 CASE WHEN VAC.MODIFIEDBY=0 then 'I' else 'U' end	end  RecordStatus
                                 FROM H_001_VACTRANS VAC
                                 left JOIN H_001_PERSONS P ON VAC.PERREF=P.LOGICALREF
                                 left JOIN H_001_VACDEFS VCD ON VCD.LOGICALREF=VAC.VACDEFREF
                                 left JOIN H_001_PAYELEMS PY ON PY.NR=VCD.WORKELEMNR AND PY.TYP=1
                                 left JOIN S_USERS U ON U.LOGICALREF=VAC.MODIFIEDBY
                                 where vac.LOGICALREF NOT IN (SELECT REQUESTREF FROM H_001_VACTRANS)  
                                 and
                                 VAC.BOSTATUS<>2  and  VAC.PLANBEGDATE BETWEEN   @begdate and   @enddate
                                 AND
                                 VAC.LOGICALREF NOT IN (
                                 SELECT V2.LOGICALREF
                                 FROM
                                 ( SELECT * FROM  H_001_VACTRANS WHERE TRTYPE=1 AND BOSTATUS<>2 AND PLANBEGDATE BETWEEN   @begdate and   @enddate ) V1
                                 INNER JOIN ( SELECT * FROM  H_001_VACTRANS WHERE TRTYPE=2 AND BOSTATUS<>2 AND PLANBEGDATE BETWEEN   @begdate and   @enddate AND LOGICALREF NOT IN (SELECT REQUESTREF FROM H_001_VACTRANS)) V2
                                 ON  V1.PERREF=V2.PERREF  AND  (  CONVERT(DATE,V1.PLANBEGDATE) BETWEEN  CONVERT(DATE,V2.PLANBEGDATE)  AND   CONVERT(DATE,V2.PLANENDDATE) OR  CONVERT(DATE,V1.PLANENDDATE) BETWEEN  CONVERT(DATE,V2.PLANBEGDATE)  AND  CONVERT(DATE, V2.PLANENDDATE) )
                                )   ";
            SqlConnection sql= new SqlConnection(sqlConn);
            List<VacationResult> list=   sql.Query<VacationResult>(query,new { begdate,enddate}).ToList();
            return list;
        }
        public int getPntTransferId(string perCode, int pntYear, int pntMonth)
        {
            SqlConnection sql= new SqlConnection(testsqlConn);
            return sql.ExecuteScalar<int>("select Id from Ntl_PntTransfer where PerCode=@perCode and PntMonth=@pntMonth and PntYear=@pntYear ", new { perCode, pntYear, pntMonth });
        }
        public DateTime getPntPeriodControl()
        {
            SqlConnection sql= new SqlConnection(testsqlConn);
            return sql.ExecuteScalar<DateTime>(" select PERDBEG from H_001_PAYPERDS where  PERDEND is null ");
        }    
        public void savePntTransfer(Ntl_PntTransfer pntTransfer)
        {
            string query=$@" INSERT INTO dbo.Ntl_PntTransfer
                       (  PerCode         
                         ,PntMonth       
                         ,PntYear  
                         ,SgkGun   
                         ,VergiOdemeGunu           
                         ,ToplamCalismaGunu      
                         ,Gun          
                         ,Saat         
                         ,HTatGun     
                         ,HTatSaat         
                         ,GTatGun   
                         ,GTatSaat     
                         ,YillikIzinGun 
                         ,YillikIzinSaat 
                         ,SosyalIzinGun 
                         ,SosyalIzinSaat 
                         ,IdariGun 
                         ,IdariSaat 
                         ,UsizIzinGun 
                         ,UsizIzinSaat     
                         ,IstrahatGun     
                         ,IstrahatSaat           
                         ,PandemiUsizGun    
                         ,PandemiUsizSaat          
                         ,Fzm1  
                         ,BayramMesaisi 
                         ,KisaCalismaOdenegiGun  
                         ,KisaCalismaOdenegisaat 
                       ,Transfered
                       ,Message)
                 VALUES
                       (@PerCode         
                       ,@PntMonth       
                       ,@PntYear  
                       ,@SgkGun   
                       ,@VergiOdemeGunu        
                       ,@ToplamCalismaGunu     
                       ,@Gun          
                       ,@Saat         
                       ,@HTatGun     
                       ,@HTatSaat         
                       ,@GTatGun   
                       ,@GTatSaat     
                       ,@YillikIzinGun 
                       ,@YillikIzinSaat 
                       ,@SosyalIzinGun 
                       ,@SosyalIzinSaat 
                       ,@IdariGun 
                       ,@IdariSaat 
                       ,@UsizIzinGun 
                       ,@UsizIzinSaat     
                       ,@IstrahatGun     
                       ,@IstrahatSaat          
                       ,@PandemiUsizGun    
                       ,@PandemiUsizSaat       
                       ,@Fzm1  
                       ,@BayramMesaisi 
                       ,@KisaCalismaOdenegiGun 
                       ,@KisaCalismaOdenegisaat                     
                       ,0
                       ,'')";

            SqlConnection sql= new SqlConnection(testsqlConn);
            sql.Execute(query, pntTransfer);

        }
        public void updatePntTransfer(Ntl_PntTransfer pntTransfer)
        {

            string query=$@" UPDATE dbo.Ntl_PntTransfer
                                SET PerCode=@PerCode         
                                   ,PntMonth=@PntMonth       
                                   ,PntYear=@PntYear  
                                   ,SgkGun=@SgkGun   
                                   ,VergiOdemeGunu=@VergiOdemeGunu        
                                   ,ToplamCalismaGunu=@ToplamCalismaGunu     
                                   ,Gun=@Gun          
                                   ,Saat=@Saat         
                                   ,HTatGun=@HTatGun     
                                   ,HTatSaat=@HTatSaat         
                                   ,GTatGun=@GTatGun   
                                   ,GTatSaat=@GTatSaat     
                                   ,YillikIzinGun=@YillikIzinGun 
                                   ,YillikIzinSaat=@YillikIzinSaat 
                                   ,SosyalIzinGun=@SosyalIzinGun 
                                   ,SosyalIzinSaat=@SosyalIzinSaat 
                                   ,IdariGun=@IdariGun 
                                   ,IdariSaat=@IdariSaat 
                                   ,UsizIzinGun=@UsizIzinGun 
                                   ,UsizIzinSaat=@UsizIzinSaat     
                                   ,IstrahatGun=@IstrahatGun     
                                   ,IstrahatSaat=@IstrahatSaat          
                                   ,PandemiUsizGun=@PandemiUsizGun    
                                   ,PandemiUsizSaat=@PandemiUsizSaat       
                                   ,Fzm1=@Fzm1  
                                   ,BayramMesaisi=@BayramMesaisi 
                                   ,KisaCalismaOdenegiGun=@KisaCalismaOdenegiGun 
                                   ,KisaCalismaOdenegisaat=@KisaCalismaOdenegisaat                                   
                                   ,Transfered = 0
                                   ,Message = ''
                                 WHERE Id = @Id";
            SqlConnection sql= new SqlConnection(testsqlConn);
            sql.Execute(query, pntTransfer);

        }
    }
}