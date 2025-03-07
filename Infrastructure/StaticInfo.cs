using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnLibrary
{
    public class StaticInfo
    {
        public static readonly string MainPath = Environment.GetEnvironmentVariable("MainPath") ?? throw new Exception("not found \"MainPath\" variable");
        public static readonly string Username = Environment.GetEnvironmentVariable("VpnUsername") ?? throw new Exception("not found \"Username\" variable");
        public static readonly string Password = Environment.GetEnvironmentVariable("VpnPassword") ?? throw new Exception("not found \"Password\" variable");
        public static readonly string IdOfShop = Environment.GetEnvironmentVariable("ShopId") ?? throw new Exception("not found \"ShopId\" variable");
        public static readonly string PaymentAmount = Environment.GetEnvironmentVariable("PaymentAmount") ?? throw new Exception("not found \"PaymentAmount\" variable");
        public static readonly string Secret1 = Environment.GetEnvironmentVariable("Secret1") ?? throw new Exception("not found \"Secret1\" variable");
    }
}
