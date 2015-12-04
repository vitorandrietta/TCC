using Eletronicos.Model.Purchase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronicos.Data
{
    public class PurchaseDAO : IData<Purchase>
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

        /// <summary>
        /// insert a new row in the Client table of the database
        /// </summary>
        /// <param name="objectToBeInserted">represents the model that gonna be inserted in the database table</param>

        public void Insert(Purchase objectToBeInserted)
        {
            try
            {
                //trocar parametro da proc CD_CLIENT ID = varchar(13)
                this.connection.Open();
                this.command = new SqlCommand("exec PR_PURCHASE_INSERT @QT_ProductQuantity,@CD_ClientID,@CD_ProductId,@CD_PurchaseStatus");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@QT_ProductQuantity", objectToBeInserted.ProductQuantity);
                this.command.Parameters.AddWithValue("@CD_ClientID", objectToBeInserted.ClientID);
                this.command.Parameters.AddWithValue("@CD_ProductId", objectToBeInserted.ProductID);
                this.command.Parameters.AddWithValue("@CD_PurchaseStatus", objectToBeInserted.PurchaseStatus);
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

        public void Delete(Purchase objectToBeDeleted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_PURCHASE_DELETE @CD_PurchaseId");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CD_PurchaseId", objectToBeDeleted.PurchaseID);
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

        public void Update(Purchase objectToBeUpdated)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec proc PR_PURCHASE_UPDATE @CD_PurchaseID,@QT_ProductQuantity,@CD_ClientID,@CD_ProductId,@CD_PurchaseStatus");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@QT_ProductQuantity", objectToBeUpdated.ProductQuantity);
                this.command.Parameters.AddWithValue("@CD_ClientID", objectToBeUpdated.ClientID);
                this.command.Parameters.AddWithValue("@CD_ProductId", objectToBeUpdated.ProductID);
                this.command.Parameters.AddWithValue("@CD_PurchaseStatus", objectToBeUpdated.PurchaseStatus);
                this.command.Parameters.AddWithValue("@CD_PurchaseID", objectToBeUpdated.PurchaseID);
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

        public IList<Purchase> FindAll()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_Purchase_Select()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<Purchase> purchases = new List<Purchase>();

                while (this.query.Read())
                {
                    purchases.Add(new Purchase()
                    {
                        PurchaseID = this.query.GetInt32(0),
                        ProductQuantity = this.query.GetInt32(1),
                        ClientID = this.query.GetString(2),
                        ProductID = this.query.GetInt32(3),
                        PurchaseStatus = this.query.GetInt32(4)
                    });
                }

                return purchases;
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

        public Purchase Find(Purchase objectToBeFound)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO. FN_Purchase_SelectSingle (@CD_PurchaseID)");
                this.command.Parameters.AddWithValue("@CD_PurchaseID", objectToBeFound.PurchaseID);
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                Purchase purchase = null;

                if (this.query.Read())
                {
                    purchase = new Purchase()
                   {
                       PurchaseID = this.query.GetInt32(0),
                       ProductQuantity = this.query.GetInt32(1),
                       ClientID = this.query.GetString(2),
                       ProductID = this.query.GetInt32(3),
                       PurchaseStatus = this.query.GetInt32(4)
                   };
                }

                return purchase;
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
