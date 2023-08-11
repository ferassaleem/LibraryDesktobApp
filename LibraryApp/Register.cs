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
    public partial class frmRegister : Form
    {
        string Data = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\LibraryApp\\LibraryApp\\Library.mdf;Integrated Security=True";
        //string Data = "Data Source=DESKTOP-CDSJAGG; Initial Catalog=Library;Integrated Security=True; TrustServerCertificate=True";

        public frmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Data);
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Users VALUES(@UserName, @FullName, @Email, @Password)", connection);
            command.Parameters.AddWithValue("@UserName", txtUserName.Text);
            command.Parameters.AddWithValue("@FullName", txtFullName.Text);
            command.Parameters.AddWithValue("@Email", txtEmail.Text);
            command.Parameters.AddWithValue("@Password", txtPassword.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Register is successfully");
            Close();
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
