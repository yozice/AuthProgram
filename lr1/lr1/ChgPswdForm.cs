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
    public partial class ChgPswdForm : Form
    { 
        profile userF3;
        bool isFirstAuthF3;
        bool pswChanged;

        public ChgPswdForm(profile user, bool isFirstAuth)
        {
            int ind = -1;
            foreach (profile userBuf in dataBase.dBase)
            {
                ind++;
                if (userBuf.name == user.name)
                {
                    break;
                }
            }
            userF3 = dataBase.dBase[ind];
            isFirstAuthF3 = isFirstAuth;
            pswChanged = false;
            InitializeComponent();
        }

        private void ChgPswdForm_Load(object sender, EventArgs e)
        {
            if (isFirstAuthF3) 
            {
                textBox3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool fLet = false;
            bool fSym = false;
            bool fPun = false;
            if (isFirstAuthF3) 
            {
                if (userF3.is_restricted)
                {
                    if (textBox1.Text.Length >= 8)
                    {
                        foreach (char c in textBox1.Text)
                        {
                            if (char.IsLetter(c))
                            {
                                fLet = true;
                            }
                            else if (char.IsSymbol(c))
                            {
                                fSym = true;
                            }
                            else if (char.IsPunctuation(c))
                            {
                                fPun = true;
                            }
                        }

                        if (fLet && fSym && fPun)
                        {
                            if (textBox1.Text == textBox2.Text)
                            {
                                userF3.password = textBox1.Text;
                                userF3.len_pswrd = textBox1.Text.Length;
                                pswChanged = true;
                            }
                            else
                            {
                                label5.Text = "Пароли не совпадают";
                            }
                        }
                        else
                        {
                            label5.Text = "Необходимо использовать символы: a-z, A-Z \n+-*/=,.!?-():;\"";
                        }
                    }
                    else
                    {
                        label5.Text = "Длина пароля меньше 8 символов";
                    }
                }
                else
                {
                    if (textBox1.Text == textBox2.Text)
                    {
                        userF3.password = textBox1.Text;
                        userF3.len_pswrd = textBox1.Text.Length;
                        pswChanged = true;
                    }
                    else
                    {
                        label5.Text = "Пароли не совпадают";
                    }
                }
            }
            else
            {
                if(textBox3.Text==userF3.password)
                {
                    if (userF3.is_restricted)
                    {
                        if (textBox1.Text.Length >= 8)
                        {
                            foreach (char c in textBox1.Text)
                            {
                                if (char.IsLetter(c))
                                {
                                    fLet = true;
                                }
                                else if (char.IsSymbol(c))
                                {
                                    fSym = true;
                                }
                                else if (char.IsPunctuation(c))
                                {
                                    fPun = true;
                                }
                            }

                            if (fLet && fSym && fPun)
                            {
                                if (textBox1.Text == textBox2.Text)
                                {
                                    userF3.password = textBox1.Text;
                                    userF3.len_pswrd = textBox1.Text.Length;
                                    pswChanged = true;
                                }
                                else
                                {
                                    label5.Text = "Пароли не совпадают";
                                }
                            }
                            else
                            {
                                label5.Text = "Необходимо использовать символы: a-z, A-Z \n+-*/=,.!?-():;\"";
                            }
                        }
                        else
                        {
                            label5.Text = "Длина пароля меньше 8 символов";
                        }
                    }
                    else
                    {
                        if (textBox1.Text == textBox2.Text)
                        {
                            userF3.password = textBox1.Text;
                            userF3.len_pswrd = textBox1.Text.Length;
                            pswChanged = true;
                        }
                        else
                        {
                            label5.Text = "Пароли не совпадают";
                        }
                    }
                }
                else
                {
                    label4.Text = "Неверно введен старый пароль";
                }
            }

            int ind = -1;
            foreach (profile user in dataBase.dBase)
            {
                ind++;
                if (user.name == userF3.name)
                {
                    break;
                }
            }
            dataBase.dBase[ind] = userF3;
            if(pswChanged)
            {
                Close();
            }
        }

        private void ChgPswdForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isFirstAuthF3)
            {
                Application.Exit();
            }
            else
            {
                Close();
            }
        }
    }
}
