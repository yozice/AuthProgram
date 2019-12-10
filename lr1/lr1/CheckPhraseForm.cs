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
    public partial class CheckPhraseForm : Form
    {
        public CheckPhraseForm()
        {
            InitializeComponent();
        }

        private void CheckPhraseForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"data.txt"));
            }
            catch (FileNotFoundException e1)
            {
                using (StreamWriter sw = new StreamWriter(@"data.txt"))
                {
                    sw.WriteLine("Admin" + " " +
                            "" + " " +
                            "0" + " " +
                            "false" + " " +
                            "true");
                }
            }
        }

        private string Decrypt(byte[] Key)
        {
            string decrypted;
            FileStream fs = new FileStream(@"data.txt", FileMode.Open, FileAccess.Read);
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = Key;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (CryptoStream csDecrypt = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        decrypted = srDecrypt.ReadToEnd();
                    }
                }
            }
            return decrypted;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isCorrectDecrypted = true; // Проверка верная ли парольная фраза была введена
            string decrypted = "";
            byte[] strByte;
            if (textBox1.Text != "")
            {
                byte[] salt = new byte[8];
                //using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                //{
                //    // Fill the array with a random value.
                //    rngCsp.GetBytes(salt);
                //}
                try
                {
                    using (FileStream fs = new FileStream(@"salt.txt", FileMode.Open, FileAccess.Read))
                    {
                        fs.Read(salt, 0, 8);
                        fs.Close();
                    }
                }
                catch(Exception err)
                {
                    label2.Text = "Неверно сгенерирована парольная фраза\nНеобходимо удалить файл data.txt";
                }
                using (Rfc2898DeriveBytes sha1Hash = new Rfc2898DeriveBytes(textBox1.Text, salt))
                {
                    strByte = sha1Hash.GetBytes(16);
                }
                // Decrypt the bytes to a string.
                try
                {
                    decrypted = Decrypt(strByte);
                }
                catch(Exception err)
                {
                    label2.Text = "Парольная фраза была введена неверно";
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(@"data.txt"))
                {
                    decrypted = sr.ReadToEnd();
                }
            }

            try
            {
                String[] line = decrypted.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int ind = 0;
                string[] lineSplitted = new string[5];
                profile user;
                while (ind < line.Length)
                {
                    lineSplitted = line[ind].Split(' ');
                    user.name = lineSplitted[0];
                    user.password = lineSplitted[1];
                    user.len_pswrd = Convert.ToInt32(lineSplitted[2]);
                    user.is_blocked = Convert.ToBoolean(lineSplitted[3]);
                    user.is_restricted = Convert.ToBoolean(lineSplitted[4]);
                    dataBase.dBase.Add(user);
                    ind++;
                }
            }
            catch(Exception e2)
            {
                label2.Text = "Парольная фраза была введена неверно";
                isCorrectDecrypted = false;
            }
            if (isCorrectDecrypted)
            {
                bool isCorrectPhrase = false;
                for (int i = 0; i < dataBase.dBase.Count; i++)
                {
                    if (dataBase.dBase[i].name == "Admin")
                    {
                        isCorrectPhrase = true;
                    }
                }
                if (isCorrectPhrase)
                {
                    LoginForm newForm = new LoginForm();
                    newForm.Owner = this;
                    newForm.Show();
                    this.Hide();
                }
                else
                {
                    label2.Text = "Парольная фраза была введена неверно";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
