using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr1
{
    class Crypter
    {
        // string -> 
        public static string EncryptPswd(string decPswd)
        {
            List<char> encPswd = new List<char> { };
            List<char> encPswdSS = new List<char> { }; //SS = SecondStep
            int gamma = 0;
            // Первый шаг шифрования: побайтное шифрование без ключа
            for (int i = 0; i < decPswd.Length - 1; i++)
            {
                encPswd.Add(Convert.ToChar(decPswd[i] ^ decPswd[i + 1]));
            }
            encPswd.Insert(0, Convert.ToChar(decPswd[0] ^ encPswd[encPswd.Count - 1]));
            // Второй шаг шифрования: гаммирование
            for (int i = 0; i < encPswd.Count; i++)
            {
                encPswdSS.Add(Convert.ToChar(encPswd[i] ^ gamma));
                gamma = (5 * gamma + 3) % 256;
            }

            return ConvToStrFromList(encPswdSS);
        }

        public static string DecryptPswd(string encPswd)
        {
            List<char> decPswd = new List<char> { };
            List<char> decPswdSS = new List<char> { };
            int gamma = 0;
            // обратное гаммирование
            for (int i = 0; i < encPswd.Length; i++)
            {
                decPswd.Add(Convert.ToChar(encPswd[i] ^ gamma));
                gamma = (5 * gamma + 3) % 256;
            }
            // побайтная дешифрация без ключа
            decPswdSS.Add(Convert.ToChar(decPswd[0] ^ decPswd[decPswd.Count - 1]));
            for (int i = 0; i < decPswd.Count - 1; i++)
            {
                decPswdSS.Add(Convert.ToChar(decPswd[i + 1] ^ decPswdSS[i]));
            }

            //decPswdSS.Add(Convert.ToChar(encPswd[0] ^ encPswd[encPswd.Length - 1]));
            //for (int i = 0; i < encPswd.Length - 1; i++)
            //{
            //    decPswdSS.Add(Convert.ToChar(encPswd[i + 1] ^ decPswdSS[i]));
            //}
            return ConvToStrFromList(decPswdSS);
        }

        public static string ConvToStrFromList(List<char> pswd)
        {
            string res = "";
            for (int i = 0; i < pswd.Count; i++)
            {
                res += pswd[i];
            }
            return res;
        }
    }
}
