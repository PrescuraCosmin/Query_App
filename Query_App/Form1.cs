using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Query_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ExecuteBtn.Click += ExecuteBtn_1_Click;
        }
        private void Btn_1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The porcess has started");

        }

        private void ExecuteBtn_1_Click(object sender, EventArgs e)
        {
            {
                string[] queryFilePaths =
 {
                @"C:\Users\Cosmin\Desktop\Script_queries\SQLQuery.sql",
                @"C:\Users\Cosmin\Desktop\Script_queries\SQLQuery2.sql"
            };
                string connectionString = @"Data Source=DESKTOP-N8PKN5D\MSSQLSERVER2023;Initial Catalog=Automatic queries;Integrated Security=True";

                CreateCommand(queryFilePaths, connectionString);
            }
        }
        // CreateCommand is the command for query execution
        private void CreateCommand(string[] queryFilePaths, string connectionString)
        {
            // Clear the text 
            TxtOutput.Clear();

            // adding the progress bar 

            ProgressBar1.Maximum = queryFilePaths.Length;
            ProgressBar1.Value = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (string queryFilePath in queryFilePaths)
                {
                    string queryString = File.ReadAllText(queryFilePath);
                    SqlCommand command = new SqlCommand(queryString, con);
                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        AppendText("Querry executed succesfully. Rows affected: " + rowsAffected);
                    }
                    catch (Exception ex)
                    {
                        AppendText("Error executing query: " + ex.Message);
                    }

                    ProgressBar1.Value++;

                    System.Threading.Thread.Sleep(300);
                }
            }
        }
        private void AppendText(string text)
        {
            TxtOutput.AppendText(text + Environment.NewLine);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ProgressBar1.Click += ProgressBar1_Click;
        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtOutput.Clear();

            MessageBox.Show("Logs deleted!");
        }
    }
}
