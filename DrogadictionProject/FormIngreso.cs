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
    
    public partial class FormIngreso : Form
    {
        public FormIngreso()
        {
            InitializeComponent();
        }
        public static int userId { get; set; } = 3;
        string URL = "http://52.168.52.154/webapi/api/Users/Post";
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
                HttpResponseMessage result = await client.PostAsync(URL, content);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.Headers.ContentLength == 4)
                    {
                        MessageBox.Show("Bienvenido");
                        FormEncuesta frmEncuesta = new FormEncuesta();                        
                        frmEncuesta.StartPosition = FormStartPosition.CenterScreen;
                        frmEncuesta.Show();
                        this.Dispose(false);
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                    }
                }
                else
                {
                    MessageBox.Show("Error de conexión");
                }
                Console.WriteLine(result.Content.Headers.ContentLength);   
            }
        }

        public async void GetUser()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL + "/GetUser");
                if (response.IsSuccessStatusCode)
                {
                    var lista = response.Content.ReadAsAsync<IList<User>>();
                    userId = lista.Result[0].userId;
                }
                else
                {
                    MessageBox.Show("No se ha podido traer información");
                }
            }
        }
    }
}
