﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingStoreApplication
{
    public partial class frmAddNewSeller : Form
    {
        DBConnect dbCon = new DBConnect();
        public frmAddNewSeller()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmAddNewSeller_Load(object sender, EventArgs e)
        {
            lblSellerID.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = true;
            BindSeller();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSellerName.Text == String.Empty)
            {
                MessageBox.Show("Please enter Seller Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSellerName.Focus();
                return;
            }
            else if (txtPass.Text == String.Empty)
            {
                MessageBox.Show("Please enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select SellerName from tblSeller where SellerName=@SellerName", dbCon.GetCon());
                cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                dbCon.OpenCon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Seller Name already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {
                    cmd = new SqlCommand("spSellerInsert", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                    cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtAge.Text));
                    cmd.Parameters.AddWithValue("@SellerPhone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@SellerPass", txtPass.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Seller inserted successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindSeller();
                    }
                }
                dbCon.CloseCon();


            }
        }

        private void txtClear()
        {
            txtSellerName.Clear();
            txtPass.Clear();
            txtAge.Clear();
            txtPhone.Clear();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSellerID.Text == String.Empty)
                {
                    MessageBox.Show("Please select SellerID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return;
                }
                if (txtSellerName.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellerName.Focus();
                    return;
                }
                else if (txtPass.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select SellerName from tblSeller where SellerName=@SellerName", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);

                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Seller Name already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spSellerUpdate", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(lblSellerID.Text));
                        cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                        cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtAge.Text));
                        cmd.Parameters.AddWithValue("@SellerPhone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@SellerPass", txtPass.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        dbCon.CloseCon();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller updated successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            btnAdd.Visible = true;
                            lblSellerID.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("update failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
                    }
                    dbCon.CloseCon();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSellerID.Text == String.Empty)
                {
                    MessageBox.Show("Please select Seller ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lblSellerID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spSellerDelete", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(lblSellerID.Text));
                        cmd.CommandType = CommandType.StoredProcedure;
                        dbCon.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller deleted successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            btnAdd.Visible = true;
                            lblSellerID.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Delete failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
                        dbCon.CloseCon();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BindSeller()
        {
            SqlCommand cmd = new SqlCommand("select * from tblSeller", dbCon.GetCon());
            dbCon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dbCon.CloseCon();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            lblSellerID.Visible = true;
            btnAdd.Visible = false;

            lblSellerID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtSellerName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtAge.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtPass.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}
