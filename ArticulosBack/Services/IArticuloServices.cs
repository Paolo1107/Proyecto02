using ArticulosBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Services
{
    public interface IArticuloServices
    {
        List<Articulo> GetAllArticulos();

        bool AddArticulos(Articulo art);

        public bool UpdateArticulos(Articulo art);
       

        bool DeleteArticulos(int art);

    }
}
