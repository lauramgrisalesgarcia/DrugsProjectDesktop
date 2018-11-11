using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DrogadictionProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string URL = "localhost/Drogadiccion/api/Users";
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login(textBox1.Text, textBox2.Text);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Login(string user, string pass)
        {
            Login login = new Login();
            login.Name = user;
            login.Password = pass;

            using (var client = new HttpClient())
            {
                var serializedUser = JsonConvert.SerializeObject(login);
                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URL, content);
            }
        }
    }
}
