using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Toolkit
{
    public static class RandomPassword
    {
        public static string CreateRandomPassword(int length = 14)
        {
            string validChars = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[1] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static int RandomNumberGenerator(int min = 100000, int max = 999999)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
