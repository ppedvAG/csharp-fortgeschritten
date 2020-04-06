using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace WinFormsGoogleBooks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var suchUrl = "https://www.googleapis.com/books/v1/volumes?q=" + suchTb.Text;


            //var url = "https://www.googleapis.com/books/v1/volumes?q=katze";

            //var web = new WebClient(); //AAAALT!!!

            var http = new HttpClient();
            var json = await http.GetStringAsync(suchUrl);

            textBox1.Text = json;

            BookResults result = JsonConvert.DeserializeObject<BookResults>(json);
            dataGridView1.DataSource = result.items.Select(x => x.volumeInfo).ToList();

        }
    }
}
