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
    public partial class Pacientes : Form
    {
        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");
        public Pacientes()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
              //  OracleCommand comando = new OracleCommand("INDUENO", ora);
                //OracleCommand comando2 = new OracleCommand("TELDUE", ora);
                OracleCommand comando3 = new OracleCommand("PAC_PROCEDURE", ora);

                comando3.CommandType = System.Data.CommandType.StoredProcedure;
               comando3.Parameters.Add("npaciente", OracleType.VarChar).Value = this.txtnompaciente.Text;
                comando3.Parameters.Add("codmed", OracleType.Number).Value = Convert.ToInt32(this.txtcodmed.Text);
                comando3.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);
                comando3.Parameters.Add("fechapri", OracleType.DateTime).Value = this.dateTimePicker1.Value;
                comando3.Parameters.Add("rza", OracleType.VarChar).Value = this.txtraza.Text;


                //INSERT A DUENO
              /*  comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);
                comando.Parameters.Add("NDUENO", OracleType.VarChar).Value = this.textBox6.Text;
                comando.Parameters.Add("RES", OracleType.VarChar).Value = this.textBox7.Text;

                //INSERT A TELEFONODUENO
                comando2.CommandType = System.Data.CommandType.StoredProcedure;
                comando2.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);
                comando2.Parameters.Add("TELD", OracleType.Number).Value = Convert.ToInt32(this.txteld1.Text);
                */


               // comando.ExecuteNonQuery();
                //comando2.ExecuteNonQuery();
                comando3.ExecuteNonQuery();
                MessageBox.Show("Insertado correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo ingresar campos vacios/ error de ingresado");
            }
            ora.Close();
        }
    }
}
