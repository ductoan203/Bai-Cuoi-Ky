using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kiem_Tra_Giua_Ky
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string connectionString = "Data Source=MSI;Initial Catalog=QLSV_HoTenSv;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        String status;

        void LoadData()
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from tblSinhVien";
            da.SelectCommand= cmd;
            dt.Clear();
            da.Fill(dt);
            dgvMain.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open(); 
            status = "Reset";
            LoadData();
            chon(status);
        }
        public void chon(String status)
        {
            switch (status)
            {
                case "Reset":
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;

                    txtID.Enabled = false;
                    txtTen.Enabled = false;
                    dtpNgaysinh.Enabled = false;
                    cbbGioiTinh.Enabled = false;
                    cbbHocvan.Enabled = false;
                    txtQuequan.Enabled = false;
                    txtDiachi.Enabled = false;
                    txtGhichu.Enabled = false;
                    txtTimkiem.Enabled = true;
                    break;

                case "Insert":
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnGhi.Enabled = true;
                    btnHuy.Enabled = true;

                    txtID.Enabled = true;
                    txtTen.Enabled = true;
                    dtpNgaysinh.Enabled = true;
                    cbbGioiTinh.Enabled = true;
                    cbbHocvan.Enabled = true;
                    txtQuequan.Enabled = true;
                    txtDiachi.Enabled = true;
                    txtGhichu.Enabled = true;
                    txtTimkiem.Enabled = false;

                    txtID.Focus();

                    break;

                case "Edit":
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnGhi.Enabled = true;
                    btnHuy.Enabled = true;

                    txtID.Enabled = false;
                    txtTen.Enabled = true;
                    dtpNgaysinh.Enabled = true;
                    cbbGioiTinh.Enabled = true;
                    cbbHocvan.Enabled = true;
                    txtQuequan.Enabled = true;
                    txtDiachi.Enabled = true;
                    txtGhichu.Enabled = true;

                    txtTimkiem.Enabled = false;
                    txtTen.Focus();

                    break;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            status = "Insert";
            chon(status);
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if(status == "Insert")
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "Insert into tblSinhVien values('"+txtID.Text+"','"+txtTen.Text+"','"+dtpNgaysinh.Value.ToString("MM/dd/yyyy")+"','"+cbbGioiTinh.Text+"','"+cbbHocvan.Text+"','"+txtQuequan.Text+"','"+txtDiachi.Text+"','"+txtGhichu.Text+"')";
                cmd.ExecuteNonQuery();
                LoadData();
            }
            if(status == "Edit")
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "Update tblSinhVien set TenSV='" + txtTen.Text + "', NgaySinh = '" + dtpNgaysinh.Value.ToString("MM/dd/yyyy") + "', GioiTinh = '" + cbbGioiTinh.Text + "', TrinhDoHocVan = '" + cbbHocvan.Text + "'," +
                    " QueQuan ='" + txtQuequan.Text + "', DiaChi ='" + txtDiachi.Text + "', GhiChu = '" + txtGhichu.Text + "' Where ID ='"+txtID.Text+"'";
                cmd.ExecuteNonQuery();
                LoadData();
           
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Ban co muon huy bo khong", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                status = "Reset";
                chon(status);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            status = "Edit";
            chon(status);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            cmd= con.CreateCommand();
            cmd.CommandText = "Delete from tblSinhVien where TenSV = '"+txtTen.Text+"'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Xoa thanh cong");
            LoadData();
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var row = dgvMain.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtTen.Text = row.Cells["TenSV"].Value.ToString();
                dtpNgaysinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtDiachi.Text = row.Cells["DiaChi"].Value.ToString();
                txtQuequan.Text = row.Cells["QueQuan"].Value.ToString();
                txtGhichu.Text = row.Cells["Ghichu"].Value.ToString();
                cbbGioiTinh.Text = row.Cells["Gioitinh"].Value.ToString();
                cbbHocvan.Text = row.Cells["Hocvan"].Value.ToString();
            }
            catch(Exception ex) { }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            status = "Insert";
            chon(status);
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * From tblSinhVien where TenSv= '" + txtTimkiem.Text + "'";
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvMain.DataSource= dt;
        }
    }
}
