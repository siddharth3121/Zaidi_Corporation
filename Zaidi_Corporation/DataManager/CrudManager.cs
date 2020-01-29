using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Zaidi_Corporation.Models;

namespace Zaidi_Corporation.DataManager
{
    public class CrudManager
    {
        #region Member variable
        private string m_sConnectionString;
        #endregion

        #region Constructor
        public CrudManager()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        #endregion

        #region  Get or Set ConnectionString

        public string ConnectionString
        {
            get => m_sConnectionString;
            set => m_sConnectionString = value;
        }

        #endregion

        #region Public Method

        
        public List<CrudModel> GetDetails()
        {
            var lstData = new List<CrudModel>();

            string sQuery = "SELECT * FROM CRUD_DATA";

            using (SqlConnection objSqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(sQuery, objSqlConnection))
                {
                    objSqlConnection.Open();
                    var objReader = objSqlCommand.ExecuteReader();

                    try
                    {
                        while (objReader.Read())
                        {
                            var objData = new CrudModel();
                            objData.Batch_Name = objReader["Batch_Name"].ToString();                         
                            objData.Batch_Location = objReader["Batch_Location"].ToString();
                            objData.Start_Date = objReader["Start_Date"].ToString();
                            objData.End_Date = objReader["End_Date"].ToString();
                            lstData.Add(objData);
                        }
                    }
                    finally
                    {
                        objReader.Close();
                    }
                }
            }

            return lstData;
        }

        public bool InsertDetails(CrudModel objData)
        {
            string sQuery = "Insert Into CRUD_DATA Values(@Batch_Name,@Batch_Location,@Start_Date,@End_Date)";

            using (var objConnection = new SqlConnection(ConnectionString))
            {
                using (var objSqlCommand = new SqlCommand(sQuery, objConnection))
                {
                    objSqlCommand.Parameters.AddWithValue("@Batch_Name", objData.Batch_Name);
                    objSqlCommand.Parameters.AddWithValue("@Batch_Location", objData.Batch_Location);
                    objSqlCommand.Parameters.AddWithValue("@Start_Date", objData.Start_Date);
                    objSqlCommand.Parameters.AddWithValue("@End_Date", objData.End_Date);

                    objConnection.Open();

                    int n = objSqlCommand.ExecuteNonQuery();

                    if (n >= 1)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public bool EditDetails(CrudModel objData)
        {
            string sQuery = "UPDATE CRUD_DATA SET Batch_Name= @Batch_Name,Batch_Location= @Batch_Location,Start_Date= @Start_Date,End_Date= @End_Date " +
                "WHERE Batch_Name= @Batch_Name";

            using (var objConnection = new SqlConnection(ConnectionString))
            {
                using (var objSqlCommand = new SqlCommand(sQuery, objConnection))
                {
                    objSqlCommand.Parameters.AddWithValue("@Batch_Name", objData.Batch_Name);
                    objSqlCommand.Parameters.AddWithValue("@Batch_Location", objData.Batch_Location);
                    objSqlCommand.Parameters.AddWithValue("@Start_Date", objData.Start_Date);
                    objSqlCommand.Parameters.AddWithValue("@End_Date", objData.End_Date);

                    objConnection.Open();

                    int n = objSqlCommand.ExecuteNonQuery();

                    if (n >= 1)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public bool DeleteDetails(string Name)
        {
            string sQuery = "Delete From CRUD_DATA WHERE Batch_Name = @Batch_Name";

            using (var objConnection = new SqlConnection(ConnectionString))
            {
                using (var objSqlCommand = new SqlCommand(sQuery, objConnection))
                {
                    objSqlCommand.Parameters.AddWithValue("@Batch_Name",Name);                 

                    objConnection.Open();

                    int n = objSqlCommand.ExecuteNonQuery();

                    if (n >= 1)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public CrudModel GetSpecificUser(string sBatchName)
        {
            CrudModel objData = null;
            string sQuery = "Select * from CRUD_DATA WHERE Batch_Name = @Name";

            using (var objSqlConnection = new SqlConnection(ConnectionString))
            {
                using (var objSqlCommand = new SqlCommand(sQuery, objSqlConnection))
                {
                    objSqlCommand.Parameters.AddWithValue("@Name", sBatchName);
                    objSqlConnection.Open();
                    var objReader = objSqlCommand.ExecuteReader();
                    try
                    {
                        while(objReader.Read())
                        {
                            objData = new CrudModel();
                            if (string.IsNullOrWhiteSpace(objReader["Batch_Name"].ToString())== false)
                            {
                                objData.Batch_Name = objReader["Batch_Name"].ToString();
                            }
                          if(string.IsNullOrWhiteSpace(objReader["Batch_Location"].ToString())==false)
                            {
                                objData.Batch_Location = objReader["Batch_Location"].ToString();
                            }
                          if(string.IsNullOrWhiteSpace(objReader["Start_Date"].ToString()) == false)
                            {
                                objData.Start_Date = objReader["Start_Date"].ToString();
                            }
                          if(string.IsNullOrWhiteSpace(objReader["End_Date"].ToString())== false)
                            {
                                objData.End_Date = objReader["End_Date"].ToString();
                            }
                        }
                    }
                    finally
                    {
                        objReader.Close();                     
                    }
                }
            }
            return objData;
        }

        #endregion
    }
}