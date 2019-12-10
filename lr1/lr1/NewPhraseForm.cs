using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace lr1
{
    public partial class NewPhraseForm : Form
    {
        public NewPhraseForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] strByte;
            byte[] salt = new byte[8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(salt);
            }
            using (Rfc2898DeriveBytes sha1Hash = new Rfc2898DeriveBytes(textBox1.Text, salt))
            {
                strByte = sha1Hash.GetBytes(16);
            }
            Encrypt(strByte);

            using (FileStream fs = new FileStream(@"salt.txt", FileMode.Create, FileAccess.Write))
            {
                fs.Write(salt, 0, 8);
                fs.Close();
            }
            //dataBase.dBase.Clear();
            Close();
        }

        private void Encrypt(byte[] Key)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                FileStream fs = new FileStream(@"data.txt", FileMode.Create, FileAccess.Write);
                aesAlg.Key = Key;
                aesAlg.IV = Key;
                aesAlg.Mode = CipherMode.CBC;

                // Create a encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (CryptoStream csEncrypt = new CryptoStream(fs, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        for (int i = 0; i < dataBase.dBase.Count; i++)
                        {
                            swEncrypt.WriteLine(dataBase.dBase[i].name + " " +
                                dataBase.dBase[i].password + " " +
                                dataBase.dBase[i].len_pswrd + " " +
                                dataBase.dBase[i].is_blocked + " " +
                                dataBase.dBase[i].is_restricted);
                        }
                    }
                }
            }
        }

        private void NewPhraseForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
