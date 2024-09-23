using ArticulosBack.Data.Interfaces;
using ArticulosBack.Entities;
using ArticulosBack.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ArticulosBack.Data.Implementations
{
    public class AplicacionRepository : IAplicacionRepository
    {
        private readonly SqlConnection _connection;
        public AplicacionRepository()
        {
            _connection = DataHelper.GetInstance().GetConnection();
        }

        public bool Actualizar(int id, Factura factura)
        {
            SqlCommand cmd = new SqlCommand("SP_ActualizarFactura", _connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nroFactura", id);
            cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
            cmd.Parameters.AddWithValue("@formaPago", factura.FormaPago.Id);
            cmd.Parameters.AddWithValue("@cliente", factura.Cliente);

            _connection.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            _connection.Close();

            return filasAfectadas > 0;
        }

        public List<Factura> Consultar(DateTime? fecha, int? formaPagoId)
        {
            List<Factura> facturas = new List<Factura>();

            // Lista de parámetros para el procedimiento almacenado
            List<SqlParameterss> parametros = new List<SqlParameterss>
    {
        new SqlParameterss("@fecha", fecha),
        new SqlParameterss("@formaPagoId", formaPagoId)
    };

            DataTable table = DataHelper.GetInstance().ExecuteSP("SP_ObtenerFacturas", parametros);

            foreach (DataRow row in table.Rows)
            {
                var factura = new Factura
                {
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Cliente = row["Cliente"].ToString(),
                    FormaPago = new FormaPago
                    {
                        Id = Convert.ToInt32(row["formaPago"]),
                      
                    }
                };

                facturas.Add(factura);
            }

            return facturas;
        }

        public List<Factura> GetAllF()
        {
            var lsFactura = new List<Factura>();
            var dataHelp = DataHelper.GetInstance();
            var table = dataHelp.ExecuteSP("sp_ObtenerTodasLasFacturas", null);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    Factura oFactura = new Factura()
                    {
                        NroFactura = Convert.ToInt32(row["nroFactura"]),
                        Fecha = Convert.ToDateTime(row["fecha"]),
                        Cliente = row["cliente"].ToString(),
                        FormaPago = new FormaPago
                        {
                            Id = Convert.ToInt32(row["formaPagoId"]),
                            Nombre = row["nombreFormaPago"].ToString() // Asegúrate de que el nombre del campo coincida con el alias en la consulta
                        }
                    };
                    lsFactura.Add(oFactura);
                }
            }
            return lsFactura;
        }

        public bool Registrar(Factura factura)
        {
            SqlCommand cmd = new SqlCommand("SP_InsertarFactura", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
            cmd.Parameters.AddWithValue("@formaPago", factura.FormaPago.Id);
            cmd.Parameters.AddWithValue("@cliente", factura.Cliente);

            SqlParameter param = new SqlParameter("@nroFactura", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(param);

            _connection.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            int nroFacturaCreado = Convert.ToInt32(param.Value);
            _connection.Close();

            return filasAfectadas > 0;

        }
    }
}