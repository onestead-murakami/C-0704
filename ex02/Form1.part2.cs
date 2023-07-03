using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ex02
{
    public partial class Form1Part2 : Form
    {
        public Form1Part2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var start = DateTime.Now;
            var list = Enumerable.Range(0, 3000).ToList();
            var path = @"c:\SECollege\0703\button1_Click";
            Directory.CreateDirectory(path);
            var file = Path.Combine(path, "button1_Click.txt");
            using (var writer = new StreamWriter(file))
            {
                list.ForEach(value =>
                {
                    long s = 0;
                    for (int i = 0; i < 1000000; i++)
                        s += i;
                    writer.WriteLine(value + ":" + s);
                });
            }
            var time = DateTime.Now - start;
            MessageBox.Show(time.ToString("s\\.fffffff"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = Enumerable.Range(0, 3000).ToList();
            var start = DateTime.Now;
            var path = @"c:\SECollege\0703\button2_Click";
            Directory.CreateDirectory(path);
            var file = Path.Combine(path, "button2_Click.txt");
            using (var writer = new StreamWriter(file))
            using (var editor = TextWriter.Synchronized(writer))
            {
                list.AsParallel().ForAll(value =>
                {
                    long s = 0;
                    for (int i = 0; i < 1000000; i++)
                        s += i;
                    editor.WriteLine(value + ":" + s);
                });
            }
            var time = DateTime.Now - start;
            MessageBox.Show(time.ToString("s\\.fffffff"));
        }

    }
}
