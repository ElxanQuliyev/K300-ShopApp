using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApp
{
    public partial class Form1 : Form
    {
        ShopDB db = new ShopDB();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnlogin.Location = new Point((this.ClientSize.Width - lblwelcom.Size.Width)/2,this.ClientSize.Height - 100);
            //lblwelcom.Text = "Welcome to our Online Shop";
            //lblwelcom.Location = new Point((this.ClientSize.Width - lblwelcom.Size.Width) / 2, 50);
            //pictureBox1.Location = new Point((this.ClientSize.Width - pictureBox1.Size.Width) / 2,
            //    (this.ClientSize.Height - pictureBox1.Size.Height) / 2);

            if (Properties.Settings.Default.isChecked)
            {
                txtmail.Text = Properties.Settings.Default.email;
                txtpswrd.Text = Properties.Settings.Default.password;
            }
        }

        
        public void LoginDashboad()
        {

            string email = txtmail.Text;
            string password = txtpswrd.Text;
            if (email != "" && password != "")
            {
                Admin selectAdmin = db.Admins.FirstOrDefault(adm => adm.Email == email && adm.Password == password);
                if (selectAdmin != null)
                {
                    if (selectAdmin.Password == password)
                    {
                        AdminDashboard admd = new AdminDashboard();
                        admd.ShowDialog();
                    }
                }
                Worker selectedWorker = db.Workers.FirstOrDefault(wr => wr.Email == email);
                if (selectedWorker != null)
                {

                    if (selectedWorker.Password == password)
                    {
                        if (ckrmber.Checked)
                        {
                            Properties.Settings.Default.email = txtmail.Text;
                            Properties.Settings.Default.password = txtpswrd.Text;
                            Properties.Settings.Default.isChecked = true;
                            Properties.Settings.Default.Save();

                        }
                        else
                        {
                            Properties.Settings.Default.email = "";
                            Properties.Settings.Default.password = "";
                            Properties.Settings.Default.isChecked = false;
                            Properties.Settings.Default.Save();
                        }
                        WorkerDashboard wrk = new WorkerDashboard();
                        wrk.ShowDialog();
                    }
                    else
                    {
                        lblEror.Text = "pASWordD doesnt correct";

                    }
                }
                else
                {
                    lblEror.Text = "Email doesnt correct";
                }
            }


            else
            {
                lblEror.Text = "Plase All the Fill";
            }
        }
        private void btnLogin(object sender, EventArgs e)
        {
            LoginDashboad();

        }

        private void txtpswrd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                LoginDashboad();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
