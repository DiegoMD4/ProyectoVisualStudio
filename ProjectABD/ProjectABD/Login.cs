using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace ProjectABD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");

        private void button1_Click(object sender, EventArgs e)
        {
           
            ora.Open();
            OracleCommand comando = new OracleCommand("SELECT * FROM USUARIO WHERE NOM_USUARIO = :usuario AND CONTRASENA = :clave", ora);
            comando.Parameters.AddWithValue(":usuario", usuario.Text);
            comando.Parameters.AddWithValue(":clave", textBox2.Text);
            OracleDataReader lector = comando.ExecuteReader();

            string usu = this.usuario.Text;

            if (lector.Read())
            {
                Inicio ini = new Inicio(usu);
                ora.Close();

                ini.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No se encuentra el usuario");
                ora.Close();
            }
           
        }
    }

}
