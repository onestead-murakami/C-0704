using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ex02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = Enumerable.Range(0, 3000).ToList();
            var start = DateTime.Now;
            var path = @"c:\SECollege\0704\button1_Click";
            Directory.CreateDirectory(path);
            list.ForEach(value =>
            {
                var file = Path.Combine(path, $"{value}.txt");
                using (var writer = new StreamWriter(file))
                    writer.WriteLine("演習");
            });
            var time = DateTime.Now - start;
            MessageBox.Show(time.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = Enumerable.Range(0, 3000).ToList();
            var start = DateTime.Now;
            var path = @"c:\SECollege\0704\button2_Click";
            Directory.CreateDirectory(path);
            list.AsParallel().ForAll(value =>
            {
                var file = Path.Combine(path, $"{value}.txt");
                using (var writer = new StreamWriter(file))
                    writer.WriteLine("演習");
            });
            var time = DateTime.Now - start;
            MessageBox.Show(time.ToString());
        }
    }
}
