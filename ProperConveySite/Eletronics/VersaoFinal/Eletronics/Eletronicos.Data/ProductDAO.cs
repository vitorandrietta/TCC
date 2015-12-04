using Eletronicos.Model.Product;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronicos.Data
{
    public class ProductDAO : IData<Product>
    {
        /// <summary>
        /// A variable that represents the connection with the database
        /// </summary>
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBELETRONICS"].ConnectionString);

        /// <summary>
        /// A variable that represents the result of a query in the database
        /// </summary>
        private SqlDataReader query;

        /// <summary>
        /// A variable that represents a command to be executed in the database
        /// </summary>
        private SqlCommand command;


        public void Insert(Product objectToBeInserted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_INSERT_Product @PRODUCTNAME,@PRODUCTDESCRIPTION,@AVAIABLEQUANTITY,@SUPPLIER");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@PRODUCTNAME", objectToBeInserted.ProductName);
                this.command.Parameters.AddWithValue("@PRODUCTDESCRIPTION", objectToBeInserted.ProductDescription);
                this.command.Parameters.AddWithValue("@AVAIABLEQUANTITY", objectToBeInserted.AvaiableQuantity);
                this.command.Parameters.AddWithValue("@SUPPLIER", objectToBeInserted.SupplierId);
                this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                this.connection.Close();
                this.command = null;
            }
        }

        public void Delete(Product objectToBeDeleted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_DELETE_PRODUCT @ProductId");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("ProductId", objectToBeDeleted.ProductID);
                this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                this.connection.Close();
                this.command = null;
            }

        }


        public void Update(Product objectToBeUpdated)
        {
            try
            {
                this.connection.Open();

                this.command = new SqlCommand("exec PR_Update_Product @ProductId,@PRODUCTNAME,@PRODUCTDESCRIPTION,@AVAIABLEQUANTITY,@SUPPLIER");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@PRODUCTNAME", objectToBeUpdated.ProductName);
                this.command.Parameters.AddWithValue("@PRODUCTDESCRIPTION", objectToBeUpdated.ProductDescription);
                this.command.Parameters.AddWithValue("@AVAIABLEQUANTITY", objectToBeUpdated.AvaiableQuantity);
                this.command.Parameters.AddWithValue("@SUPPLIER", objectToBeUpdated.SupplierId);
                this.command.Parameters.AddWithValue("@ProductId", objectToBeUpdated.ProductID);
                this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                this.connection.Close();
                this.command = null;
            }
        }

        public IList<Product> FindAll()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_Product_Select()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<Product> produtos = new List<Product>();
                while (this.query.Read())
                {
                    produtos.Add(new Product()
                    {
                        ProductID = this.query.GetInt32(0),
                        ProductName = this.query.GetString(1),
                        ProductDescription = this.query.GetString(2),
                        AvaiableQuantity = this.query.GetInt32(3),
                        SupplierId = this.query.GetString(4)

                    });
                }

                return produtos;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }

        public Product Find(Product objectToBeFound)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_Product_SelectSingle (@CD_ProductId)");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CD_ProductId", objectToBeFound.ProductID);
                this.query = this.command.ExecuteReader();
                Product produto = null;
                if (this.query.Read())
                {
                    produto = new Product()
                    {
                        ProductID = this.query.GetInt32(0),
                        ProductName = this.query.GetString(1),
                        ProductDescription = this.query.GetString(2),
                        AvaiableQuantity = this.query.GetInt32(3),
                        SupplierId = this.query.GetString(4)

                    };
                }

                return produto;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }

        public IList<Product> FindProductsByFilter(Product filterProduct)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_Product_ProductSelectByFilter @CD_ProductId,@NM_ProductName,@DS_ProductDescription,@QT_AvaiableQuantity,@CD_Supplier");

                if (filterProduct.SupplierId == null)
                {
                    filterProduct.SupplierId = "";
                }

                if (filterProduct.ProductName == null)
                {
                    filterProduct.ProductName = "";
                }

                if (filterProduct.ProductDescription == null)
                {
                    filterProduct.ProductDescription = "";
                }
                filterProduct.SupplierId = filterProduct.SupplierId.Replace(".", "").Replace("-", "");

                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CD_ProductId", filterProduct.ProductID);
                this.command.Parameters.AddWithValue("@NM_ProductName", filterProduct.ProductName);
                this.command.Parameters.AddWithValue("@DS_ProductDescription", filterProduct.ProductDescription);
                this.command.Parameters.AddWithValue("@QT_AvaiableQuantity", filterProduct.AvaiableQuantity);
                this.command.Parameters.AddWithValue("@CD_Supplier", filterProduct.SupplierId);
                this.query = this.command.ExecuteReader();
                IList<Product> produtos = new List<Product>();
                while (this.query.Read())
                {
                    produtos.Add(new Product()
                    {
                        ProductID = this.query.GetInt32(0),
                        ProductName = this.query.GetString(1),
                        ProductDescription = this.query.GetString(2),
                        AvaiableQuantity = this.query.GetInt32(3),
                        SupplierId = this.query.GetString(4)

                    });
                }

                return produtos;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }

        }

    }
}
