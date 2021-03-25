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
    public partial class Doctores : Form
    {
        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");
        
        public Doctores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
                OracleCommand comando3 = new OracleCommand("DOC_PROCEDURE", ora);
                OracleCommand comando4 = new OracleCommand("CELMEDICO_", ora);

                comando3.CommandType = System.Data.CommandType.StoredProcedure;
                comando3.Parameters.Add("codmed", OracleType.Number).Value = Convert.ToInt32(this.txtcodmed.Text);
                comando3.Parameters.Add("NOMMED", OracleType.VarChar).Value = this.txtnombmed.Text;
                comando3.Parameters.Add("dir", OracleType.VarChar).Value = this.dir.Text;
                comando3.Parameters.Add("TELM", OracleType.Number).Value = Convert.ToInt32(this.txtelcasa.Text);
                comando3.Parameters.Add("ingre", OracleType.DateTime).Value = this.dateTimePicker1.Text;
                comando3.Parameters.Add("emer", OracleType.VarChar).Value = this.txtemer.Text;

                comando4.Parameters.Add("codmed", OracleType.Number).Value = Convert.ToInt32(this.txtcodmed.Text);
                comando4.Parameters.Add("TELC", OracleType.Number).Value = Convert.ToInt32(this.txtcelular.Text);

                comando3.ExecuteNonQuery();
                MessageBox.Show("Correcto");

                //Actualiza despues del agregado
                OracleCommand abrir = new OracleCommand("SELECTDOCTORES", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("doc", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
            catch (Exception)
            {

                MessageBox.Show("Error de ingresado");
            }
            ora.Close();
            
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Doctores_Load(object sender, EventArgs e)
        {
            ora.Open();
            OracleCommand abrir = new OracleCommand("SELECTDOCTORES", ora);
            abrir.CommandType = System.Data.CommandType.StoredProcedure;
            abrir.Parameters.Add("doc", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adap = new OracleDataAdapter();
            adap.SelectCommand = abrir;
            DataTable tabla = new DataTable();
            adap.Fill(tabla);
            dataGridView1.DataSource = tabla;

            ora.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.txtcodmed.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                this.txtnombmed.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                this.dir.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                this.txtemer.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                this.txtelcasa.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                this.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                this.txtcelular.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("delete_medicos", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("codmed", OracleType.Number).Value = Convert.ToInt32(this.txtcodmed.Text);
                comando.Parameters.Add("nomed", OracleType.VarChar).Value = this.txtnombmed.Text;
                comando.ExecuteNonQuery();

                MessageBox.Show("Eliminado correctamente");
                //Para actualizar la tabla 
                OracleCommand abrir = new OracleCommand("SELECTDOCTORES", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("doc", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;
                throw;
            }
        }
    }
}
