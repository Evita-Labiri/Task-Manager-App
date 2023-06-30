using System.Data.SqlClient;

namespace TaskManagerFinalCFProjectApp.DAO.DBUtil
{
    public class DBHelper
    {
        private static SqlConnection? conn;

        private DBHelper() { }

        public static SqlConnection? GetConnection()
        {
            conn = null;
            try
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.AddJsonFile("appsettings.json");

                string url = configurationManager.GetConnectionString("DefaultConnection");
                conn = new SqlConnection(url);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.StackTrace);
            }

            return conn;
        }

        public static void CloseConnection() 
        {
            conn?.Close();
        }
    }
}
