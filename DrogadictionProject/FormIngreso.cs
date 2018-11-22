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
        public static int userId { get; set; }
        public static string usuario { get; set; }
        public static string userName { get; set; }

        string URL = "http://52.168.52.154/webapi/api/";
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
            usuario = user;
            login.Name = user;
            login.Password = pass;

            using (var client = new HttpClient())
            {
                var serializedUser = JsonConvert.SerializeObject(login);
                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                HttpResponseMessage result = await client.PostAsync(URL+ "Users/Post", content);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.Headers.ContentLength == 4)
                    {                        
                        GetUser();                        
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
            }
        }

        public async void GetUser()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL + "Usuarios/GetUsuario?usuario=" + usuario);
                if (response.IsSuccessStatusCode)
                {
                    string lista = response.Content.ReadAsStringAsync().Result.Replace("\\","").Trim(new char[1] { '"' });                    
                    var usuario = JsonConvert.DeserializeObject<Student>(lista);
                    userId = usuario.Id;
                    userName = usuario.Nombre;
                    FormMenu frmMenu = new FormMenu();
                    frmMenu.StartPosition = FormStartPosition.CenterScreen;
                    frmMenu.Show();
                    this.Dispose(false);
                }
                else
                {
                    MessageBox.Show("No se ha podido traer información");
                }
            }
        }
    }
}
