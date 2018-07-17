using studenrecordsystem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceRecordSystem
{
    /// <summary>
    /// Summary description for SearhWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SearhWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public int FindRecord(string StudentIdToFindStudent)
        {
            try
            {
                string connectionString;
                SqlConnection sqlConnection;
                SqlCommand sqlCommand;

                connectionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string queryCount = "SELECT COUNT(StudentId) FROM [dbo].[Students] WHERE StudentId like '" + StudentIdToFindStudent + "'";
                sqlCommand = new SqlCommand(queryCount, sqlConnection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                read.Read();
                string recordCount = read[0].ToString();

                if (recordCount == "1")
                {
                    return -1;
                }
                sqlConnection.Close();
                return 0;
            }

            catch (Exception exception)
            {
                //Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
                return 0;
            }
        }

        [WebMethod]
        public Student GetStudent(string StudentIdToPrint)
        {
            try
            {
                Student studentToReturn = new Student();
                string connectionString;
                SqlConnection sqlConnection;
                SqlCommand sqlCommand;

                connectionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string querySelectStudent = "SELECT * FROM [dbo].[Students] WHERE StudentId like '" + StudentIdToPrint + "'";
                sqlCommand = new SqlCommand(querySelectStudent, sqlConnection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                read.Read();
                
                studentToReturn.Name = read[0].ToString();
                studentToReturn.Surname = read[1].ToString();
                studentToReturn.Birthday = Utils.GetDateTimeOnConsoleWithValidationAndFormat(read[2].ToString(), "", "");
                studentToReturn.StudentId = read[3].ToString();
                studentToReturn.Gsm = read[4].ToString();
                
                sqlConnection.Close();
                return studentToReturn;
            }
            catch (Exception exception)
            {
                //Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
                return null;
            }
        }
     }
}
