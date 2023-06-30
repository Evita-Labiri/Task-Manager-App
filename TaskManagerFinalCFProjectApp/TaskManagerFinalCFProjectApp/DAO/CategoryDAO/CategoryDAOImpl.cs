using System.Data.SqlClient;
using System.Threading.Tasks;
using TaskManagerFinalCFProjectApp.DAO.DBUtil;
using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.DAO.CategoryDAO
{
    public class CategoryDAOImpl : ICategoryDAO
    {
        public void Insert(Category? category)
        {
            if (category == null) return;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();
                string sql = "INSERT INTO CATEGORY" + "(CATEGORY_NAME) VALUES" + "(@CategoryName)";

                using SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while inserting the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Update(Category? category)
        {
            if (category == null) return;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sql = "UPDATE CATEGORY SET CATEGORY_NAME=@categoryName WHERE ID=@id";

                using SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@categoryName", category.CategoryName);
                command.Parameters.AddWithValue("@id", category.Id);

                command.ExecuteNonQuery();
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

                string sql = "DELETE FROM CATEGORY WHERE ID=@id";

                using SqlCommand command = new SqlCommand(sql, conn);
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

        public Category? GetById(int id)
        {
            Category? category = null;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sql = "SELECT * FROM CATEGORY WHERE ID=@id";

                using SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category = new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME"))
                    };
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while retrieving the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }
            return category!;
        }

        public Category GetByName(string categoryName)
        {
            Category? category = null;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sql = "SELECT * FROM CATEGORY WHERE CATEGORY_NAME=@categoryName";

                using SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@categoryName", categoryName);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category = new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME"))
                    };
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while retrieving the task. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }
            return category!;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();
                conn!.Open();

                string sql = "SELECT * FROM CATEGORY";

                using SqlCommand command = new SqlCommand(sql, conn);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME"))
                    };
                    categories.Add(category);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred while retrieving the tasks. Please try again later.");
                Console.WriteLine(e.StackTrace);
                throw;
            }

            return categories;
        }
    }
}
