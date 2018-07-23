using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Harbour.Utils
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DESEncrypt
    {

        private static string Key = "MATICSOFT";

        private static string IV = Key;

        private static Encoding Encod = Encoding.UTF8;


        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">明文</param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, Key);
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">明文</param> 
        /// <param name="Key">Key</param>
        /// <returns>密文Base64编码文本</returns> 
        public static string Encrypt(string Text, string Key)
        {
            return Encrypt(Text, Key, IV);
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">明文</param> 
        /// <param name="Key">Key</param> 
        /// <param name="IV">向量</param>
        /// <returns>密文Base64编码文本</returns> 
        public static string Encrypt(string Text, string Key, string IV)
        {
            return Encrypt(Text, Key, IV, Encod);
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">明文</param> 
        /// <param name="Key">Key</param> 
        /// <param name="IV">向量</param>
        /// <param name="Encod">编码方式</param>
        /// <returns>密文Base64编码文本</returns> 
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

        #region ========解密========


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text">密文（Base64编码）</param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, Key);
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">密文（Base64编码）</param> 
        /// <param name="Key">Key</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string Text, string Key)
        {
            return Decrypt(Text, Key, IV);
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">密文（Base64编码）</param> 
        /// <param name="Key">Key</param> 
        /// <param name="IV">向量</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string Text, string Key, string IV)
        {
            return Decrypt(Text, Key, IV, Encod);
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="Text">密文（Base64编码）</param>
        /// <param name="Key">密钥</param>
        /// <param name="IV">向量</param>
        /// <param name="Encod">编码方式</param>
        /// <returns>明文</returns>
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
            //密钥长度只能是8位
            des.Key = Encod.GetBytes(MD5Encrypt.MD5(Key).Substring(0, 8));
            des.IV = Encod.GetBytes(MD5Encrypt.MD5(IV).Substring(0, 8));
            return des;
        }
    }
}
