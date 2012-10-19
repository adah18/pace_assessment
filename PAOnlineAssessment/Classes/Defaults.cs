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
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment.Classes
{
    public class Defaults
    {        
        public static SqlConnection PaceRegistrationConnection;// = new SqlConnection(ConfigurationManager.ConnectionStrings["PaceRegistration"].ConnectionString.ToString());        
        public static OleDbConnection XLSConnection;
        public static OleDbConnection XLSXConnection;

        public static string TempFolderPath = "~\\assessment\\uploaded_images\\temp_file\\";
        public static string TempImagePath = "~/assessment/uploaded_images/temp_file/";

        public static string FolderPath = "~\\assessment\\uploaded_images\\";
        public static string ImagePath = "~/assessment/uploaded_images/";

    }   

}
