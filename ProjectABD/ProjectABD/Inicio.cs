using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProjectABD
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            abrirformhija(new Pacientes());
        }

        private void abrirformhija(object formhija)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(fh);
            this.panel2.Tag = fh;
            fh.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            abrirformhija(new Pacientes());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            abrirformhija(new Doctores());
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            abrirformhija(new Duenos());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            abrirformhija(new Citas());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            abrirformhija(new Cirugia());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            abrirformhija(new Medicinas());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.max.Visible = false;
            this.res.Visible = true;
        }

        private void res_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.max.Visible = true;
            this.res.Visible = false;
        }

        private void min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMs, int wParam, int lParam);

        private void Inicio_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hora.Text = DateTime.Now.ToLongTimeString();
        }

        public Inicio(string text)
        {
            InitializeComponent();
            lblusuario.Text = text;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Pantalla_usuarios pu = new Pantalla_usuarios();
            pu.Show();
        }
    }
}
