using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenAsset.RestClient.TestLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void addToRows<T>(T[] dataArray)
        {
            dataGridView1.DataSource = dataArray.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.fillGetData(int.Parse(limitTextBox.Text), int.Parse(offsetTextBox.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = "Updating!";
            Program.updatePutData(long.Parse(idTextBox.Text), captionTextBox.Text);
            button2.Text = "Update caption";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult diagRes = openFileDialog1.ShowDialog();
            if (diagRes == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                filenameTextBox.Text = filename;
            }
        }

        private void postButton_Click(object sender, EventArgs e)
        {
            Program.newPostData(filenameTextBox.Text, int.Parse(accessLevelTextBox.Text), long.Parse(categoryIdTextBox.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.delete(long.Parse(delIdTextBox.Text));
        }




    }
}
