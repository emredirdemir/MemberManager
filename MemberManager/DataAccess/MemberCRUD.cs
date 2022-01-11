using MemberManagement.entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MemberManager.DataAccess
{
    public class MemberCRUD
    {
        readonly string connectionString = "Data Source=localhost;User Id=SYSTEM;Password=1234;";

        public IEnumerable<Member> GetMembers()
        {
            var Members = new List<Member>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand("select * from Member", con);
                command.Connection = con;
                command.InitialLOBFetchSize = 1000;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var list = new Member();
                    list.Id = Convert.ToInt32(reader["ID"].ToString());
                    list.Name = reader["NAME"].ToString();
                    list.LastName = reader["LASTNAME"].ToString();
                    list.PhoneNumber = Convert.ToInt32(reader["PHONENUMBER"].ToString());
                    list.DOB = Convert.ToDateTime(reader["DOB"].ToString());
                    list.MailAddress = reader["MAILADDRESS"].ToString();

                    Members.Add(list);
                }
                con.Close();
            }
            return Members;
        }

        public Member GetMembersById(int Id)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                Member response = new Member();

                string query = $"select * from Member where Id = {Id}";
                OracleCommand command = new OracleCommand(query, con);
                command.Connection = con;
                command.CommandType = CommandType.Text;
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Member read = new Member();
                    read.Id = Convert.ToInt32(reader["ID"].ToString());
                    read.Name = reader["NAME"].ToString();
                    read.LastName = reader["LASTNAME"].ToString();
                    read.PhoneNumber = Convert.ToInt32(reader["PHONENUMBER"].ToString());
                    read.DOB = Convert.ToDateTime(reader["DOB"].ToString());
                    read.MailAddress = reader["MAILADDRESS"].ToString();
                    response = read;
                }
                con.Close();
                return response;
            }
        }

        public void CreateMembers(Member report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "InsertMember";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                command.Parameters.Add("p_phonenumber", report.PhoneNumber);
                command.Parameters.Add("p_dob", report.DOB);
                command.Parameters.Add("p_email", report.MailAddress);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateMembers(Member report)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = con;
                command.CommandText = "UpdateMember";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", report.Id);
                command.Parameters.Add("p_name", report.Name);
                command.Parameters.Add("p_lastname", report.LastName);
                command.Parameters.Add("p_phonenumber", report.PhoneNumber);
                command.Parameters.Add("p_dob", report.DOB);
                command.Parameters.Add("p_email", report.MailAddress);
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
                command.CommandText = "DeleteMember";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_id", Id);
                con.Open();
                command.ExecuteNonQuery();

                con.Close();
            }
        }

    }

}
