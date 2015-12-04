namespace Eletronicos.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Eletronicos.Model;
    using System.Data;
    using System.Net;
    using System.IO;
    using System.Web.Script.Serialization;
    using System.Runtime.Serialization.Json;
    /// <summary>
    /// A class that performs CRUD operations of Supplier Table
    /// </summary>
    public class SupplierDAO
    {
        private const string supplierServiceReference = "http://localhost:50821/SupplierService.svc/SupplierServices/";

        /// <summary>
        ///  A variable that represents the connection with the database
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

        public void AddSupplier(Supplier supplier)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(supplierServiceReference + "AddSupplier");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(supplier);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            var result = streamReader.ReadToEnd();

        }

        public void UpdateSupplier(Supplier supplier)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(supplierServiceReference + "UpdateSupplier");

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(supplier);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            var result = streamReader.ReadToEnd();
        }

        public void RemoveSupplier(Supplier supplier)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(supplierServiceReference + "RemoveSupplier");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "DELETE";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(supplier);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            var result = streamReader.ReadToEnd();

        }

        public IList<SupplierType> FindAllSupplierTypes()
        {

            string url = supplierServiceReference + "FindAllSupplierTypes";
            WebRequest request = WebRequest.Create(url);
            WebResponse ws = request.GetResponse();
            DataContractJsonSerializer jsonSerializer =
            new DataContractJsonSerializer(typeof(IList<SupplierType>));
            return (IList<SupplierType>)jsonSerializer.ReadObject(ws.GetResponseStream());

        }

        public IList<Supplier> FindAllSuppliers()
        {
            string url = supplierServiceReference + "FindAllSuppliers";
            WebRequest request = WebRequest.Create(url);
            WebResponse ws = request.GetResponse();
            DataContractJsonSerializer jsonSerializer =
            new DataContractJsonSerializer(typeof(IList<Supplier>));
            return (IList<Supplier>)jsonSerializer.ReadObject(ws.GetResponseStream());

        }

        public Supplier Find(Supplier objectToBeFound)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_SupplierSingleSelect(@CNPJ)");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CNPJ", objectToBeFound.CNPJ);
                this.query = this.command.ExecuteReader();
                Supplier supplier = null;
                if (this.query.Read())
                {
                    supplier = new Supplier()
                    {
                        CorporateName = this.query.GetString(0),
                        CNPJ = this.query.GetString(1),
                        SupplierTypeID = this.query.GetInt32(2)
                    };
                }

                return supplier;
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
