using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES_Algorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                string encryptedText = EncryptData(textBox1.Text, textBox3.Text, true);
                if (checkBox1.Checked)
                    File.WriteAllText(@"C:\Users\rolik\OneDrive\Desktop\New folder\EncryptedText.txt", encryptedText);
                textBox5.Text = encryptedText;
                
            }
            else
            {
               string  encryptedText = EncryptData(textBox1.Text, textBox3.Text, false);
                if (checkBox1.Checked)
                    File.WriteAllText(@"C:\Users\rolik\OneDrive\Desktop\New folder\EncryptedText.txt", encryptedText);
                    textBox5.Text = encryptedText;
            }
        }

        public string EncryptData(string strData, string strKey, bool EBC)
        {
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] key = new byte[8];
            byte[] inputByteArray;
            byte[] hash;

            try
            {
                MD5CryptoServiceProvider ha = new MD5CryptoServiceProvider();
                hash = ha.ComputeHash(Encoding.ASCII.GetBytes(strKey));
                for (int i = 0; i < 8; i++)
                {
                    key[i] = hash[i];
                }
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                if (EBC == true)
                {
                    ObjDES.Mode = CipherMode.ECB;
                    ObjDES.Key = key;
                }
                else
                {
                    ObjDES.Mode = CipherMode.CBC;
                    ObjDES.Key = key;
                    ObjDES.IV = IV;
                }
                    inputByteArray = Encoding.ASCII.GetBytes(strData);
                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DecryptData(string strData, string strKey, bool EBC)
        {
            byte[] key = new byte[8];// Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[strData.Length];
            byte[] hash;
            try
            {
                MD5CryptoServiceProvider ha = new MD5CryptoServiceProvider();
                hash = ha.ComputeHash(Encoding.ASCII.GetBytes(strKey));
                for (int i = 0; i < 8; i++)
                {
                    key[i] = hash[i];
                }
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                if (EBC == true)
                {
                    ObjDES.Mode = CipherMode.ECB;
                    ObjDES.Key = key;
                }
                else
                {
                    ObjDES.Mode = CipherMode.CBC;
                    ObjDES.Key = key;
                    ObjDES.IV = IV;
                }
                inputByteArray = Convert.FromBase64String(strData);

                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                Encoding encoding = Encoding.ASCII;
                return encoding.GetString(Objmst.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (checkBox2.Checked)
                {
                    textBox6.Text = DecryptData(File.ReadAllText(@"C:\Users\rolik\OneDrive\Desktop\New folder\EncryptedText.txt"), textBox4.Text, true);
                }
                else
                {
                    textBox6.Text = DecryptData(textBox2.Text, textBox4.Text, true);
                }
            }
            else
            {
                if (checkBox2.Checked)
                {
                    textBox6.Text = DecryptData(File.ReadAllText(@"C:\Users\rolik\OneDrive\Desktop\New folder\EncryptedText.txt"), textBox4.Text, false);
                }
                else
                    textBox6.Text = DecryptData(textBox2.Text, textBox4.Text, false);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
