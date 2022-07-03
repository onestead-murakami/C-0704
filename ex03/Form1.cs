using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ex03
{
    public partial class Form1 : Form
    {
        private readonly object sync = new object();
        private StreamWriter writer = null;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != openFileDialog1.ShowDialog())
                return;

            var values = new List<string>();
            values.Add(textBox1.Text);
            values.Add(textBox2.Text);
            values.Add(textBox3.Text);
            values.Add(textBox4.Text);

            string fileName = openFileDialog1.FileName;
            using (this.writer = File.CreateText(fileName))
            {
                values.AsParallel().ForAll(WriteLineToFile);
            }

            MessageBox.Show("書込OK［" + fileName + "］");
        }

        private void WriteLineToFile(string text)
        {
            if (this.writer != null)
            {
                lock (this.sync)
                {
                    this.writer.WriteLine(text);
                }
            }
        }

    }
}
