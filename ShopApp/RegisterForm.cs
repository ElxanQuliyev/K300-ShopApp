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
    public partial class RegisterForm : Form
    {
        ShopDB db;
        public RegisterForm()
        {
            db =  new ShopDB();
            InitializeComponent();
        }

        private void btnregister2_Click(object sender, EventArgs e)
        {
            string fullname = txtFulname.Text;
            string email = txtmail.Text;
            string phone = txtmail.Text;
            string password = txtPasword.Text;
            string confirm = txtConfirm.Text;
            string[] myempty = { fullname, email,phone, password, confirm };
            if (mainExtensions.IsEmpty(myempty, ""))
            {
                if(password == confirm)
                {
                    //Worker newWorker = new Worker();
                    //newWorker.Fullname = fullname;
                    //newWorker.Email = email;
                    //newWorker.Phone = phone;
                    //newWorker.Password = password;
                    db.Workers.Add(new Worker()
                    {

                        Fullname = fullname,
                        Email = email,
                        Phone = phone,
                        Password = password

                    }); 
                    db.SaveChanges();
                    MessageBox.Show("Worker Create succesfuly!");
                }
            }
            else
            {

            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
