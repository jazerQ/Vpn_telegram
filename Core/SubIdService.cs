using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class SubIdService
    {
        private static string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        private static Random random = new Random();
        public static string Generate() 
        {
            string res = "";
            for (int i = 0; i < 16; i++) 
            {

                res += chars[random.Next(chars.Length)];
            }
            return res;
        }

    }
}
