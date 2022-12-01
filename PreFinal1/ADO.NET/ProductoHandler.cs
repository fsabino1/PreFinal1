using PreFinal1.Models;
using System.Data;
using System.Data.SqlClient;

namespace PreFinal1.ADO.NET
{
    public class ProductoHandler
    {
        private SqlConnection conexion;
        private string CadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=fsabinodb_PreFinal1;" +
            "User Id=fsabino_PreFinal1;" +
            "Password=fsabino1";

        public ProductoHandler()
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

        public List<Producto> GetProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            if (CadenaConexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Producto", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(reader["Id"].ToString());
                                producto.Descripciones = reader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(reader["Costo"].ToString());
                                producto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"].ToString());
                                producto.Stock = Convert.ToInt32(reader["Stock"].ToString());
                                producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                                listaProductos.Add(producto);
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
            return listaProductos;
        }
    }
}
