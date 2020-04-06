using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

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

        private void button2_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "XML Datei|*.xml|Alle Dateien|*.*";
            dlg.Title = "Bücher speichern";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    serial.Serialize(sw, dataGridView1.DataSource);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML Datei|*.xml|Alle Dateien|*.*";
            dlg.Title = "Bücher laden";


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sr = new StreamReader(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    dataGridView1.DataSource = serial.Deserialize(sr);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "DAT Datei|*.dat|Alle Dateien|*.*";
            dlg.Title = "Bücher speichern";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new FileStream(dlg.FileName, FileMode.Create))
                {
                    var serial = new BinaryFormatter();
                    serial.Serialize(sw, dataGridView1.DataSource);
                }
            }
        }
    }

}
