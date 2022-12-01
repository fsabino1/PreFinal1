using PreFinal1.Models;
using System.Data;
using System.Data.SqlClient;

namespace PreFinal1.ADO.NET
{
    public class VentaHandler
    {
        private SqlConnection conexion;
        private string CadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=fsabinodb_PreFinal1;" +
            "User Id=fsabino_PreFinal1;" +
            "Password=fsabino1";

        public VentaHandler()
        {
            try
            {
                conexion = new SqlConnection(CadenaConexion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Venta> GetVenta()
        {
            List<Venta> listaVentas = new List<Venta>();
            if (CadenaConexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(reader["Id"].ToString());
                                venta.Comentarios = reader["Comentarios"].ToString();
                                venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                                listaVentas.Add(venta);
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return listaVentas;
        }
    }
}
