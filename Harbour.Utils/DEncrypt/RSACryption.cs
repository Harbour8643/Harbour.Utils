using System;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
namespace Harbour.Utils
{
    /// <summary>
    /// RSACryption,.netFramework���ṩ��RSA�㷨�涨��ÿ�μ��ܵ��ֽ������ܳ�����Կ�ĳ���ֵ��ȥ11����ÿ�μ��ܵõ������ĳ���ǡǡ����Կ�ĳ��ȣ��ɲ��÷ֶμ���
    /// </summary>
    public class RSACryption
    {

        private static Encoding Encod = Encoding.Unicode;

        /// <summary>
        /// RSA������Կ
        /// </summary>
        /// <param name="xmlPrivateKeys">xml˽Կ</param>
        /// <param name="xmlPublicKey">xml��Կ</param>
        public static void RSAKey(out string xmlPrivateKeys, out string xmlPublicKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlPrivateKeys = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ����

        /// <summary>
        /// RSA�ļ��ܺ�����KEY������XML����ʽ,���ص����ַ������ü��ܷ�ʽ�г������Ƶģ�
        /// </summary>
        /// <param name="xmlPublicKey">xml��Կ</param>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <returns>���ģ�Base64��</returns>
        public static string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            return RSAEncrypt(xmlPublicKey, encryptString, Encod);
        }

        /// <summary>
        /// RSA�ļ��ܺ�����KEY������XML����ʽ,���ص����ַ������ü��ܷ�ʽ�г������Ƶģ�
        /// </summary>
        /// <param name="xmlPublicKey">xml��Կ</param>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="Encod">���뷽ʽ</param>
        /// <returns>���ģ�Base64��</returns>
        public static string RSAEncrypt(string xmlPublicKey, string encryptString, Encoding Encod)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = Encod.GetBytes(encryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// RSA�Ľ��ܺ���,KEY������XML����ʽ
        /// </summary>
        /// <param name="xmlPrivateKey">˽Կ</param>
        /// <param name="decryptString">�����ܵ��ַ���(Base64)</param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            return RSADecrypt(xmlPrivateKey, decryptString, Encod);
        }

        /// <summary>
        /// RSA�Ľ��ܺ���,KEY������XML����ʽ
        /// </summary>
        /// <param name="xmlPrivateKey">˽Կ</param>
        /// <param name="decryptString">�����ܵ��ַ���(Base64)</param>
        /// <param name="Encod">���뷽ʽ</param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, string decryptString, Encoding Encod)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = Encod.GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region �ֶμ��ܽ���

        /// <summary>
        /// �ֶμ��ܣ�KEY������XML����ʽ
        /// </summary>
        /// <param name="xmlPublicKey">��Կ</param>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <returns>���ģ�Base64��</returns>
        public static String RSABlockEncrypt(string xmlPublicKey, String encryptString)
        {
            return RSABlockEncrypt(xmlPublicKey, encryptString, Encod);
        }

        /// <summary>
        /// �ֶμ��ܣ�KEY������XML����ʽ
        /// </summary>
        /// <param name="xmlPublicKey">��Կ</param>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="Encod">���뷽ʽ</param>
        /// <returns>���ģ�Base64��</returns>
        public static String RSABlockEncrypt(string xmlPublicKey, String encryptString, Encoding Encod)
        {
            try
            {
                RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider();
                RSACryptography.FromXmlString(xmlPublicKey);
                byte[] PlaintextData = Encod.GetBytes(encryptString);
                int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //���ܿ���󳤶�����
                if (PlaintextData.Length <= MaxBlockSize)
                    return Convert.ToBase64String(RSACryptography.Encrypt(PlaintextData, false));

                using (MemoryStream PlaiStream = new MemoryStream(PlaintextData))
                {
                    using (MemoryStream CrypStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToEncrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                            Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
                            CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                            BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA�ķֶν��ܺ���,KEY������XML����ʽ
        /// </summary>
        /// <param name="xmlPrivateKey">��Կ</param>
        /// <param name="decryptString">�����ܵ��ַ���(Base64)</param>
        /// <returns></returns>
        public static String RSABlockDecrypt(string xmlPrivateKey, String decryptString)
        {
            return RSABlockDecrypt(xmlPrivateKey, decryptString, Encod);
        }

        /// <summary>
        /// RSA�ķֶν��ܺ���,KEY������XML����ʽ
        /// </summary>
        /// <param name="xmlPrivateKey">��Կ</param>
        /// <param name="decryptString">�����ܵ��ַ���(Base64)</param>
        /// <param name="Encod">���뷽ʽ</param>
        /// <returns></returns>
        public static String RSABlockDecrypt(string xmlPrivateKey, String decryptString, Encoding Encod)
        {
            RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider();
            RSACryptography.FromXmlString(xmlPrivateKey);

            Byte[] CiphertextData = Convert.FromBase64String(decryptString);
            int MaxBlockSize = RSACryptography.KeySize / 8;    //���ܿ���󳤶�����

            if (CiphertextData.Length <= MaxBlockSize)
                return Encod.GetString(RSACryptography.Decrypt(CiphertextData, false));

            using (MemoryStream CrypStream = new MemoryStream(CiphertextData))
            {
                using (MemoryStream PlaiStream = new MemoryStream())
                {
                    Byte[] Buffer = new Byte[MaxBlockSize];
                    int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] ToDecrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                        Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                        PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                        BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    return Encod.GetString(PlaiStream.ToArray());
                }
            }
        }

        #endregion

        #region RSA����ǩ��

        #region ��ȡHash������

        /// <summary>
        /// ��ȡHash������
        /// </summary>
        /// <param name="strSource">��ǩ�����ַ���</param>
        /// <param name="HashData">Hash����</param>
        /// <returns></returns>
        public static bool GetHash(string strSource, ref byte[] HashData)
        {
            try
            {
                byte[] Buffer;
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                Buffer = Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ȡHash������
        /// </summary>
        /// <param name="strSource">��ǩ�����ַ���</param>
        /// <param name="strHashData">Hash����</param>
        /// <returns></returns>
        public static bool GetHash(string strSource, ref string strHashData)
        {
            try
            {
                //���ַ�����ȡ��Hash���� 
                byte[] Buffer;
                byte[] HashData;
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                Buffer = Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ȡHash������
        /// </summary>
        /// <param name="objFile">��ǩ�����ļ�</param>
        /// <param name="HashData">Hash����</param>
        /// <returns></returns>
        public static bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            try
            {
                //���ļ���ȡ��Hash���� 
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ȡHash������
        /// </summary>
        /// <param name="objFile">��ǩ�����ļ�</param>
        /// <param name="strHashData">Hash����</param>
        /// <returns></returns>
        public static bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            try
            {
                //���ļ���ȡ��Hash���� 
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RSAǩ��

        /// <summary>
        /// RSAǩ��
        /// </summary>
        /// <param name="strKeyPrivate">˽Կ</param>
        /// <param name="HashbyteSignature">��ǩ��Hash����</param>
        /// <param name="EncryptedSignatureData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //����ǩ�����㷨ΪMD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //ִ��ǩ�� 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSAǩ��
        /// </summary>
        /// <param name="strKeyPrivate">˽Կ</param>
        /// <param name="HashbyteSignature">��ǩ��Hash����</param>
        /// <param name="strEncryptedSignatureData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref string strEncryptedSignatureData)
        {
            byte[] EncryptedSignatureData = Convert.FromBase64String(strEncryptedSignatureData);

            bool bol = SignatureFormatter(strKeyPrivate, HashbyteSignature, ref  EncryptedSignatureData);
            strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
            if (bol)
                return true;
            else
                return false;
        }

        /// <summary>
        /// RSAǩ��
        /// </summary>
        /// <param name="strKeyPrivate">˽Կ</param>
        /// <param name="strHashbyteSignature">��ǩ��Hash����</param>
        /// <param name="EncryptedSignatureData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;

                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //����ǩ�����㷨ΪMD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //ִ��ǩ�� 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSAǩ��
        /// </summary>
        /// <param name="strKeyPrivate">˽Կ</param>
        /// <param name="strHashbyteSignature">��ǩ��Hash����</param>
        /// <param name="strEncryptedSignatureData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                byte[] EncryptedSignatureData;
                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //����ǩ�����㷨ΪMD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //ִ��ǩ�� 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA ǩ����֤
        /// <summary>
        /// RSAǩ����֤
        /// </summary>
        /// <param name="strKeyPublic">��Կ</param>
        /// <param name="HashbyteDeformatter">Hash����</param>
        /// <param name="DeformatterData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSAǩ����֤
        /// </summary>
        /// <param name="strKeyPublic">��Կ</param>
        /// <param name="strHashbyteDeformatter">Hash����</param>
        /// <param name="DeformatterData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, byte[] DeformatterData)
        {
            return SignatureDeformatter(strKeyPublic, Convert.FromBase64String(strHashbyteDeformatter), DeformatterData);
        }
        /// <summary>
        /// RSAǩ����֤
        /// </summary>
        /// <param name="strKeyPublic">��Կ</param>
        /// <param name="HashbyteDeformatter">Hash����</param>
        /// <param name="strDeformatterData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, string strDeformatterData)
        {
            return SignatureDeformatter(strKeyPublic, HashbyteDeformatter, Convert.FromBase64String(strDeformatterData));
        }
        /// <summary>
        /// RSAǩ����֤
        /// </summary>
        /// <param name="strKeyPublic">��Կ</param>
        /// <param name="strHashbyteDeformatter">Hash����</param>
        /// <param name="strDeformatterData">ǩ����Ľ��</param>
        /// <returns></returns>
        public static bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
        {
            return SignatureDeformatter(strKeyPublic, Convert.FromBase64String(strHashbyteDeformatter), Convert.FromBase64String(strDeformatterData));
        }
        #endregion

        #endregion

    }
}
