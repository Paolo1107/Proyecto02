using ArticulosBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Data.Repositories
{
    public interface IArticuloRepository
    {
        List<Articulo> GetArticulos();

        bool Add(Articulo articulo);

        bool Update(Articulo articulo);
        

        bool Delete(int articulo);

    }
}
