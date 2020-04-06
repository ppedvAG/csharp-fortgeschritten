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

        private void button5_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "JSON Datei|*.json|Alle Dateien|*.*";
            dlg.Title = "Bücher speichern";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(dlg.FileName))
                    sw.Write(JsonConvert.SerializeObject(dataGridView1.DataSource, Formatting.Indented));
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is List<StringComparer>) //typprüfung
            {
                List<StringComparer> data = (List<StringComparer>)dataGridView1.DataSource; //casting
            }

            //boxing
            List<StringComparer> data2 = dataGridView1.DataSource as List<StringComparer>;
            if (data2 != null)
            {
                //...
            }

            if (dataGridView1.DataSource is List<Volumeinfo> volumes) //ab VS2017 - pattern matching
            {
                //Linq Quries / Linq Expressions
                var query = from b in volumes
                            where b.pageCount > 100
                            orderby b.averageRating descending, b.title
                            select b;

                dataGridView1.DataSource = query.ToList();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is List<Volumeinfo> volumes)
            {
             
                dataGridView1.DataSource = volumes.Where(b => b.pageCount > 100)
                                                  .OrderByDescending(x => x.averageRating)
                                                  .ThenBy(x => x.title)
                                                  .ToList();
            }


            //beispiel Erweiterungsmethode
            DateTime dt = DateTime.Now;
            dt.GetKW();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is List<Volumeinfo> volumes)
            {
                var result = volumes.Where(x => x.ratingsCount > 0).Average(x => x.averageRating);
                MessageBox.Show($"Result: {result}");
            }
        }
    }

    public static class MeineErweiterungen
    {
        public static int GetKW(this DateTime dt) //<- erweitungerungsmethode 
        {
            return 15;
        }

    }

}
