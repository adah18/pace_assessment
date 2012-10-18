using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;

namespace PAOnlineAssessment.Classes
{
    //Validators
    public class Validator
    {
        public static void AlertBack(string message, string url)
        {
            HttpContext.Current.Response.Write("<script>alert('" + message + "');window.location='" + url + "';</script>");
        }

        public static void Alert(string message)
        {
            HttpContext.Current.Response.Write("<script>alert('" + message + "');</script>");
        }
        //Check if String is Empty, Return True if Empty
        public static bool isEmpty(string Expression)
        {
            bool Result = true;

            if (Expression.Trim().Length > 0)
            {
                Result = false;
            }

            return Result;
        }

        public static bool CanbeAccess(string code, string access_string)
        {
            bool value;
            try
            {
                code = "-" + code + "-";


                Debug.WriteLine(code + ", " + access_string);
                if (access_string.Contains(code))
                {
                    value = true;
                    Debug.WriteLine("true");
                }
                else
                {
                    value = false;
                    Debug.WriteLine("false");
                }
            }
            catch
            {
                value = false;
            }

            return value;
        }
        //
        //Check if 2 Input Strings are Equal, Return True if Not Equals
        public static bool isNotEqual(string Expression1, string Expression2)
        {
            bool Result = true;

            if (Expression1 == Expression2)
            {
                Result = false;
            }
            return Result;
        }

        //Check if Selected Item in the DropDownList is the First Item, Return True if 1st Item is selected
        public static bool isDefaultSelected(string Expression)
        {
            bool Result = true;

            if (Expression != "0")
            {
                Result = false;
            }

            return Result;
        }

        //Check if Money Specified is valid, Return True if Not Valid
        public static bool isMoneyNotValid(string Expression)
        {
            bool Result = false;
            try
            {
                float flt = float.Parse(Expression);

                Debug.WriteLine("Converted Cash Bond: " + flt.ToString());
            }
            catch
            {
                Result = true;
            }

            return Result;

        }

        //Check if Email Address is Valid, returns True if Email is Invalid
        public static bool isEmailNotValid(string Expression)
        {
            if (Expression.Contains("@") && Expression.Contains("."))
            {
                return false;
            }
            return true;
        }

        //Finalize the Input String, Special Characters will be removed and Replaced to match SQL Statements Standards
        public static string Finalize(string Expression)
        {
            Expression = Expression.Replace("'", "''");

            if (CountCharacterInString("'", Expression) % 2 != 0)
            {
                Expression = Expression + "'";
            }


            if (Expression.Contains("--"))
            {
                Expression = Expression.Replace("--", "");
            }
            if (Expression.Contains('"'.ToString()))
            {
                Expression = Expression.Replace('"'.ToString(), "");
            }
            Expression = Expression.Trim();
            return Expression;
        }

        public static bool isNotValid(string value)
        {
            bool x = false;
            if (value.Contains("'"))
            {
                x = true;
            }
            else if (value.Contains("--"))
            {
                x = true;
            }
            return x;
        }

        //Count the times the Number of Character was repeated in the string
        private static int CountCharacterInString(string CharacterToFind, string Expression)
        {
            int ExpressionCount = Expression.Length;
            string Temporary = Expression.Replace(CharacterToFind, "");
            int TemporaryCount = Temporary.Length;
            if (Expression.Length - Temporary.Length == 0)
            {
                return 0;
            }
            else
            {
                return Expression.Length - Temporary.Length;
            }
        }

        //Check if an Internet Connection is Available
        public static bool isNotOnline()
        {
            bool Status = false;

            System.Uri Url = new System.Uri("http://www.microsoft.com");

            System.Net.WebRequest WebReq;
            System.Net.WebResponse Resp;
            WebReq = System.Net.WebRequest.Create(Url);
            WebReq.Timeout = 2000;
            Debug.WriteLine("Timeout: " + WebReq.Timeout);

            try
            {
                Resp = WebReq.GetResponse();
                Resp.Close();
                WebReq = null;
                Status = false;
            }

            catch
            {
                WebReq = null;
                Status = true;
            }



            return Status;
        }
    }
    public class conv
    {
        public static int ToInt32(string field)
        {
            return Convert.ToInt32(field);
        }
    }
    //Misc System Procedures
    public class SystemProcedures : DataAccessObject
    {
        //Deactivates Records
        public int DeactivateRecord(string TableName, string ReferenceField, string ReferenceID, string Username)
        {
            OpenConnection();
            int RowsAffected = ExecuteNonQuery("UPDATE " + TableName + " SET Status='D',LastUpdateUser='" + Username + "',LastUpdateDate=getdate() WHERE " + ReferenceField + " = " + ReferenceID);
            return RowsAffected;

        }

        //Activate Records
        public int ActivateRecord(string TableName, string ReferenceField, string ReferenceID, string Username)
        {
            OpenConnection();
            int RowsAffected = ExecuteNonQuery("UPDATE " + TableName + " SET Status='A',LastUpdateUser='" + Username + "',LastUpdateDate=getdate() WHERE " + ReferenceField + " = " + ReferenceID);
            return RowsAffected;

        }
        //Activate Parent Records
        public int ActivateParent(string TableName, string ReferenceField, string ReferenceID, string Username)
        {
            OpenConnection();
            int RowsAffected = ExecuteNonQuery("UPDATE " + TableName + " SET Status='A',UpdateUser='" + Username + "',UpdateDate=getdate() WHERE " + ReferenceField + " = " + ReferenceID);
            return RowsAffected;

        }

        //Send Email For Login Details Requests
        public void SendLoginRequestEmail(object UserInfo)
        {
            try
            {
                string EmailMsg;

                RetrieveUser RInfo = (RetrieveUser)UserInfo;

                string uri = HttpContext.Current.Request.Url.Authority;
                EmailMsg = "Dear " + RInfo.Firstname + " " + RInfo.Lastname + ",<br><br>";
                EmailMsg = EmailMsg + "The system received recently a request to retrieve your User Login Information.<br><br>";
                EmailMsg = EmailMsg + "Your Username is '" + RInfo.Username + "'<br>";
                EmailMsg = EmailMsg + "Your Password is '" + RInfo.Password + "'<br><br>";
                EmailMsg = EmailMsg + "To Login your account, Please Click the Link Below<br><a href='http://" + uri + "/login.aspx'>Pace Academy Online Assessment System Login</a><br><br>";
                EmailMsg = EmailMsg + "If you did NOT request to retrieve your User Login Information, you don't need to take any action.<br><br>";
                EmailMsg = EmailMsg + "-----------------------<br>";
                EmailMsg = EmailMsg + "This is a System Generated Email.";


                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("NYKFIL.Notifier@gmail.com", "789456123q"),
                    EnableSsl = true
                };
                MailAddress emailSender = new MailAddress("NYKFIL.Notifier@gmail.com", "Pace Assessment");
                mail.From = emailSender;
                MailAddress receipient = new MailAddress(RInfo.EmailAddress);
                mail.To.Add(receipient);
                mail.IsBodyHtml = true;
                mail.Body = EmailMsg;
                mail.Subject = "User Login Information - Pace Academy Online Assessment System";

                smtp.Send(mail);
            }
            catch
            {

            }
        }


        //Send Activation Email for Account Activations
        public void SendActivationEmail(object UserInfo, string ActivationURL, string SessionString)
        {
            try
            {
                string EmailMsg;

                //RetrieveUser RInfo = (RetrieveUser)UserInfo;
                Constructors.StudentAccount RInfo = (Constructors.StudentAccount)UserInfo;


                EmailMsg = "Dear " + RInfo.Firstname + " " + RInfo.Lastname + ",<br><br>";
                EmailMsg = EmailMsg + "Welcome to the Pace Academy Online Assessment System<br><br>";
                EmailMsg = EmailMsg + "Please verify your email by clicking here:<br>";
                EmailMsg = EmailMsg + "<a href='" + ActivationURL + RInfo.StudentID + "&session=" + SessionString + "'>" + ActivationURL + RInfo.StudentID + "&session=" + SessionString + "</a><BR><BR>";
                EmailMsg = EmailMsg + "-----------------------<br>";
                EmailMsg = EmailMsg + "This is a System Generated Email.";

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("NYKFIL.Notifier@gmail.com", "789456123q"),
                    EnableSsl = true
                };
                MailAddress emailSender = new MailAddress("NYKFIL.Notifier@gmail.com", "Pace Assessment");
                mail.From = emailSender;
                MailAddress receipient = new MailAddress(RInfo.EmailAddress);
                mail.To.Add(receipient);
                mail.IsBodyHtml = true;
                mail.Body = EmailMsg;
                mail.Subject = "Email Activation - Pace Academy Online Assessment System";

                smtp.Send(mail);
            }
            catch
            {

            }
        }
    }
}
