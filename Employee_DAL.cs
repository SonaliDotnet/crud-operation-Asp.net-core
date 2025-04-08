using CRUDwithADONET.Models;
using Microsoft.Data.SqlClient;

namespace CRUDwithADONET.DAL
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");
            configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        public List<EmployeeModel> GetAll()
        {
            List<EmployeeModel> employeeModelList = new List<EmployeeModel>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_Employees]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    EmployeeModel EmployeeModel = new EmployeeModel();
                    EmployeeModel.Id = Convert.ToInt32(dr["Id"]);
                    EmployeeModel.FirstName = dr["FirstName"].ToString();
                    EmployeeModel.LastName = dr["LastName"].ToString();
                    EmployeeModel.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                    EmployeeModel.Email = dr["Email"].ToString();
                    EmployeeModel.Salary = Convert.ToDouble(dr["Salary"]);
                    employeeModelList.Add(EmployeeModel);
                }
                _connection.Close();
            }
            return employeeModelList;
        }

        public bool Insert(EmployeeModel model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Insert_Employee]";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();

            }
            return id > 0 ? true : false;
        }

        public EmployeeModel GetById(int id)
        {
            EmployeeModel employee = new EmployeeModel();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_EmployeesById]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.FirstName = dr["FirstName"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);
                }
                _connection.Close();
            }
            return employee;
        }
       
        public bool Update(EmployeeModel model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Update_Employee]";
                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();

            }
            return id > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            int deleteRowCount = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Delete_Employee]";
                _command.Parameters.AddWithValue("@Id",id); 
                _connection.Open();
                deleteRowCount = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return deleteRowCount > 0 ? true : false;
        }
    }
}
