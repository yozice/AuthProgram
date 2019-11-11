﻿using System;
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
    public partial class ActSelForm : Form
    {
        profile userF2;
        bool isFirstAuthF2;

        public ActSelForm(profile user, bool isFirstAuth)
        {
            userF2 = user;
            isFirstAuthF2 = isFirstAuth;
            InitializeComponent();
        }

        private void ActSelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"data.txt"))
            {
                for (int i = 0; i < dataBase.dBase.Count; i++)
                {
                    sw.WriteLine(dataBase.dBase[i].name + " " +
                        dataBase.dBase[i].password + " " +
                        dataBase.dBase[i].len_pswrd + " " +
                        dataBase.dBase[i].is_blocked + " " +
                        dataBase.dBase[i].is_restricted);
                }
            }
            this.Owner.Show();
        }

        private void ActSelForm_Load(object sender, EventArgs e)
        {
            if (isFirstAuthF2)
            {
                ChgPswdForm newForm = new ChgPswdForm(userF2, true);
                newForm.ShowDialog();
                isFirstAuthF2 = false;
            }
            if (userF2.name != "Admin")
            {
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChgPswdForm newForm = new ChgPswdForm(userF2, false);
            newForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewUsersForm newForm = new ViewUsersForm();
            newForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddUsersForm newForm = new AddUsersForm();
            newForm.ShowDialog();
        }
    }
}