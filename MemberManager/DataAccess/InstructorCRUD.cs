using MemberManagement.entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class InstructorCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<Instructor> GetInstructors()
        {
            var Instructors = new List<Instructor>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from Instructor", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new Instructor();
                    list.Id = Convert.ToInt32(reader["ID"].ToString());
                    list.Name = reader["NAME"].ToString();
                    list.LastName = reader["LASTNAME"].ToString();
                    list.Branch = reader["BRANCH"].ToString();

                    Instructors.Add(list);
                }
                con.Close();
            }
            return Instructors;
        }

        public Instructor GetInstructorsById(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                Instructor response = new Instructor();

                string query = $"select * from Instructor where Id = {Id}";
                OracleCommand command = new OracleCommand(query, con);
                command.Connection = con;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Instructor read = new Instructor();
                    read.Id = Convert.ToInt32(reader["ID"].ToString());
                    read.Name = reader["NAME"].ToString();
                    read.LastName = reader["LASTNAME"].ToString();
                    read.Branch = reader["BRANCH"].ToString();

                    response = read;
                }
                con.Close();
                return response;
            }
        }

        public void CreateInstructors(Instructor report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertInstructor";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                command.Parameters.Add("p_branch", report.Branch);

                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateInstructors(Instructor report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "UpdateInstructor";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", report.Id);
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                command.Parameters.Add("p_branch", report.Branch);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void Delete(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "DeleteInstructors";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", Id);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

    }

}
