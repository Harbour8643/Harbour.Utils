using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Harbour.Utils
{
    /// <summary>
    /// DES����/�����ࡣ
    /// </summary>
    public class DESEncrypt
    {

        private static string Key = "MATICSOFT";

        private static string IV = Key;

        private static Encoding Encod = Encoding.UTF8;


        #region ========����========

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text">����</param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, Key);
        }

        /// <summary> 
        /// �������� 
        /// </summary> 
        /// <param name="Text">����</param> 
        /// <param name="Key">Key</param>
        /// <returns>����Base64�����ı�</returns> 
        public static string Encrypt(string Text, string Key)
        {
            return Encrypt(Text, Key, IV);
        }

        /// <summary> 
        /// �������� 
        /// </summary> 
        /// <param name="Text">����</param> 
        /// <param name="Key">Key</param> 
        /// <param name="IV">����</param>
        /// <returns>����Base64�����ı�</returns> 
        public static string Encrypt(string Text, string Key, string IV)
        {
            return Encrypt(Text, Key, IV, Encod);
        }

        /// <summary> 
        /// �������� 
        /// </summary> 
        /// <param name="Text">����</param> 
        /// <param name="Key">Key</param> 
        /// <param name="IV">����</param>
        /// <param name="Encod">���뷽ʽ</param>
        /// <returns>����Base64�����ı�</returns> 
        public static string Encrypt(string Text, string Key, string IV, Encoding Encod)
        {
            DESCryptoServiceProvider des = GetDesscsp(Key, IV, Encod);
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] inputByteArray = Encod.GetBytes(Text);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();
                    cs.Close();
                    ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }

        #endregion

        #region ========����========


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text">���ģ�Base64���룩</param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, Key);
        }

        /// <summary> 
        /// �������� 
        /// </summary> 
        /// <param name="Text">���ģ�Base64���룩</param> 
        /// <param name="Key">Key</param> 
        /// <returns>����</returns> 
        public static string Decrypt(string Text, string Key)
        {
            return Decrypt(Text, Key, IV);
        }

        /// <summary> 
        /// �������� 
        /// </summary> 
        /// <param name="Text">���ģ�Base64���룩</param> 
        /// <param name="Key">Key</param> 
        /// <param name="IV">����</param> 
        /// <returns>����</returns> 
        public static string Decrypt(string Text, string Key, string IV)
        {
            return Decrypt(Text, Key, IV, Encod);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="Text">���ģ�Base64���룩</param>
        /// <param name="Key">��Կ</param>
        /// <param name="IV">����</param>
        /// <param name="Encod">���뷽ʽ</param>
        /// <returns>����</returns>
        public static string Decrypt(string Text, string Key, string IV, Encoding Encod)
        {
            DESCryptoServiceProvider des = GetDesscsp(Key, IV, Encod);
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] inputByteArray = Convert.FromBase64String(Text);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();
                    cs.Close();
                    ms.Close();
                }
            }
            return Encod.GetString(cipherBytes);
        }

        #endregion

        private static DESCryptoServiceProvider GetDesscsp(string Key, string IV, Encoding Encod)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //��Կ����ֻ����8λ
            des.Key = Encod.GetBytes(MD5Encrypt.MD5(Key).Substring(0, 8));
            des.IV = Encod.GetBytes(MD5Encrypt.MD5(IV).Substring(0, 8));
            return des;
        }
    }
}
