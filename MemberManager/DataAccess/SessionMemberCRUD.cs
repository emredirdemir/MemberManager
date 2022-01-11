using MemberManager.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class SessionMemberCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<SessionMember> GetSessionMembers()
        {
            var SessionMembers = new List<SessionMember>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from SessionMembers", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new SessionMember();
                    list.SessionId = Convert.ToInt32(reader["SessionMemberID"].ToString());
                    list.MemberId = Convert.ToInt32(reader["MEMBERID"].ToString());
                    list.Name = reader["NAME"].ToString();
                    list.LastName = reader["LASTNAME"].ToString();

                    SessionMembers.Add(list);
                }
                con.Close();
            }
            return SessionMembers;
        }

        public SessionMember GetSessionMembersById(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                SessionMember response = new SessionMember();

                string query = $"select * from SessionMembers where Id = {Id}";
                OracleCommand command = new OracleCommand(query, con);
                command.Connection = con;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SessionMember read = new SessionMember();
                    read.SessionId = Convert.ToInt32(reader["SessionMemberID"].ToString());
                    read.MemberId = Convert.ToInt32(reader["MEMBERID"].ToString());
                    read.Name = reader["NAME"].ToString();
                    read.LastName = reader["LASTNAME"].ToString();

                    response = read;
                }
                con.Close();
                return response;
            }
        }

        public void CreateSessionMembers(SessionMember report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertSessionMembers";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_memberid", report.MemberId);
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateSessionMembers(SessionMember report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "UpdateSessionMembers";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", report.SessionId);
                command.Parameters.Add("p_memberid", report.MemberId);
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
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
                command.CommandText = "DeleteSessionMembers";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", Id);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}