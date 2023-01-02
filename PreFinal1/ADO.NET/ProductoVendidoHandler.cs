using PreFinal1.Models;
using System.Data;
using System.Data.SqlClient;


namespace PreFinal1.ADO.NET
{
    public class ProductoVendidoHandler
    {
        private SqlConnection conexion;
        private string CadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=fsabinodb_PreFinal1;" +
            "User Id=fsabino_PreFinal1;" +
            "Password=fsabino1";

        public ProductoVendidoHandler()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
            }
        }

        public ProductoVendido obtenerProdVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido productoVdo = new ProductoVendido();
            productoVdo.id = long.Parse(reader["Id"].ToString());
            productoVdo.idProducto = long.Parse(reader["IdProducto"].ToString());
            productoVdo.idVenta = long.Parse(reader["IdVenta"].ToString());
            productoVdo.stock = int.Parse(reader["Stock"].ToString());
            return productoVdo;
        }
        // Cargar una venta en la tabla venta, cargar la venta el ProductoVendido y descontar stock en tabla Producto
        public ProductoVendido cargarProductosVendidos(ProductoVendido productoV)
        {
            ProductoRepository repo = new ProductoRepository();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido (IdProducto, Stock, IdVenta) VALUES (@idProducto, @stock, @idVenta); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoV.idProducto });
                    Producto prodComparar = repo.obtnerProductoDesdeID(productoV.idProducto);

                    if (productoV.stock >= prodComparar.stock)
                    {
                        Producto productoADescontar = repo.descontarStock(productoV.stock, productoV.idProducto);
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = prodComparar.stock });
                    }
                    else
                    {
                        Producto productoADescontar = repo.descontarStock(productoV.stock, productoV.idProducto);
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productoV.stock });
                    }

                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoV.idVenta });
                    productoV.id = long.Parse(cmd.ExecuteScalar().ToString());
                    return productoV;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }


        // Eliminar producto vendido desde IdVenta
        public bool eliminarProductoVendido(long idVta)
        {
            ProductoRepository repo = new ProductoRepository();
            List<ProductoVendido> productoRef = obtenerProdVendidoIdVta(idVta);
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                {
                    conexion.Open();

                    foreach (ProductoVendido pv in productoRef)
                    {
                        Producto productoAModificar = repo.sumarStock(pv.stock, pv.idProducto);

                    }
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVta });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }

                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

        }

        //Obtener producto vendido desde IdVenta
        public List<ProductoVendido> obtenerProdVendidoIdVta(long idVenta)
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            if (conexion == null) { throw new Exception("Conexión no establecida"); }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVenta });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido producto = obtenerProdVendidoDesdeReader(reader);
                                lista.Add(producto);
                            }

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

        //Obtener productos vendidos desde IdUsuario/sumar stock
        public List<ProductoVendido> obtenerProductoVendidoDesdeIdUser(long idVta)
        {
            ProductoRepository repo = new ProductoRepository();
            List<ProductoVendido> productoRef = obtenerProdVendidoIdVta(idVta);
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                {
                    conexion.Open();

                    foreach (ProductoVendido pv in productoRef)
                    {
                        Producto productoAModificar = repo.sumarStock(pv.stock, pv.idProducto);

                    }
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVta });

                }

                return productoRef;
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

        }

        //obtener productos vendidos desde IdUsuario y los productos

        public List<ProductoVendido> obtenerListaProductoVendidoDesdeIdUser(long idVta)
        {
            ProductoRepository repo = new ProductoRepository();
            List<ProductoVendido> productoRef = obtenerProdVendidoIdVta(idVta);
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                {
                    conexion.Open();

                    foreach (ProductoVendido pv in productoRef)
                    {
                        Producto productoAModificar = repo.obtnerProductoDesdeID(pv.idProducto);

                    }
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVta });

                }

                return productoRef;
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

        }
    }
}
