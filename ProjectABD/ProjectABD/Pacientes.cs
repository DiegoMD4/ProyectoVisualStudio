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
                OracleCommand comando = new OracleCommand("INDUENO", ora);
                OracleCommand comando2 = new OracleCommand("TELDUE", ora);
                OracleCommand comando3 = new OracleCommand("PAC_PROCEDURE", ora);
                OracleCommand especie = new OracleCommand("ESP_PROCEDURE", ora);

                especie.CommandType = System.Data.CommandType.StoredProcedure;
                especie.Parameters.Add("esp", OracleType.VarChar).Value = this.txtespecie.Text;
                especie.Parameters.Add("FAMI", OracleType.VarChar).Value = this.txtFam.Text;
                especie.Parameters.Add("NPACIENTE", OracleType.VarChar).Value = this.txtnompaciente.Text;

                comando3.CommandType = System.Data.CommandType.StoredProcedure;
                comando3.Parameters.Add("npaciente", OracleType.VarChar).Value = this.txtnompaciente.Text;
                comando3.Parameters.Add("codmed", OracleType.Number).Value = Convert.ToInt32(this.txtcodmed.Text);
                comando3.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);
                comando3.Parameters.Add("rza", OracleType.VarChar).Value = this.txtraza.Text;
                comando3.Parameters.Add("fech", OracleType.DateTime).Value = this.dateTimePicker1.Text;
             //   comando3.Parameters.Add("foto", OracleType.Blob).Value = Convert.ToByte(this.pictureBox1.Image);


                //INSERT A DUENO
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);
                comando.Parameters.Add("NDUENO", OracleType.VarChar).Value = this.textBox6.Text;
                comando.Parameters.Add("RES", OracleType.VarChar).Value = this.textBox7.Text;

                //INSERT A TELEFONODUENO
                comando2.CommandType = System.Data.CommandType.StoredProcedure;
                comando2.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);
                comando2.Parameters.Add("TELD", OracleType.Number).Value = Convert.ToInt32(this.txteld1.Text);


                especie.ExecuteNonQuery();
                comando.ExecuteNonQuery();
                comando2.ExecuteNonQuery();
                comando3.ExecuteNonQuery();
                MessageBox.Show("Insertado correctamente");
                //Actualiza la tabla con los nuevos elementos
                OracleCommand abrir = new OracleCommand("SELECTPACIENTES", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("pac", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adap = new OracleDataAdapter();
                adap.SelectCommand = abrir;
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                dataGridView1.DataSource = tabla;

            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
            ora.Close();
        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            ora.Open();
            OracleCommand abrir = new OracleCommand("SELECTPACIENTES", ora);
            abrir.CommandType = System.Data.CommandType.StoredProcedure;
            abrir.Parameters.Add("pac", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adap = new OracleDataAdapter();
            adap.SelectCommand = abrir;
            DataTable tabla = new DataTable();
            adap.Fill(tabla);
            dataGridView1.DataSource = tabla;

            ora.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        { //Eliminar 
            try
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("delete_pacientes", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("nombpac", OracleType.VarChar).Value = this.txtnompaciente.Text;
                comando.Parameters.Add("dnid", OracleType.Number).Value = Convert.ToInt32(this.textBox2.Text);

                OracleCommand comando1 = new OracleCommand("delete_especie", ora);
                comando1.CommandType = System.Data.CommandType.StoredProcedure;
                comando1.Parameters.Add("n_esp", OracleType.VarChar).Value = this.txtespecie.Text;
                comando1.Parameters.Add("nombpac", OracleType.VarChar).Value = this.txtnompaciente.Text;

                comando1.ExecuteNonQuery();
                comando.ExecuteNonQuery();

                MessageBox.Show("Eliminado correctamente");
                //Para actualizar la tabla 
                OracleCommand abrir = new OracleCommand("SELECTPACIENTES", ora);
                abrir.CommandType = System.Data.CommandType.StoredProcedure;
                abrir.Parameters.Add("pac", OracleType.Cursor).Direction = ParameterDirection.Output;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.txtnompaciente.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                this.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                this.textBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                this.txteld1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                this.txtcodmed.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                this.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                this.textBox4.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                this.txtraza.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                this.txtespecie.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                this.txtFam.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                this.textBox7.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox4.Text = openFileDialog1.FileName;
                this.pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
