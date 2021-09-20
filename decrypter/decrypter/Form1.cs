using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ransomware;


namespace decrypter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private static void decrypt_file(string dir, Byte[] key,ref bool flag)
        {
            var di = new DirectoryInfo(dir);
            try
            {
                foreach (FileInfo fi in di.GetFiles("*.*"))

                    module.decrypt_check(fi.FullName, key,ref flag);
                foreach (DirectoryInfo d in di.GetDirectories())
                    decrypt_file(d.FullName, key,ref flag);
            }
            catch (Exception)
            {
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;

            string[] pairs =this.textBox1.Text.Split('-');

            byte[] mykey = new byte[pairs.Length];
            try
            {
                for (int i = 0; i < pairs.Length; i++)
                    mykey[i] = Convert.ToByte(Convert.ToString(pairs[i]), 16);
                decrypt_file(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), mykey,ref flag);

                MessageBox.Show("done");
                
                
            }
            catch (Exception) { }
           
            
        }
    }
}
