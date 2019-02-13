using App.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.UI.Desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;

            label1.Text = ConfigurationManager.AppSettings["IGV"];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Creando instancia del objeto factura
            Factura documento = new Factura();
            documento.NroSerie = "001";
            documento.TipoDocumento = "01";
            documento.FechaCreacion = DateTime.Now;

            Documento documento2 = new Factura();
            //documento2 = documento; //Ejemplo con valor por referencia ByRef
            documento2.NroSerie = "002";
            documento2.TipoDocumento = "03";
            documento2.FechaCreacion = DateTime.Now;
            var calculo = documento2.CalcularTotalNeto();

            Documento boleta = new Boleta();
            boleta.NroSerie = "001";
            boleta.TipoDocumento = "01";
            boleta.FechaCreacion = DateTime.Now;
            var calculo2 = boleta.CalcularTotalNeto();
        }
    }
}
