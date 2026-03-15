using Microsoft.Data.SqlClient;
using System.Data;

namespace ShivaC_Learning
{
    public partial class Form1 : Form
    {
        private Label lblFirstName;
        private Label lblSecondName;
        private TextBox txtFirstName;
        private TextBox txtSecondName;
        private Button btnSubmit;
        private DataGridView dgvNames;

        string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDB;Integrated Security=True";


        private void InitializeComponent()
        {
            lblFirstName = new Label();
            lblSecondName = new Label();
            txtFirstName = new TextBox();
            txtSecondName = new TextBox();
            btnSubmit = new Button();
            dgvNames = new DataGridView();

            SuspendLayout();

            // Labels
            lblFirstName.Text = "First Name:";
            lblFirstName.Location = new Point(50, 30);
            lblFirstName.AutoSize = true;

            lblSecondName.Text = "Second Name:";
            lblSecondName.Location = new Point(50, 70);
            lblSecondName.AutoSize = true;

            // TextBoxes
            txtFirstName.Location = new Point(160, 30);
            txtFirstName.Size = new Size(150, 27);

            txtSecondName.Location = new Point(160, 70);
            txtSecondName.Size = new Size(150, 27);

            // Button
            btnSubmit.Text = "Submit";
            btnSubmit.Location = new Point(150, 110);
            btnSubmit.Size = new Size(100, 30);
            btnSubmit.Click += BtnSubmit_Click;

            // DataGridView
            dgvNames.Location = new Point(20, 160);
            dgvNames.Size = new Size(350, 150);
            dgvNames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Form
            ClientSize = new Size(400, 330);
            Controls.Add(lblFirstName);
            Controls.Add(txtFirstName);
            Controls.Add(lblSecondName);
            Controls.Add(txtSecondName);
            Controls.Add(btnSubmit);
            Controls.Add(dgvNames);
            Text = "User Entry Form";

            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string secondName = txtSecondName.Text;

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(secondName))
            {
                MessageBox.Show("Please enter both names.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query =
                        "INSERT INTO Names (FirstName, SecondName) VALUES (@FirstName, @SecondName)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@SecondName", secondName);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data inserted successfully!");

                txtFirstName.Clear();
                txtSecondName.Clear();

                LoadData(); // Refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Names";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvNames.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Error: " + ex.Message);
            }
        }
    }
}
