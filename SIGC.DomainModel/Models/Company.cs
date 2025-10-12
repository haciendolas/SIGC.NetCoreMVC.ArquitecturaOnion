using SIGC.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGC.DomainModel.Models
{
   public class Company
    {
        public int CompanyID { get;  set; }
        public string CompanyTradeName { get; private set; } = null!;
        public string CompanySocialReason { get; private set; } = null!;
        public string CompanyDocumentNumber { get; private set; }  = null!;
        public DateTime CompanyBirthDate { get; private set; }
        public int CountryID { get; private set; }
        public string? CompanyAddress { get; private set; }
        public Int16 TaxpayerTypeID { get; private set; }
        public short SectorID { get; private set; }
        public string? CompanyMobile { get; private set; }
        public string? CompanyPhone { get; private set; }
        public string? CompanyLogo { get; private set; }
        public StateEnum StateID { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public int CreatedBy { get; private set; }

        protected Company() { }

        public static Company Create(string CompanyTradeName,string CompanySocialReason,
            string CompanyDocumentNumber,DateTime CompanyBirthDate,int CountryID,
            string? CompanyAddress,short TaxpayerTypeID,short SectorID,string? CompanyMobile,
            string? CompanyPhone,string? CompanyLogo, StateEnum StateID,DateTime CreatedDateTime,
            int CreatedBy
            )
        {
            return new Company()
            {
                CompanyTradeName = CompanyTradeName,
                CompanySocialReason = CompanySocialReason,
                CompanyDocumentNumber = CompanyDocumentNumber,
                CompanyBirthDate = CompanyBirthDate,
                CountryID = CountryID,
                CompanyAddress = CompanyAddress,
                TaxpayerTypeID = TaxpayerTypeID,
                SectorID = SectorID,
                CompanyMobile = CompanyMobile,
                CompanyPhone = CompanyPhone,
                CompanyLogo = CompanyLogo,
                StateID = StateID,
                CreatedDateTime = CreatedDateTime,
                CreatedBy = CreatedBy
            };
        }

       public static Company Update(int CompanyID, string CompanyTradeName,string CompanySocialReason,
            string CompanyDocumentNumber,DateTime CompanyBirthDate,int CountryID,
            string? CompanyAddress,short TaxpayerTypeID,short SectorID,string? CompanyMobile,
            string? CompanyPhone,string? CompanyLogo, StateEnum StateID,DateTime CreatedDateTime,
            int CreatedBy
            )
        {
            return new Company()
            {
                CompanyID = CompanyID,
                CompanyTradeName = CompanyTradeName,
                CompanySocialReason = CompanySocialReason,
                CompanyDocumentNumber = CompanyDocumentNumber,
                CompanyBirthDate = CompanyBirthDate,
                CountryID = CountryID,
                CompanyAddress = CompanyAddress,
                TaxpayerTypeID = TaxpayerTypeID,
                SectorID = SectorID,
                CompanyMobile = CompanyMobile,
                CompanyPhone = CompanyPhone,
                CompanyLogo = CompanyLogo,
                StateID = StateID,
                CreatedDateTime = CreatedDateTime,
                CreatedBy = CreatedBy
            };
        }

        public static Company ChangeState(int CompanyID,  StateEnum StateID, DateTime CreatedDateTime, int CreatedBy)
        {
            return new Company()
            {
                CompanyID = CompanyID,                
                StateID = StateID,
                CreatedDateTime = CreatedDateTime,
                CreatedBy = CreatedBy
            };
        }
    }
}
