using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrogadictionProject
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            lblStudentName.Text = FormIngreso.userName;
            ControlBox = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormIngreso frmIngreso = new FormIngreso();
            frmIngreso.StartPosition = FormStartPosition.CenterScreen;
            frmIngreso.Show();
            this.Dispose(false);
        }

        private void encuestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEncuesta frmEncuesta = new FormEncuesta();
            frmEncuesta.StartPosition = FormStartPosition.CenterScreen;
            frmEncuesta.Show();
            this.Close();
        }
    }
}
