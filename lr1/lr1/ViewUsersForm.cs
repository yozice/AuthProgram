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
    public partial class ViewUsersForm : Form
    {
        int index;
        List<profile> dBaseF4;
        public ViewUsersForm()
        {
            index = 1;
            dBaseF4 = dataBase.dBase;

            if (dataBase.dBase.Count == 1)
            {
                button1.Enabled = false;
            }
            InitializeComponent();
        }

        private void ViewUsersForm_Load(object sender, EventArgs e)
        {
            if(dataBase.dBase.Count != 1)
            {
                textBox1.Text = dataBase.dBase[index].name;
                checkBox1.Checked = dataBase.dBase[index].is_blocked;
                checkBox2.Checked = dataBase.dBase[index].is_restricted;
            }
        }

        private void BtnNext(object sender, EventArgs e)
        {
            index++;
            if (index == dataBase.dBase.Count)
            {
                index = 1;
            }
            if (dataBase.dBase.Count != 1)
            {
                textBox1.Text = dataBase.dBase[index].name;
                checkBox1.Checked = dataBase.dBase[index].is_blocked;
                checkBox2.Checked = dataBase.dBase[index].is_restricted;
            }
        }

        private void BtnSave(object sender, EventArgs e)
        {
            profile user;
            user.name = dataBase.dBase[index].name;
            user.password = dataBase.dBase[index].password;
            user.len_pswrd = dataBase.dBase[index].len_pswrd;
            user.is_blocked = checkBox1.Checked;
            user.is_restricted = checkBox2.Checked;

            dataBase.dBase[index] = user;
        }

        private void BtnOk(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClose(object sender, EventArgs e)
        {
            dataBase.dBase = dBaseF4;
            Close();
        }
    }
}
