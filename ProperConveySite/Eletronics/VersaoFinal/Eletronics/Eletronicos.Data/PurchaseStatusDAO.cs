using Eletronicos.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronicos.Data
{
    class PurchaseStatusDAO : IData<PurchaseStatus>
    {   /// <summary>
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

        public void Insert(PurchaseStatus objectToBeInserted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_INSERT_PurchaseStatus @CD_StatusId,@DS_PurchaseStatusDescription");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CD_StatusId", objectToBeInserted.StatusId);
                this.command.Parameters.AddWithValue("@DS_PurchaseStatusDescription", objectToBeInserted.StatusDescription);
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

        public void Delete(PurchaseStatus objectToBeDeleted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_Delete_PurchaseStatus @CD_StatusID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CD_StatusID", objectToBeDeleted.StatusId);
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

        public void Update(PurchaseStatus objectToBeUpdated)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_Update_PurchaseStatus @CD_StatusID,@DS_PurchaseStatusDescription");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CD_StatusId", objectToBeUpdated.StatusId);
                this.command.Parameters.AddWithValue("@DS_PurchaseStatusDescription", objectToBeUpdated.StatusDescription);
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

        public IList<PurchaseStatus> FindAll()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_PurchaseStatus_Select()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<PurchaseStatus> purchaseStatus = new List<PurchaseStatus>();
                while (this.query.Read())
                {
                    purchaseStatus.Add(new PurchaseStatus()
                    {
                        StatusId = this.query.GetInt32(0),
                        StatusDescription = this.query.GetString(1)                       
                    });
                }

                return purchaseStatus;
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

        public PurchaseStatus Find(PurchaseStatus objectToBeFound)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_PurchaseStatus_SelectSingle(@CD_StatusId)");
                this.command.Parameters.AddWithValue("@CD_StatusId", objectToBeFound.StatusId);
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                PurchaseStatus purchaseStatus = null;
                if (this.query.Read())
                {
                    purchaseStatus = new PurchaseStatus()
                    {
                        StatusId = this.query.GetInt32(0),
                        StatusDescription = this.query.GetString(1)
                    };
                }

                return purchaseStatus;
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
