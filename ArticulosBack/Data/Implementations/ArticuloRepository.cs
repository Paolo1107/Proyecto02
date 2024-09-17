using ArticulosBack.Data.Repositories;
using ArticulosBack.Entities;
using ArticulosBack.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Data.Implementations
{
    public class ArticuloRepository : IArticuloRepository
    {
        public List<Articulo> GetArticulos()
        {
            List<Articulo> lstArticulo = new List<Articulo>();
            DataHelper help = DataHelper.GetInstance();
            DataTable tabla = help.ExecuteSP("SP_ObtenerTodosLosArticulos", null);

            foreach (DataRow row in tabla.Rows)
            {
                Articulo articulo = new Articulo
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Nombre = row["nombre"].ToString(),
                    PrecioUnitario = Convert.ToDecimal(row["precioUnitario"])
                };

                lstArticulo.Add(articulo);
            }
            return lstArticulo;

        }

        public bool Add(Articulo articulo)
        {
            bool result;
            List<SqlParameterss> parameters = new List<SqlParameterss>()
            {
                new SqlParameterss("@nombre", articulo.Nombre),
                new SqlParameterss("@precioUni", articulo.PrecioUnitario)
            };

            result = DataHelper.GetInstance().SDML("SP_INSERTAR_ARTICULOS", parameters) > 0;
            return result;
        }

        public bool Update(Articulo articulo)
        {
            bool result = true;
            List<SqlParameterss> parametersses = new List<SqlParameterss>
            {
                new SqlParameterss("@id", articulo.Id),
                new SqlParameterss("@nombre", articulo.Nombre),
                new SqlParameterss("@precioUni", articulo.PrecioUnitario)
            };

            result = DataHelper.GetInstance().SDML("SP_ACTUALIZAR_ARTICULOS", parametersses) > 0;
            return result;

        }

        public bool Delete(int articulo)
        {
            bool result;
            List<SqlParameterss> parameters = new List<SqlParameterss>
            {
                new SqlParameterss("@id", articulo)
            };

            result = DataHelper.GetInstance().SDML("SP_ELIMINAR_ARTICULOS", parameters) > 0;
            return result;

        }

        
    }
}
