using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Data.OleDb;

namespace PAOnlineAssessment.Classes
{
    public class DataAccessObject
    {
        public SqlDataReader dr;
        public DataSet ds;
        SqlCommand myCommand;

        ////////////////////////
        //--------------------//
        //---SQL Procedures---//
        //--------------------//
        ////////////////////////

        //open the Sql Connection
        public void OpenConnection()
        {
            try
            {
                Defaults.PaceRegistrationConnection.Dispose();
                //Close SQL Connection
                Defaults.PaceRegistrationConnection.Close();                
                //Open a New SQL Connection
                Defaults.PaceRegistrationConnection.Open();
            }
            catch
            {                
                //Instantiate New Connection
                Defaults.PaceRegistrationConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["PaceRegistration"].ConnectionString.ToString());
                //Open a New SQL Connection
                Defaults.PaceRegistrationConnection.Open();
            }
            
        }
        //execute select queries that have values to return, return as DataReader
        public SqlDataReader ExecuteReader(string query)
        {
            OpenConnection();
            return new SqlCommand(query, Defaults.PaceRegistrationConnection).ExecuteReader();            
        }
        //execute select queries that have values to return, return as DataReader
        public DataSet ExecuteQuery(string query)
        {
            OpenConnection();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(new SqlCommand(query, Defaults.PaceRegistrationConnection));
            da.Fill(ds, "t1");
            CloseConnection();
            return ds;
        }
        //execute insert, update and other queries with no values to be returned, returns the number of rows affected
        public int ExecuteNonQuery(string query)
        {
            OpenConnection();
            int rowsAffected;            
            rowsAffected = new SqlCommand(query, Defaults.PaceRegistrationConnection).ExecuteNonQuery();
            CloseConnection();
            return rowsAffected;
        }
        //execute select queries that only have 1 value to return [COUNT(),SUM(),AVG(),etc.]
        public string ExecuteScalar(string query)
        {
            string toBeReturned = string.Empty;
            try
            {
                
                OpenConnection();
                myCommand = new SqlCommand(query, Defaults.PaceRegistrationConnection);
                toBeReturned = myCommand.ExecuteScalar().ToString();
            }
            catch 
            {
                toBeReturned = string.Empty;
            }
            CloseConnection();
            return toBeReturned;
        }
        //close the Sql Connection
        public void CloseConnection()
        {
            Defaults.PaceRegistrationConnection.Close();
        }        


        ////////////////////////
        //--------------------//
        //---XLS Procedures---//
        //--------------------//
        ////////////////////////

        //Open a new Connection
        public void OpenXLSConnection(string DataFile)
        {
            string xlsconnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="+DataFile+"; Extended Properties=Excel 8.0";
            
            try
            {
                Defaults.XLSConnection.Close();
            }
            catch
            {
            }
            Defaults.XLSConnection = new OleDbConnection(xlsconnectionString);          
        }

        //execute select queries that have values to return, return as DataReader
        public OleDbDataReader ExecuteXLSReader(string query,string DataFile)
        {
            OpenXLSConnection(DataFile);
            return new OleDbCommand(query, Defaults.XLSConnection).ExecuteReader();
        }

        public DataSet ExecuteXLSQuery(string query, string DataFile)
        {
            OpenXLSConnection(DataFile);

            OleDbCommand cmd = new OleDbCommand(query, Defaults.XLSConnection);
            OleDbDataAdapter da = new OleDbDataAdapter();

            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            da.Fill(ds, "t1");

            return ds;
        }
    

        
        /////////////////////////
        //---------------------//
        //---XLSX Procedures---//
        //---------------------//
        /////////////////////////

        //Open a new connection
        public void OpenXLSXConnection(string DataFile)
        {
            string xlsxconnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + DataFile + "; Extended Properties=Excel 12.0";

            try
            {
                Defaults.XLSXConnection.Close();
            }
            catch
            {
            }
            Defaults.XLSXConnection = new OleDbConnection(xlsxconnectionString);
        }

        //execute select queries with values to return, return as DataReader
        public OleDbDataReader ExecuteXLSXReader(string query, string DataFile)
        {
            OpenXLSXConnection(DataFile);
            return new OleDbCommand(query, Defaults.XLSXConnection).ExecuteReader();
        }

        public DataSet ExecuteXLSXQuery(string query, string DataFile)
        {
            OpenXLSXConnection(DataFile);

            OleDbCommand cmd = new OleDbCommand(query, Defaults.XLSXConnection);
            OleDbDataAdapter da = new OleDbDataAdapter();

            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            da.Fill(ds, "t1");
            return ds;
        }

    }
}
