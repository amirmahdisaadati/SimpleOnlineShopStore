using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Test.Utils.Utils
{
    public class RandomStringGenerator
    {
        private static readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private static readonly Random _random = new Random();

        public static string GenerateRandomString(int length = 50)
        {
            var stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(_chars[_random.Next(_chars.Length)]);
            }
            return stringBuilder.ToString();
        }
    }
}
