using ArticulosBack.Data.Implementations;
using ArticulosBack.Data.Repositories;
using ArticulosBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Services
{
    public class ArticuloManager : IArticuloServices
    {
        IArticuloRepository _articuloRepository;
        public ArticuloManager()
        {
            _articuloRepository = new ArticuloRepository();
        }

        public List<Articulo> GetAllArticulos()
        {
            return _articuloRepository.GetArticulos();
        }

        public bool AddArticulos(Articulo art)
        {
            return _articuloRepository.Add(art);
        }

        public bool UpdateArticulos(Articulo art)
        {
            return _articuloRepository.Update(art);
        }

        public bool DeleteArticulos(int art)
        {
            return _articuloRepository.Delete(art);
        }

      
    }
}
