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
    public partial class Pantalla_usuarios : Form
    {
        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");
        public Pantalla_usuarios()
        {
            InitializeComponent();
        }

        private void Pantalla_usuarios_Load(object sender, EventArgs e)
        {
            ora.Open();
            OracleCommand abrir = new OracleCommand("seleccionaUsuario", ora);
            abrir.CommandType = System.Data.CommandType.StoredProcedure;
            abrir.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adap = new OracleDataAdapter();
            adap.SelectCommand = abrir;
            DataTable tabla = new DataTable();
            adap.Fill(tabla);
            dataGridView1.DataSource = tabla;

            ora.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                this.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                this.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("new_user", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("usu", OracleType.VarChar).Value = this.textBox1.Text;
                comando.Parameters.Add("cont", OracleType.VarChar).Value = this.textBox2.Text;

                comando.ExecuteNonQuery();
                MessageBox.Show("Agregado correctamente");

                //Para actualizar la tabla 
                OracleCommand abrir = new OracleCommand("seleccionaUsuario", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
            catch (Exception)
            {

                MessageBox.Show("Error o campos vacios"); ;
            }
            ora.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("delete_user", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("idu", OracleType.Number).Value = Convert.ToInt32(this.textBox4.Text);
                comando.ExecuteNonQuery();

                MessageBox.Show("Eliminado correctamente");
                //Para actualizar la tabla 
                OracleCommand abrir = new OracleCommand("seleccionaUsuario", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
            catch (Exception)
            {

                MessageBox.Show("Error al eliminar");
            }
            ora.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
