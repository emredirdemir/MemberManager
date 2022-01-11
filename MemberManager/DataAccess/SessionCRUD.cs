using MemberManager.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class SessionCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<Session> GetSessions()
        {
            var Sessions = new List<Session>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from Sessions", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new Session();
                    list.Id = Convert.ToInt32(reader["ID"].ToString());
                    list.LessonName = reader["LESSONNAME"].ToString();
                    list.Instructor = reader["INSTRUCTOR"].ToString();
                    list.Quota = Convert.ToInt32(reader["QUOTA"].ToString());

                    Sessions.Add(list);
                }
                con.Close();
            }
            return Sessions;
        }

        public Session GetSessionsById(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                Session response = new Session();

                string query = $"select * from Sessions where Id = {Id}";
                OracleCommand command = new OracleCommand(query, con);
                command.Connection = con;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Session read = new Session();
                    read.Id = Convert.ToInt32(reader["ID"].ToString());
                    read.LessonName = reader["LESSONNAME"].ToString();
                    read.Instructor = reader["INSTRUCTOR"].ToString();
                    read.Quota = Convert.ToInt32(reader["QUOTA"].ToString());

                    response = read;
                }
                con.Close();
                return response;
            }
        }

        public void CreateSessions(Session report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertSessions";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_lessonname", report.LessonName);
                command.Parameters.Add("p_instructor", report.Instructor);
                command.Parameters.Add("p_quota", report.Quota);


                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateSessions(Session report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "UpdateSessions";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", report.Id);
                command.Parameters.Add("p_lessonname", report.LessonName);
                command.Parameters.Add("p_instructor", report.Instructor);
                command.Parameters.Add("p_quota", report.Quota);
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
                command.CommandText = "DeleteSessions";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", Id);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

    }

}