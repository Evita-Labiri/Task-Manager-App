using System.Data.SqlClient;
using TaskManagerFinalCFProjectApp.DAO.DBUtil;
using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.DAO.TaskDAO
{
    public class TaskDAOImpl : ITaskDAO
    {
        public void Insert(Tasks? task, string? categoryName)
        {
            if (task == null) return;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sqlCategory = "SELECT ID FROM CATEGORY WHERE CATEGORY_NAME = @categoryName";
                using SqlCommand categoryCommand = new SqlCommand(sqlCategory, conn);
                categoryCommand.Parameters.AddWithValue("@categoryName", categoryName);

                object result = categoryCommand.ExecuteScalar();
                int categoryId = result != null ? (int)result : 0;


                string sqlTask = "INSERT INTO TASK"
                    + "(TASK_NAME, DESCRIPTION,DUE_DATE, CATEGORY_ID, STATUS) VALUES"
                    + "(@taskName, @description, @dueDate, @categoryId, @status)";
                using SqlCommand taskCommand = new SqlCommand(sqlTask, conn);
                taskCommand.Parameters.AddWithValue("@taskName", task.TaskName);
                taskCommand.Parameters.AddWithValue("@description", task.Description);
                taskCommand.Parameters.AddWithValue("@dueDate", task.DueDate);
                taskCommand.Parameters.AddWithValue("@categoryId", categoryId);
                taskCommand.Parameters.AddWithValue("@status", task.Status);

                taskCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while inserting the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Update(Tasks? task, string? categoryName)
        {
            if (task == null) return;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sqlCategory = "SELECT ID FROM CATEGORY WHERE CATEGORY_NAME = @categoryName";
                using SqlCommand categoryCommand = new SqlCommand(sqlCategory, conn);
                categoryCommand.Parameters.AddWithValue("@categoryName", categoryName);

                object result = categoryCommand.ExecuteScalar();
                int categoryId = result != null ? (int)result : 0;


                string sqlTask = "UPDATE TASK "
                     + "SET TASK_NAME = @taskName, DESCRIPTION = @description, DUE_DATE = @dueDate, CATEGORY_ID = @categoryId, STATUS = @status "
                     + "WHERE ID = @id";

                using SqlCommand taskCommand = new SqlCommand(sqlTask, conn);
                taskCommand.Parameters.AddWithValue("@taskName", task.TaskName);
                taskCommand.Parameters.AddWithValue("@description", task.Description);
                taskCommand.Parameters.AddWithValue("@dueDate", task.DueDate);
                taskCommand.Parameters.AddWithValue("@categoryId", categoryId);
                taskCommand.Parameters.AddWithValue("@status", task.Status);
                taskCommand.Parameters.AddWithValue("@id", task.Id);


                taskCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while updating the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sql = "DELETE FROM TASK WHERE ID=@id";

                using SqlCommand command = new(sql, conn);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while deleting the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public Tasks? GetById(int id)
        {
            Tasks? task = null;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();
                string sql = "SELECT * FROM TASK WHERE ID = @id";
                using SqlCommand command = new(sql, conn);
                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    task = new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        TaskName = reader.GetString(reader.GetOrdinal("TASK_NAME")),
                        Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                        DueDate = reader.GetDateTime(reader.GetOrdinal("DUE_DATE")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CATEGORY_ID")),
                        Status = (TasksStatus)reader.GetInt32(reader.GetOrdinal("STATUS"))

                    };
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while retrieving the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }

            return task;
        }

        public List<Tasks> GetByCategoryName(string categoryName)
        {
            List<Tasks>? tasks = new List<Tasks>();

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sql = "SELECT * FROM TASK WHERE CATEGORY_ID = (SELECT ID FROM CATEGORY WHERE CATEGORY_NAME = @categoryName)";

                using SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@categoryName", categoryName);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tasks? task = new Tasks
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        TaskName = reader.GetString(reader.GetOrdinal("TASK_NAME")),
                        Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                        DueDate = reader.GetDateTime(reader.GetOrdinal("DUE_DATE")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CATEGORY_ID")),
                        Status = (TasksStatus)reader.GetInt32(reader.GetOrdinal("STATUS"))
                    };

                    tasks.Add(task);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while retrieving the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }

            return tasks;
        }

        public List<Tasks> GetAll()
        {
            List<Tasks>? tasks = new List<Tasks>();

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();
                string sql = "SELECT * FROM TASK"; 
                using SqlCommand command = new(sql, conn);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tasks task = new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        TaskName = reader.GetString(reader.GetOrdinal("TASK_NAME")),
                        Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                        DueDate = reader.GetDateTime(reader.GetOrdinal("DUE_DATE")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CATEGORY_ID")),
                        Status = (TasksStatus)reader.GetInt32(reader.GetOrdinal("STATUS")),
         
                    };
                    tasks.Add(task);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while retrieving the tasks. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }

            return tasks;
        }
    }
}
