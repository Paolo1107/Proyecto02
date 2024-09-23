using ArticulosBack.Data.Implementations;
using ArticulosBack.Data.Interfaces;
using ArticulosBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Services
{
    public class AplicacionService : IAplicacionService
    {
        private readonly IAplicacionRepository _aplicacionService;
        public AplicacionService()
        {
            _aplicacionService = new AplicacionRepository();
        }
        public bool AgregarFactura(Factura factura)
        {
            return _aplicacionService.Registrar(factura);
        }

        public List<Factura> ConsultarFacturas(DateTime? fecha, int? formaPago)
        {
            return _aplicacionService.Consultar(fecha, formaPago);
        }
        public bool ActualizarFactura(int id, Factura factura)
        {
            return _aplicacionService.Actualizar(id, factura);
        }



        public List<Factura> GetAllFacturas()
        {
            return _aplicacionService.GetAllF();
        }
    }
}
