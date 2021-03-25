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
    public partial class Cirugia : Form
    {
        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");
        public Cirugia()
        {
            InitializeComponent();
        }

        private void Cirugia_Load(object sender, EventArgs e)
        {
            ora.Open();

            OracleCommand abrir = new OracleCommand("SELECTCIRUGIA", ora);
            abrir.CommandType = System.Data.CommandType.StoredProcedure;
            abrir.Parameters.Add("cir", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adap = new OracleDataAdapter();
            adap.SelectCommand = abrir;
            DataTable tabla = new DataTable();
            adap.Fill(tabla);
            dataGridView1.DataSource = tabla;
            ora.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        { //Agregado
           
            try
            {
                OracleCommand cirugia = new OracleCommand("CIRUGIA_PROCEDURE", ora);
                cirugia.CommandType = System.Data.CommandType.StoredProcedure;

                cirugia.Parameters.Add("ncir", OracleType.VarChar).Value = this.textBox1.Text;
                cirugia.Parameters.Add("nompaciente", OracleType.VarChar).Value = this.textBox2.Text;
                cirugia.Parameters.Add("estim", OracleType.VarChar).Value = this.Duracion.Text;
                cirugia.Parameters.Add("descir", OracleType.VarChar).Value = this.textBox3.Text;
                cirugia.Parameters.Add("ries", OracleType.VarChar).Value = this.Riesgo.Text;
                cirugia.Parameters.Add("ane", OracleType.VarChar).Value = this.Anestesia.Text;


                OracleCommand abrir = new OracleCommand("SELECTCIRUGIA", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("cir", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;


                cirugia.ExecuteNonQuery();
                MessageBox.Show("Agregado correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("Error o campos vacios");
            }
            ora.Close();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Eliminar
            try
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("delete_cirugia", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("ncir", OracleType.VarChar).Value = this.textBox1.Text;
                comando.Parameters.Add("nompaciente", OracleType.VarChar).Value = this.textBox2.Text;

                comando.ExecuteNonQuery();
                MessageBox.Show("Eliminado correctamente");

            }
            catch (Exception)
            {
                
                MessageBox.Show("Error");
            }
            ora.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
