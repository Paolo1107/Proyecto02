using ArticulosBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Services
{
    public interface IAplicacionService
    {
        bool AgregarFactura(Factura factura);
        List<Factura> ConsultarFacturas(DateTime? fecha, int? formaPago);
        bool ActualizarFactura(int id, Factura factura);

        List<Factura> GetAllFacturas();
    }
}
