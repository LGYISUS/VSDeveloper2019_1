using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Base
{
    public class Documento
    {
        public string TipoDocumento { get; set; }
        public string NroSerie { get; set; }
        public DateTime FechaCreacion { get; set; }
        protected decimal Total { get; set; }
        public virtual decimal CalcularTotalNeto()
        {
            return 0;
        }
    }
}
