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

    }
}
