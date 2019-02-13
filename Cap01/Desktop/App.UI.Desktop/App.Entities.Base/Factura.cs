using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Base
{
    public class Factura : Documento
    {
        public decimal IGV { get; set; }
        public decimal SubTotal { get; set; }
        public  override decimal CalcularTotalNeto()
        {
            var resultado = ((IGV * 18)/100)+SubTotal;
            return resultado;
        }
    }
}
