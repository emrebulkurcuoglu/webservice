using log4net;
using studenrecordsystem;
using System;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebServiceRecordSystem
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class SearhWebService : System.Web.Services.WebService
    {
        ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod]
        public int FindRecord(string StudentIdToFindStudent)
        {
            Log.Info("Find record is called");
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
                
                Log.Error(exception.Message);
                return 0;
            }
        }

        [WebMethod]
        public Student GetStudent(string StudentIdToPrint)

        {
            Log.Info("GetStudent is called");
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

                Log.Error(exception.Message);
                return null;
            }
        }

        [WebMethod]
        public int HowManyAdress(string id)
        {
            Log.Info("HowManyAdress is called");
            try
            {
                string connetionString;
                SqlConnection cnn;
                SqlCommand cmd2;

                connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                string query2 = "SELECT COUNT(StudentId) FROM [dbo].[Adresses] WHERE StudentId like '" + id + "'";
                cmd2 = new SqlCommand(query2, cnn);
                SqlDataReader read2 = cmd2.ExecuteReader();
                read2.Read();
                string recordCount2 = read2[0].ToString();

                cnn.Close();
                return Convert.ToInt32(recordCount2);
            }
            

            catch (Exception exception)
            {

                Log.Error(exception.Message);
                return -1;
            }
            
        }

        [WebMethod]
        public Adress GetAdress(string StudentIdToPrint, int adressNo)
        {
            Log.Info("GetAdress is called");
            try
            {
                Adress adressToReturn = new Adress();
                string connectionString;
                SqlConnection sqlConnection;
                SqlCommand sqlCommand;

                connectionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string querySelectStudent = "SELECT * FROM [dbo].[Adresses] WHERE StudentId like '" + StudentIdToPrint + "' and AdressNo = "+ adressNo;
                sqlCommand = new SqlCommand(querySelectStudent, sqlConnection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                read.Read();

                adressToReturn.Street= read[2].ToString();
                adressToReturn.Neighborhood = read[3].ToString();
                adressToReturn.District = read[4].ToString();
                adressToReturn.State = read[5].ToString();

                sqlConnection.Close();
                return adressToReturn;
            }
            catch (Exception exception)
            {

                Log.Error(exception.Message);
                return null;
            }
        }
    }
}
