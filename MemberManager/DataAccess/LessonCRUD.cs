using MemberManagement.entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class LessonCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<Lesson> GetLessons()
        {
            var Lessons = new List<Lesson>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from Lesson", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new Lesson();
                    list.Id = Convert.ToInt32(reader["ID"].ToString());
                    list.Name = reader["NAME"].ToString();
                    list.Quota = Convert.ToInt32(reader["QUOTA"].ToString());

                    Lessons.Add(list);
                }
                con.Close();
            }
            return Lessons;
        }

        public Lesson GetLessonsById(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                Lesson response = new Lesson();

                string query = $"select * from Lesson where Id = {Id}";
                OracleCommand command = new OracleCommand(query, con);
                command.Connection = con;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Lesson read = new Lesson();
                    read.Id = Convert.ToInt32(reader["ID"].ToString());
                    read.Name = reader["NAME"].ToString();
                    read.Quota = Convert.ToInt32(reader["QUOTA"].ToString());

                    response = read;
                }
                con.Close();
                return response;
            }
        }

        public void CreateLessons(Lesson report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertLessons";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.Quota);

                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateLessons(Lesson report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "UpdateLesson";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", report.Id);
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.Quota);
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
                command.CommandText = "DeleteLesson";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", Id);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

    }

}
