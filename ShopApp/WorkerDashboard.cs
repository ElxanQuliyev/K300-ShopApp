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
    public partial class WorkerDashboard : Form
    {
        ShopDB db = new ShopDB();
        #region Cunsturctor
        public WorkerDashboard()
        {
            InitializeComponent();
        }
        #endregion

        #region Worker Load Event
        private void WorkerDashboard_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.AddRange(db.Categories.Select(ct => ct.Name).ToArray());
        }
#endregion

        private void CmbCategoryFill()
        {

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProducts.Items.Clear();
            Category selectedCategory = db.Categories.FirstOrDefault(ct => ct.Name == cmbCategory.Text);
            if (selectedCategory != null)
            {
                cmbProducts.Items.AddRange(db.Products.Where(pr => pr.CategoryId == selectedCategory.Id).Select(pr=>pr.ProductName).ToArray());

            }
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string proname = cmbProducts.Text;
            Product selectedPro = db.Products.FirstOrDefault(pr => pr.ProductName == proname);
            if (selectedPro != null)
            {
                if (selectedPro.Amounts <= 0)
                {
                    lblStock.Visible = true;
                    lblStock.Text = "Stockda bu mehsuldan qalmayib";
                    lblStock.BackColor = Color.Crimson;
                    lblStock.ForeColor = Color.White;
                    btnSale.Enabled = false;
                    numericUpDown1.Enabled = false;
                }

                else
                {
                    lblStock.Visible = true;
                    lblStock.Text = $"Mehsuldan {selectedPro.Amounts} qeder qalib";
                    lblStock.BackColor = Color.Green;
                    lblStock.ForeColor= Color.White;
                    btnSale.Enabled = true;
                    numericUpDown1.Enabled = true;

                }
            }
        }
    }
}
