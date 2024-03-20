using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Xml;
using System.Security.Policy;
using System.Reflection.Emit;

namespace TRANSLATE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public String Translate(String word)
        {
            var toLanguage = "tr";//English
            var fromLanguage = "en";//Deutsch
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={Uri.EscapeDataString(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }

        public bool test()
        {
            string adres = "https://www.google.com";
            try
            {
                WebRequest istek = WebRequest.Create(adres);
                WebResponse yanit = istek.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public WebBrowser webBrowser1 = new WebBrowser();

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Title = "Metin Dosyası Seçiniz";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            string[] dosyaicerik = System.IO.File.ReadAllLines(dosyayolu);
            listBox1.Items.AddRange(dosyaicerik);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (test())
            {
                this.Text = "TRANSLATE (BAĞLI)";
            }
            else
            {
                this.Text = "TRANSLATE (BAĞLI DEĞİL)";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://translate.google.com.tr/#tr/en/");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://translate.google.com.tr/#en/tr/");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().Contains(textBox1.Text))
                {
                    if (!listBox2.Items.Contains(listBox1.Items[i]))
                    {
                        listBox2.Items.Add(listBox1.Items[i]);
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = listBox2.SelectedItem.ToString();
            string translatedText = Translate(selectedText);
            listBox3.Items.Add(translatedText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog dosya = new SaveFileDialog();
            dosya.Title = "Metin Dosyası Kaydet";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            StreamWriter sw = new StreamWriter(dosyayolu);

            foreach (var item in listBox2.SelectedItems)
            {
                sw.WriteLine(item.ToString());
            }

            foreach (var item in listBox3.SelectedItems)
            {
                sw.WriteLine(item.ToString());
            }

            sw.Close();
        }
    }
}

