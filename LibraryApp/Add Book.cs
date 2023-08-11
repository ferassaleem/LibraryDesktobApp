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

namespace LibraryApp
{
    public partial class Add_Book : Form
    {
        string Data = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\LibraryApp\\LibraryApp\\Library.mdf;Integrated Security=True";

        public Add_Book()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Data);
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Book VALUES(@BookName, @ImageData)", connection);
            command.Parameters.AddWithValue("@BookName", txtName.Text);
            command.Parameters.AddWithValue("@ImageData", txtImageData.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Adding is successfully");
            Close();
        }
    }
}
