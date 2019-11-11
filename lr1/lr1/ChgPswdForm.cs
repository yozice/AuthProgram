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
                                userF3.password = Crypter.EncryptPswd(textBox1.Text);
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
                if (textBox3.Text == Crypter.DecryptPswd(userF3.password))
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
                                    userF3.password = Crypter.EncryptPswd(textBox1.Text);
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
                            userF3.password = Crypter.EncryptPswd(textBox1.Text);
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
                 if (pswChanged)
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

        //// string -> 
        //public static string EncryptPswd(string decPswd)
        //{
        //    List<char> encPswd = new List<char> { };
        //    List<char> encPswdSS = new List<char> { }; //SS = SecondStep
        //    int gamma = 0;
        //    // Первый шаг шифрования: побайтное шифрование без ключа
        //    for (int i = 0; i < decPswd.Length - 1; i++)
        //    {
        //        encPswd.Add(Convert.ToChar(decPswd[i] ^ decPswd[i + 1]));
        //    }
        //    encPswd.Insert(0, Convert.ToChar(decPswd[0] ^ encPswd[encPswd.Count - 1]));
        //    // Второй шаг шифрования: гаммирование
        //    for (int i = 0; i < encPswd.Count; i++)
        //    {
        //        encPswdSS.Add(Convert.ToChar(encPswd[i] ^ gamma));
        //        gamma = (5 * gamma + 3) % 256;
        //    }

        //    return ConvToStrFromList(encPswdSS);
        //}

        //public static string DecryptPswd(string encPswd)
        //{
        //    List<char> decPswd = new List<char> { };
        //    List<char> decPswdSS = new List<char> { };
        //    int gamma = 0;
        //    // обратное гаммирование
        //    for (int i = 0; i < encPswd.Length; i++)
        //    {
        //        decPswd.Add(Convert.ToChar(encPswd[i] ^ gamma));
        //        gamma = (5 * gamma + 3) % 256;
        //    }
        //    // побайтная дешифрация без ключа
        //    decPswdSS.Add(Convert.ToChar(decPswd[0] ^ decPswd[decPswd.Count - 1]));
        //    for (int i = 0; i < decPswd.Count - 1; i++)
        //    {
        //        decPswdSS.Add(Convert.ToChar(decPswd[i + 1] ^ decPswdSS[i]));
        //    }

        //    //decPswdSS.Add(Convert.ToChar(encPswd[0] ^ encPswd[encPswd.Length - 1]));
        //    //for (int i = 0; i < encPswd.Length - 1; i++)
        //    //{
        //    //    decPswdSS.Add(Convert.ToChar(encPswd[i + 1] ^ decPswdSS[i]));
        //    //}
        //    return ConvToStrFromList(decPswdSS);
        //}

        //public static string ConvToStrFromList(List<char> pswd)
        //{
        //    string res = "";
        //    for (int i = 0; i < pswd.Count; i++)
        //    {
        //        res += pswd[i];
        //    }
        //    return res;
        //}
    }
}
