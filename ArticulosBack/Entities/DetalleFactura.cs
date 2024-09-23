using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Entities
{
    public class DetalleFactura
    {
        //Properties
        public int Id { get; set; }
        public int NroFactura { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }

    }
}
