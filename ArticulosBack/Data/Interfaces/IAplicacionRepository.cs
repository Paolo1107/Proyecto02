using ArticulosBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Data.Interfaces
{
    public interface IAplicacionRepository
    {
        bool Registrar(Factura factura);
        List<Factura> Consultar (DateTime? fecha, int? formaPago);
        bool Actualizar (int id, Factura factura);

        List<Factura> GetAllF();
    }
}
