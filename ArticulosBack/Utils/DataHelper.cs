﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Utils
{
    public class DataHelper
    {
        private static DataHelper instance;
        private SqlConnection _connection;
        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.ConnectionString);
        }

        public static DataHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new DataHelper();
            }
            return instance;
        }

        public DataTable ExecuteSP(string sp, List<SqlParameterss>? parameters)
        {
            DataTable table = new DataTable();
            SqlCommand? command = null;

            try
            {
                _connection.Open();
                command = new SqlCommand(sp, _connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                        command.Parameters.AddWithValue(param.Name, param.Value);
                }
                table.Load(command.ExecuteReader());
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error ejecutando el procedimiento almacenado: {ex.Message}");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return table;
        }

        public int SDML(string sp, List<SqlParameterss>? parameters)
        {
            int rows;

            try
            {
                _connection.Open();
                SqlCommand command = new SqlCommand(sp, _connection);
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                        command.Parameters.AddWithValue(param.Name, param.Value);
                }
                rows = command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception)
            {
                rows = 0;
                Console.WriteLine("Ninguna fila fue afectada...Error");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return rows;
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        //public int ExecuteTransaction(string sp, List<SqlParameterss>? parametros, SqlConnection connection, SqlTransaction transaction, object parameterOut = null)
        //{
        //    int filasAfectadas = 0;

        //    SqlCommand cmd = new SqlCommand(sp, _connection, transaction);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    if (parametros != null)
        //    {
        //        foreach (var param in parametros)
        //        {
        //            cmd.Parameters.AddWithValue(param.Name, param.Value.ToString());
        //        }

        //        if (parameterOut != null)
        //        {
        //            var outParam = new SqlParameter("outParam", SqlDbType.Int)
        //            {
        //                Direction = ParameterDirection.Output,
        //            };
        //            cmd.Parameters.Add(outParam);
        //        }
        //        try
        //        {
        //            filasAfectadas = cmd.ExecuteNonQuery();
        //            if (parameterOut != null)
        //            {
        //                parameterOut = cmd.Parameters["@outputParam"].Value;
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
        //        }
        //        finally
        //        {
        //            if (_connection != null && _connection.State == ConnectionState.Open)
        //            {
        //                _connection.Close();
        //            }
        //        }
        //    }
        //    return filasAfectadas;
        //}
    }
}
