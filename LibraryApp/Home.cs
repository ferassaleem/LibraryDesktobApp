using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class frmHome : Form
    {
        string Data = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\LibraryApp\\LibraryApp\\Library.mdf;Integrated Security=True";
        //string Data = "Data Source=DESKTOP-CDSJAGG; Initial Catalog=Library;Integrated Security=True; TrustServerCertificate=True";

        private string _fullName;

        public frmHome(string fullName)
        {
            InitializeComponent();

            _fullName = fullName;
            lblFullName.Text = "Welcome " + _fullName;

            LoadData();
        }
        public void LoadData()
        {
            int x = 100;
            int y = 50;
            int counter = 0;

            SqlConnection connection = new SqlConnection(Data);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Book", connection);
            SqlDataReader datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                string column2Value = datareader.GetString(1);


                Panel paneln = new Panel();
                paneln.Size = new Size(340, 380);
                paneln.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(231))))); ;
                panel3.Controls.Add(paneln);

                counter++;
                paneln.Location = new Point(x, y);
                x += paneln.Width + 70;

                if (counter == 3)
                {
                    y += paneln.Height + 20;
                    x = 100;
                    counter = 0;
                }

                ////Title
                Panel paneltitle = new Panel();
                paneltitle.Size = new Size(340, 80);
                paneltitle.BackColor = Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(221)))), ((int)(((byte)(202)))));
                paneltitle.Dock = DockStyle.Top;
                TextBox title = new TextBox();
                title.Multiline = true;
                title.Location = new Point(50, 30);
                title.Font = new System.Drawing.Font("Berlin Sans FB", 16F, System.Drawing.FontStyle.Bold);
                title.ForeColor = Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(143)))), ((int)(((byte)(152)))));
                title.TextAlign = HorizontalAlignment.Center;
                title.Text = Convert.ToString(column2Value);
                title.ReadOnly = true; // جعل الـ TextBox للقراءة فقط
                title.Size = new Size(240, 20);
                title.BackColor = Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(221)))), ((int)(((byte)(202))))); ;
                title.Cursor = Cursors.Default;
                title.BorderStyle = BorderStyle.None;
                paneltitle.Controls.Add(title);
                paneln.Controls.Add(paneltitle);

                //picturebox
                string imageUrl = datareader.GetString(2); // افترض أن العمود 1 هو column5Value
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(100, 100);
                pictureBox.Size = new Size(140, 200);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                // تحقق إذا كان القيمة صالحة كرابط إنترنت
                if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                {
                    try
                    {
                        using (var webClient = new System.Net.WebClient())
                        {
                            using (var stream = new MemoryStream(webClient.DownloadData(imageUrl)))
                            {
                                pictureBox.Image = Image.FromStream(stream);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error image");
                    }
                }
                else
                {
                    // قراءة الصورة من ملف محلي
                    if (File.Exists(imageUrl))
                    {
                        try
                        {
                            pictureBox.Image = Image.FromFile(imageUrl);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error image");
                        }
                    }
                }

                paneln.Controls.Add(pictureBox);

               

                Button update = new Button();
                update.Location = new Point(40, 320);
                update.Text = "btnUpdate";
                update.Size = new Size(120, 50);
                update.ForeColor = Color.White;
                update.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Bold);
                update.BackColor = Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(143)))), ((int)(((byte)(152)))));
                paneln.Controls.Add(update);
                update.Click += new System.EventHandler(btnUpdate_Click);

                Button delete = new Button();
                delete.Location = new Point(180, 320);
                delete.Text = "btnDelete";
                delete.Size = new Size(120, 50);
                delete.ForeColor = Color.White;
                delete.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Bold);
                delete.BackColor = Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(143)))), ((int)(((byte)(152)))));
                paneln.Controls.Add(delete);
                delete.Click += new System.EventHandler(btnDelete_Click);

            }
            datareader.Close();
            connection.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update_Book updateBook = new Update_Book();
            updateBook.ShowDialog();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete_Book deleteBook = new Delete_Book();
            deleteBook.ShowDialog();
        }
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            Add_Book add_Book = new Add_Book();
            add_Book.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "Time is: " + DateTime.Now.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            LoadData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Book newBook = new Add_Book();
            newBook.ShowDialog();
        }
    }
}
