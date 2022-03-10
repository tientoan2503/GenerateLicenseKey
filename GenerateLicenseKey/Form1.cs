using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateLicenseKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Copy license key
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtLicenseKey.Text.ToString() != "")
            {
                Clipboard.SetText(txtLicenseKey.Text.ToString());
            }
        }

        // hash 
        private string hash(string text)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        // get license key
        private string getLicenseKey(string text)
        {
            string removeText = text.Substring(3, text.Length - 4);
            string newText = text.Replace(removeText, "");
            string hashText = hash(newText);
            string removeHash = hashText.Substring(3, hashText.Length - 6);
            return hashText.Replace(removeHash, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtIdMay.Text.ToString().Trim() != "")
            {
                string licenseKey = getLicenseKey(hash(txtIdMay.Text.ToString().Trim()));
                txtLicenseKey.Text = licenseKey;
            }
        }
    }
}
