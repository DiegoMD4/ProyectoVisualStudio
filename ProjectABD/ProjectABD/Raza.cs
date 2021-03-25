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
    public partial class Raza : Form
    {
        OracleConnection ora = new OracleConnection("DATA SOURCE  = xe; PASSWORD = diegomd; USER ID = diego;");
        public Raza()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
                OracleCommand comando = new OracleCommand("Rza_Procedure1", ora);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.Add("esp", OracleType.VarChar).Value = this.textBox2.Text;
                comando.Parameters.Add("rza", OracleType.VarChar).Value = this.textBox1.Text;
                comando.Parameters.Add("lon", OracleType.Number).Value = Convert.ToInt32(this.textBox3.Text);

                comando.ExecuteNonQuery();
                MessageBox.Show("Agregado correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("Error o campos vacios");
            }

        }
    }
}
