using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrogadictionProject
{
    public partial class FormEncuesta : Form
    {
        public FormEncuesta()
        {
            InitializeComponent();
            GetQuestion();            
        }
        string URL = "http://52.168.52.154/webapi/api/Preguntas/";
        int cont = 0;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormEncuesta_Load(object sender, EventArgs e)
        {

        }

        private async void GetQuestion()
        {
            using (var client = new HttpClient())
            {
                List<Questions> listpre = new List<Questions>();
                HttpResponseMessage response = await client.GetAsync(URL + "/GetPregunta");
                if (response.IsSuccessStatusCode)
                {
                    var lista = response.Content.ReadAsAsync<IList<Questions>>();
                    if (lista.Result.Count-1 > cont)
                    {
                        lblPregunta.Text = lista.Result[cont].descripcion;
                        GetAnswer(lista.Result[cont].preguntaId);
                    }
                    else
                    {
                        MessageBox.Show("Gracias por responder las preguntas");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha podido traer información");
                }
            }
        }

        public async void GetAnswer(int preguntaId)
        {
            var n = 20;
            using (var client = new HttpClient())
            {
                List<Answer> listpre = new List<Answer>();
                HttpResponseMessage response = await client.GetAsync(URL + "/GetRespuesta");
                if (response.IsSuccessStatusCode)
                {
                    var lista = response.Content.ReadAsAsync<IList<Answer>>();
                    //for (int i = 0; i < lista.Result.Count - 1; i++)
                    for (int i = 0; i < 2; i++, n+=30)
                    {
                        if (lista.Result[i].preguntaId == preguntaId)
                        {
                            RadioButton radio = new RadioButton();
                            radio.Name = lista.Result[i].respuestaId.ToString();
                            radio.Text = lista.Result[i].descripcion;
                            radio.Left = 5;
                            radio.Top = n;
                            groupBox1.Controls.Add(radio);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha podido traer información");
                }
            }
        }
        private async void Answer(int estudiante, int respuesta)
        {
            //Answer answer = new Answer();
            //answer.estudianteId = estudiante;
            //answer.fecha = DateTime.Now;
            //answer.respuestaId = respuesta;

            //using (var client = new HttpClient())
            //{
            //    var serializedUser = JsonConvert.SerializeObject(answer);
            //    var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            //    HttpResponseMessage result = await client.PostAsync(URL, content);
            //    if (result.IsSuccessStatusCode)
            //    {
            //        //if ()
            //        //{
                        
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("No se ha almacenado la respuesta");
            //        //}
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error de conexión");
            //    }
            //    Console.WriteLine(result.Content.Headers.ContentLength);
            //}
        }

        public void Clean()
        {
            
        }

        private void lblPregunta_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            cont++;
            GetQuestion();
        }
    }
}
