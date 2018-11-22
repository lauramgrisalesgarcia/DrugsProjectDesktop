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
            ControlBox = false;
            GetQuestion();
            lblStudentName.Text = FormIngreso.userName;
        }
        string URL = "http://52.168.52.154/webapi/api/Preguntas";
        int cont = 0;
        int n = 18, p = 0;
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
                HttpResponseMessage response = await client.GetAsync(URL + "/GetPregunta");
                if (response.IsSuccessStatusCode)
                {
                    var lista = response.Content.ReadAsAsync<IList<Questions>>();
                    if (lista.Result.Count-1 >= cont)
                    {
                        lblPregunta.Text = lista.Result[cont].descripcion;
                        GetAnswer(lista.Result[cont].preguntaId);
                    }
                    else
                    {
                        MessageBox.Show("Gracias por responder las preguntas");
                        FormMenu frmMenu = new FormMenu();
                        frmMenu.StartPosition = FormStartPosition.CenterScreen;
                        frmMenu.Show();
                        this.Dispose(false);
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
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL + "/GetRespuesta");
                if (response.IsSuccessStatusCode)
                {
                    var lista = response.Content.ReadAsAsync<IList<Answer>>();
                    p = 0;
                    for (int i = 0; i < lista.Result.Count - 1; i++)
                    {
                        if (lista.Result[i].IdPregunta == preguntaId)
                        {
                            RadioButton radio = new RadioButton();
                            radio.Name = lista.Result[i].IdRespuesta.ToString();
                            radio.Text = lista.Result[i].Descripcion;
                            radio.Left = 5;
                            radio.Top = n;
                            groupBox1.Controls.Add(radio);
                            n += 30;
                            p++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha podido traer información");
                }
            }
        }
        
        private async void PostAnswer(int estudiante, int respuesta)
        {
            using (var client = new HttpClient())
            {
                StudentAnswer studentanswer = new StudentAnswer();
                studentanswer.estudianteId = estudiante;
                studentanswer.fecha = DateTime.Now;
                studentanswer.respuestaId = respuesta;

                var serializedUser = JsonConvert.SerializeObject(studentanswer);
                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                HttpResponseMessage result = await client.PostAsync(URL+"/PostRespuesta", content);
                if (!(result.IsSuccessStatusCode))
                {
                    MessageBox.Show("Error de conexión");
                }
            }
        }

        public void Clean()
        {
            foreach (RadioButton group in groupBox1.Controls.OfType<RadioButton>().Where((controlname) => controlname.Name.Contains("")))
            {
                groupBox1.Controls.Remove(group);
            }
            n = 18;
        }

        private void lblPregunta_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormMenu frmMenu = new FormMenu();
            frmMenu.StartPosition = FormStartPosition.CenterScreen;
            frmMenu.Show();
            this.Dispose(false);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            RadioButton option = groupBox1.Controls.OfType<RadioButton>().Where(pre => pre.Checked).SingleOrDefault<RadioButton>();
            if (option == null)
            {
                MessageBox.Show("Debe seleccionar una respuesta antes de continuar");
            }
            else
            {
                int selected = Int16.Parse(option.Name);
                PostAnswer(FormIngreso.userId, selected);
                cont++;
                for (int i = 0; i <= p; i++)
                {
                    Clean();
                }                
                GetQuestion();
                btnBack.Dispose();
            }
                        
        }
    }
}
