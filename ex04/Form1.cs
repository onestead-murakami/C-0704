using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ex04
{
    public partial class Form1 : Form
    {
        private const string URL = "https://jl4j3d7n2b76pjbt6lo37vb43m0umuxc.lambda-url.ap-northeast-1.on.aws/";
        private readonly List<WebRequest> requests = new List<WebRequest>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));
            requests.Add(WebRequest.Create(URL));

            button1.Enabled = false;
            progressBar1.Value = 0;
            progressBar1.Step = (progressBar1.Maximum / requests.Count);

            for (int i = 0; i < requests.Count; i++)
            {
                requests[i].BeginGetResponse(CallbackGetResponse, requests[i]);
            }
        }

        private void CallbackGetResponse(IAsyncResult ar)
        {
            var request = (WebRequest)ar.AsyncState;
            var response = request.EndGetResponse(ar);
            string id = response.Headers["Content-Disposition"].Split('=')[1].Replace("\"", "");
            using (var stream = response.GetResponseStream())
            {
                using (var writer = File.CreateText(id))
                {
                    var buf = new byte[1024];
                    int count = stream.Read(buf, 0, buf.Length);
                    writer.WriteLine(Encoding.UTF8.GetString(buf, 0, count));
                }
            }
            requests.Remove(request);
            Invoke(new Action<int>(InvokeGetResponse), requests.Count);
        }

        private void InvokeGetResponse(int count)
        {
            progressBar1.PerformStep();
            if (count == 0)
            {
                MessageBox.Show("ダウンロード完了");
                button1.Enabled = true;
            }
        }

    }
}
