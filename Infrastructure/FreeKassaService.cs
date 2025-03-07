using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Telegram.Bot.Types.Payments;
using VpnLibrary;

namespace Infrastructure
{
    public class FreeKassaService
    {
        private readonly string _path = "https://pay.fk.money/";
        public string FormOfPay(long tgId) 
        {
            string telegramIdString = tgId.ToString();
            string signature = $"{StaticInfo.IdOfShop}:{StaticInfo.PaymentAmount}:{StaticInfo.Secret1}:RUB:{telegramIdString}";
            string signatureHash = Hash.GetHash(signature);
            string url = $"{_path}?m={StaticInfo.IdOfShop}&oa={StaticInfo.PaymentAmount}&currency=RUB&o={telegramIdString}&s={signatureHash}";
            return url;
        }
    }
}
