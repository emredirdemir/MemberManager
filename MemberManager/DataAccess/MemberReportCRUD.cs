using MemberManagement.entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class MemberReportCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<MemberReport> GetMemberReports()
        {
            var memberReports = new List<MemberReport>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from memberreport", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new MemberReport();
                    list.Id = Convert.ToInt32(reader["ID"].ToString());
                    list.StudentNumber = Convert.ToInt32(reader["STUDENTID"].ToString());
                    list.Name = reader["STUDENTNAME"].ToString();
                    list.LastName = reader["LASTNAME"].ToString();
                    list.LessonName = reader["LESSONNAME"].ToString();
                    list.Score = Convert.ToInt32(reader["SCORE"].ToString());
                    list.Absenteeism = Convert.ToInt32(reader["ABSENTEEISM"].ToString());
                    list.State = reader["STATE"].ToString();

                    memberReports.Add(list);
                }
                con.Close();
            }
            return memberReports;
        }

        public MemberReport GetMemberReportsById(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                MemberReport response = new MemberReport();

                string query = $"select * from memberreport where Id = {Id}";
                OracleCommand command = new OracleCommand(query, con);
                command.Connection = con;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MemberReport read = new MemberReport();
                    read.Id = Convert.ToInt32(reader["ID"].ToString());
                    read.StudentNumber = Convert.ToInt32(reader["STUDENTID"].ToString());
                    read.Name = reader["STUDENTNAME"].ToString();
                    read.LastName = reader["LASTNAME"].ToString();
                    read.LessonName = reader["LESSONNAME"].ToString();
                    read.Score = Convert.ToInt32(reader["SCORE"].ToString());
                    read.Absenteeism = Convert.ToInt32(reader["ABSENTEEISM"].ToString());
                    read.State = reader["STATE"].ToString();
                    response = read;
                }
                con.Close();
                return response;
            }
        }

        public void CreateMemberReports(MemberReport report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertMreport";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_studentid", report.StudentNumber);
                command.Parameters.Add("p_studentname", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                command.Parameters.Add("p_lessonname", report.LessonName);
                command.Parameters.Add("p_score", report.Score);
                command.Parameters.Add("p_absenteeism", report.Absenteeism);
                command.Parameters.Add("p_state", report.State);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }
        
        public void UpdateMemberReports(MemberReport report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "UpdateMreport";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", report.Id);
                command.Parameters.Add("p_studentid", report.StudentNumber);
                command.Parameters.Add("p_studentname", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                command.Parameters.Add("p_lessonname", report.LessonName);
                command.Parameters.Add("p_score", report.Score);
                command.Parameters.Add("p_absenteeism", report.Absenteeism);
                command.Parameters.Add("p_state", report.State);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void delete(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "DeleteMReport";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", Id);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

    }
}
