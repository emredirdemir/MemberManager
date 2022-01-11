using MemberManagement.entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class UserCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<User> GetUsers()
        {
            var Users = new List<User>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from Users", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new User();
                    list.Id = Convert.ToInt32(reader["ID"].ToString());
                    list.UserName = reader["USERNAME"].ToString();
                    list.Password = reader["PASSWORD"].ToString();
                    list.Role = reader["ROLE"].ToString();
                    Users.Add(list);
                }
                con.Close();
            }
            return Users;
        }

        public void CreateUsers(User user)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertUsers";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_username", user.UserName);
                command.Parameters.Add("p_password", user.Password);
                command.Parameters.Add("p_role", user.Role);

                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }


    }
}
