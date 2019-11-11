using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr1
{
    public partial class AddUsersForm : Form
    {
        public AddUsersForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            profile user;
            bool userExists=false;
            user.name = textBox1.Text;
            user.password = "";
            user.len_pswrd = 0;
            user.is_blocked = false;
            user.is_restricted = false;
            foreach(profile userBuf in dataBase.dBase)
            {
                if (userBuf.name == user.name)
                {
                    userExists = true;
                }
            }
            if(!userExists)
            {
                dataBase.dBase.Add(user);
                label2.Text = "Пользователь " + textBox1.Text + " добавлен";
            }
            else
            {
                label2.Text = "Пользователь " + textBox1.Text + " уже существует";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddUsersForm_Load(object sender, EventArgs e)
        {

        }
    }
}
