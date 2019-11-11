using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lr1
{
    public partial class LoginForm : Form
    {
        int cntFails;
        string prevName;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cntFails = 0;
            prevName = "";
            try
            {
                using (StreamReader sr = new StreamReader(@"data.txt"))
                {
                    string line;
                    string[] lineSplitted = new string[5];
                    profile user;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        lineSplitted = line.Split(' ');
                        user.name = lineSplitted[0];
                        user.password = lineSplitted[1];
                        user.len_pswrd = Convert.ToInt32(lineSplitted[2]);
                        user.is_blocked = Convert.ToBoolean(lineSplitted[3]);
                        user.is_restricted = Convert.ToBoolean(lineSplitted[4]);
                        dataBase.dBase.Add(user);
                    }
                }
            }
            catch(FileNotFoundException e1)
            {
                profile user;
                user.name = "Admin";
                user.password = "";
                user.len_pswrd = 0;
                user.is_blocked = false;
                user.is_restricted = true;
                dataBase.dBase.Add(user);
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(profile user in dataBase.dBase)
            {
                if(user.name.Equals(textBox1.Text.ToString()))
                {
                    if (user.is_blocked)
                    {
                        label3.Text = "Пользователь " + user.name + " заблокирован";
                        break;
                    }
                    else
                    {
                        if (user.password == "")
                        {
                            ActSelForm newForm = new ActSelForm(user, true);
                            newForm.Owner = this;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            label3.Text = "";
                            this.Hide();
                            newForm.Show();
                            break;
                        }
                        else if (user.password == textBox2.Text.ToString())
                        {
                            ActSelForm newform = new ActSelForm(user, false);
                            newform.Owner = this;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            label3.Text = "";
                            this.Hide();
                            newform.Show();
                            break;
                        }
                        else
                        {
                            if(user.name == prevName)
                            {
                                cntFails++;
                            }
                            else
                            {
                                prevName = user.name;
                                cntFails = 0;
                            }
                            label3.Text = "Неверный пароль";
                            break;
                        }
                    }
                }
                else
                {
                    label3.Text = "Такого пользователя не существует";
                }
            }
            if (cntFails == 2)
            {
                Close();
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 newForm = new AboutBox1();
            newForm.ShowDialog();
        }
    }
}


//try
//{
//    using (FileStream fstream = new FileStream(@"data.txt", FileMode.Open))
//    {
//        MessageBox.Show("Файл открыт");
//    }
//}
//catch(FileNotFoundException e1)
//{
//    MessageBox.Show("Файл не существует \nСоздание нового");
//    using (FileStream fstream = new FileStream(@"data.txt", FileMode.Create))
//    {
//        fstream.Write(System.Text.Encoding.Default.GetBytes("Admin 1 1 0 0"), 0, 13);
//    }
//}