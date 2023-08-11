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
using System.Xml.Linq;

namespace LibraryApp
{
    public partial class frmLogin : Form
    {
        string Data = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\LibraryApp\\LibraryApp\\Library.mdf;Integrated Security=True";
        //string Data = "Data Source=DESKTOP-CDSJAGG; Initial Catalog=Library;Integrated Security=True; TrustServerCertificate=True";
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Data);
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * From Users WHERE UserName ='" + txtUserName.Text + "' AND Password = '" + txtPassword.Text + "'", connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                string fullName = dataTable.Rows[0]["FullName"].ToString();
                frmHome home = new frmHome(fullName);
                home.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("UserName or Password is worng");
            }
            connection.Close();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegister register = new frmRegister();
            register.ShowDialog();
            Close();
        }
    }
}
