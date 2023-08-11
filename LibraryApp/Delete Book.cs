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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryApp
{
    public partial class Delete_Book : Form
    {
        string Data = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\LibraryApp\\LibraryApp\\Library.mdf;Integrated Security=True";

        public Delete_Book()
        {
            InitializeComponent();
            fill_ListBox();
        }

        void fill_ListBox()
        {
            try
            {

                SqlConnection con = new SqlConnection(Data);
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                string selection = "Select BookName from Book";
                command.CommandText = selection;
                SqlDataReader myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    cmbName.Items.Add(myReader["BookName"].ToString());
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
