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
    public class ClientLoginDAO
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

        //public ClientLoginDAO()
        //{
        //    try
        //    {
        //        this.connection.Open();
        //        this.command = new SqlCommand("INSERT INTO USERLOGIN VALUES('vitor','andrietta','76355545654','04/01/2015')");
        //        this.command.Connection = this.connection;
        //        this.command.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        this.connection.Close();
        //        this.command = null;
        //    }
        //}

        public ClientLogin Find(ClientLogin client)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT CD_ClientID from UserLogin Where NM_username=@username and CD_password=@password");
                this.command.Parameters.AddWithValue("@username", client.Username);
                this.command.Parameters.AddWithValue("@password", client.Password);
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                if (this.query.Read())
                {
                    return new ClientLogin()
                    {
                        ClientID = query.GetString(0)
                    };
                }
                else
                {
                    return null;
                }
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

        public bool Login(ClientLogin client)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT DBO.FN_ClientLogin(@username,@password)");
                this.command.Parameters.AddWithValue("@username", client.Username);
                this.command.Parameters.AddWithValue("@password", client.Password);
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                this.query.Read();
                return this.query.GetBoolean(0);

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
