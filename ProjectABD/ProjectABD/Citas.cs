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
    public partial class Citas : Form
    {
        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");
        public Citas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("CITAS_PROCEDURE", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.Add("nompaciente", OracleType.VarChar).Value = this.textBox1.Text;
                comando.Parameters.Add("nommed", OracleType.VarChar).Value = this.textBox2.Text;
                comando.Parameters.Add("fech", OracleType.DateTime).Value = this.dateTimePicker1.Text;
                comando.Parameters.Add("hor", OracleType.DateTime).Value = this.dateTimePicker2.Text;
                comando.Parameters.Add("descr", OracleType.VarChar).Value = this.textBox3.Text;
                comando.Parameters.Add("fechpro", OracleType.DateTime).Value = this.dateTimePicker3.Text;
                comando.Parameters.Add("cir", OracleType.VarChar).Value = this.textBox4.Text;

                comando.ExecuteNonQuery();
                MessageBox.Show("Agregado correctamente");

                OracleCommand abrir = new OracleCommand("SELECTCITAS", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("cit", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
            catch (Exception)
            {

                MessageBox.Show("Error o campos vacíos");
            }
            ora.Close();
        }

        private void Citas_Load(object sender, EventArgs e)
        {
            ora.Open();
            OracleCommand abrir = new OracleCommand("SELECTCITAS", ora);
            abrir.CommandType = System.Data.CommandType.StoredProcedure;
            abrir.Parameters.Add("cit", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adap = new OracleDataAdapter();
            adap.SelectCommand = abrir;
            DataTable tabla = new DataTable();
            adap.Fill(tabla);
            dataGridView1.DataSource = tabla;
            ora.Close();
        }
    }
}
