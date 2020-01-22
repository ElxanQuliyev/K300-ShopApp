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
    public partial class ProductForm : Form
    {
        ShopDB dB = new ShopDB();
        Product selectedPro;
        public ProductForm()
        {
            InitializeComponent();
        }

        private void btnAddproduct_Click(object sender, EventArgs e)
        {
            string productname = txtprdct.Text;
            string prodctPrice = txtprdcprice.Text;
            string prodctdes = richtxtdescrpt.Text;
            string amount = txtAmount.Text;
            string size = cmbboxSize.Text;
            string category = cmbboxCategory.Text;
            string[] emp = { productname , prodctPrice ,prodctdes,amount,size,category};
            if (mainExtensions.IsEmpty(emp,string.Empty)) {


                int catId = dB.Categories.First(ct => ct.Name == category).Id;
                int SizeId = dB.ProductSizes.First(ct => ct.Size== size).Id;

                Product product = new Product();


                product.ProductName = productname;
                product.Price = Convert.ToDouble(prodctPrice);
                product.Description = prodctdes;
                product.Amounts = Convert.ToInt32(amount);
                product.CategoryId  = catId;
                product.SizeId = SizeId;

                dB.Products.Add(product);
                dB.SaveChanges();
                MessageBox.Show("Added product","succes",MessageBoxButtons.OK,MessageBoxIcon.Information);
                FillGridVeiw();
            }


        }
        private void FillGridVeiw(){
            dtgProducts.DataSource = dB.Products.Select(pr => new
            {
                pr.Id,
                pr.ProductName,
                pr.ProductSize.Size,
                pr.Category.Name,
                pr.Price,
                pr.Description

            }).ToList();
            dtgProducts.Columns[0].Visible = false;
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            dtgProducts.ForeColor = Color.Black;
            FillGridVeiw();
            cmbboxCategory.Items.Add("Selected...");
            cmbboxCategory.SelectedIndex = 0;
            //dataGridView1.DataSource = dB.Products.Select(pr => { pr.ProductName,})
            cmbboxCategory.Items.AddRange(dB.Categories.Select(ct => ct.Name).ToArray());
            cmbboxSize.Items.AddRange(dB.ProductSizes.Select(ct => ct.Size).ToArray());
            
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar<48 || e.KeyChar>57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void cmbboxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbboxCategory.Items.Remove("Selected...");
        }

        private void dtgProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditClick(object sender, EventArgs e)
        {
            string proname = txtprdct.Text;
            string price = txtprdcprice.Text;
            string say = txtAmount.Text;
            string catname = cmbboxCategory.Text;
            string size = cmbboxSize.Text;
            string desc = richtxtdescrpt.Text;
            string[] listpro = { proname, price, say, catname, size };
            if (mainExtensions.IsEmpty(listpro,""))
            {
                int catId = dB.Categories.FirstOrDefault(ct => ct.Name == catname).Id;
                int sizeId = dB.ProductSizes.FirstOrDefault(ct => ct.Size == size).Id;

                selectedPro.ProductName = proname;
                selectedPro.Price = Convert.ToDouble(price);
                selectedPro.Amounts = Convert.ToInt32(say);
                selectedPro.Description = desc;
                selectedPro.CategoryId = catId;
                selectedPro.SizeId = sizeId;
                dB.SaveChanges();
                BtnEditDel("add");
                FillGridVeiw();
            }
            else
            {
                lblError.Text = "Please all the fill!";
                lblError.Visible = true;
            }
        }

        private void btnDeleteClick(object sender, EventArgs e)
        {

        }
        private void BtnEditDel(string txt)
        {
            if (txt == "add")
            {
                btnAddproduct.Visible = true;
                btnEdit.Visible = false;
                btnDelete.Visible = false;

            }
            else
            {
                btnAddproduct.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;

            }
        }
        private void dtgProducts_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           int proId= (int)dtgProducts.Rows[e.RowIndex].Cells[0].Value;

            selectedPro = dB.Products.FirstOrDefault(pr => pr.Id == proId);
            
            txtprdct.Text = selectedPro.ProductName;
            txtprdcprice.Text = selectedPro.Price.ToString();
            txtAmount.Text = selectedPro.Amounts.ToString();
            cmbboxCategory.Text = selectedPro.Category.Name;
            cmbboxSize.Text = selectedPro.ProductSize.Size;
            richtxtdescrpt.Text = selectedPro.Description;
            BtnEditDel("asa");
        }
    }
}
