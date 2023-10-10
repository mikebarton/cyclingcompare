using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Merchant
{
    public partial class MerchantService
    {
        public const string GetMerchantIdStatement = @"SELECT MerchantId FROM MerchantMapping where ApiManager = @apiManager and ApiIdentifier = @apiIdentifier limit 1";
        public const string GetAllMerchantsStatement = @"SELECT m.MerchantId, 
                                                                m.CommissionMax, 
                                                                m.CommissionMin, 
                                                                m.CommissionRate, 
                                                                m.CookieDurationHours, 
                                                                m.DateModified, 
                                                                m.Status, 
                                                                m.Name, 
                                                                m.Summary, 
                                                                m.TargetUrl, 
                                                                m.TermsAndConditions, 
                                                                m.TrackingCode, 
                                                                m.TrackingUrl ,
                                                                mm.ApiManager
                                                                FROM Merchant m
                                                                JOIN MerchantMapping mm on m.MerchantId = mm.MerchantId
                                                                where m.IsDeleted = 0";

        public const string InsertMerchantStatement = @"INSERT INTO `Merchant`
                                                (`MerchantId`,
                                                `CommissionMax`,
                                                `CommissionMin`,
                                                `CommissionRate`,
                                                `CookieDurationHours`,
                                                `DateModified`,
                                                `Status`,
                                                `Name`,
                                                `Summary`,
                                                `TargetMarket`,
                                                `TargetUrl`,
                                                `TermsAndConditions`,
                                                `TrackingCode`,
                                                `TrackingUrl`,
                                                `ValidationPeriod`)
                                                VALUES
                                                (@merchantId,
                                                @commissionMax,
                                                @commissionMin,
                                                @commissionRate,
                                                @cookieDurationHours,
                                                @dateModified,
                                                @status,
                                                @name,
                                                @summary,
                                                @targetMarket,
                                                @targetUrl,
                                                @termsAndConditions,
                                                @trackingCode,
                                                @trackingUrl,
                                                @validationPeriod)
                                                ";

        public const string InsertMerchantMappingStatement = @"INSERT INTO `MerchantMapping`
                                                                (`MerchantMappingId`,
                                                                `ApiManager`,
                                                                `ApiIdentifier`,
                                                                `MerchantId`)
                                                                VALUES
                                                                (@merchantMappingId,
                                                                @apiManager,
                                                                @apiIdentifier,
                                                                @merchantId)
                                                                ";

        public const string UpdateMerchantStatement = @"UPDATE `Merchant`
                                                        SET
                                                        `CommissionMax` = @commissionMax,
                                                        `CommissionMin` = @commissionMin,
                                                        `CommissionRate` = @commissionRate,
                                                        `CookieDurationHours` = @cookieDurationHours,
                                                        `DateModified` = @dateModified,
                                                        `Status` = @status,
                                                        `Name` = @name,
                                                        `Summary` = @summary,
                                                        `TargetMarket` = @targetMarket,
                                                        `TargetUrl` = @targetUrl,
                                                        `TermsAndConditions` = @termsAndConditions,
                                                        `TrackingCode` = @trackingCode,
                                                        `TrackingUrl` = @trackingUrl,
                                                        `ValidationPeriod` = @validationPeriod
                                                        WHERE MerchantId = @merchantId";
    }
}
