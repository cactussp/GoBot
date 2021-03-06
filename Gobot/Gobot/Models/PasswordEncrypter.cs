﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Gobot.Models
{
    public class PasswordEncrypter
    {
        public static string EncryptPassword(string password)
        {
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            byte[] salt = new byte[32];
            rand.GetBytes(salt);
            PBKDF2 passwordEncrypter = new PBKDF2(password, salt, 100000, "HMACSHA256");
            byte[] encPass = passwordEncrypter.GetBytes(32);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in salt)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            foreach (byte b in encPass)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        public static string EncryptPassword(string password, string stringsalt)
        {
            byte[] salt = new byte[32];
            for(int i = 0; i < stringsalt.Length - 1; i += 2)
            {
                salt[i / 2] = Convert.ToByte(stringsalt.Substring(i, 2), 16);
            }
            
            PBKDF2 passwordEncrypter = new PBKDF2(password, salt, 100000, "HMACSHA256");
            byte[] encPass = passwordEncrypter.GetBytes(32);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in salt)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            foreach (byte b in encPass)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
    }
}