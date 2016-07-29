using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DataServicesLib
{
    public class UserDataService : IUserDataService
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        private void CreateConnection()
        {
             _connection = new SqlConnection();
            _connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString;

        }
        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public string AuthenticateUser(DataModelLib.User user)
        {
            string role = "";
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_authenticate_user";
            _command.Connection = _connection;

            SqlParameter usernameParameter = new SqlParameter();
            usernameParameter.ParameterName = "@_username";
            usernameParameter.SqlDbType = SqlDbType.VarChar;
            usernameParameter.Size = 20;

            SqlParameter passwordParameter = new SqlParameter();
            passwordParameter.ParameterName = "@_password";
            passwordParameter.SqlDbType = SqlDbType.VarChar;
            passwordParameter.Size = 80;

            SqlParameter roleParameter = new SqlParameter();
            roleParameter.ParameterName = "@_role";
            roleParameter.SqlDbType = SqlDbType.VarChar;
            roleParameter.Size = 20;
            roleParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(usernameParameter);
            _command.Parameters.Add(passwordParameter);
            _command.Parameters.Add(roleParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.Parameters["@_username"].Value = user.Name;
                    _command.Parameters["@_password"].Value = MD5Hash(user.Password);

                    _command.ExecuteNonQuery();
                    role = roleParameter.Value.ToString();
                    
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return role;
        }
        public string AuthenticateUserById(DataModelLib.User user)
        {
            string roleById = "";
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_authenticate_userById";
            _command.Connection = _connection;

            SqlParameter usernameParameter = new SqlParameter();
            usernameParameter.ParameterName = "@_username";
            usernameParameter.SqlDbType = SqlDbType.VarChar;
            usernameParameter.Size = 20;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@_id";
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            SqlParameter roleParameter = new SqlParameter();
            roleParameter.ParameterName = "@_role";
            roleParameter.SqlDbType = SqlDbType.VarChar;
            roleParameter.Size = 20;
            roleParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(usernameParameter);
            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(roleParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.Parameters["@_username"].Value = user.Name;
                    _command.Parameters["@_id"].Value = user.Id;

                    _command.ExecuteNonQuery();
                    roleById = roleParameter.Value.ToString();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return roleById;
        }
        public int CheckId_replication(String Id, String _commandText)
        {
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = _commandText;
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    SqlDataReader r = _command.ExecuteReader();
                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            if ((r["Id"].ToString()).Equals(Id))
                            {
                                _connection.Close();
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return 1;
        }
        public string Get_Id(DataModelLib.User user)
        {
            string id = "";
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Id";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@_id";
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;
            idParameter.Direction = System.Data.ParameterDirection.Output;

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@_name";
            nameParameter.Value = user.Name;
            nameParameter.SqlDbType = SqlDbType.VarChar;
            nameParameter.Size = 20;

            SqlParameter passParameter = new SqlParameter();
            passParameter.ParameterName = "@_password ";
            passParameter.Value = MD5Hash(user.Password);
            passParameter.SqlDbType = SqlDbType.VarChar;
            passParameter.Size = 20;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(nameParameter);
            _command.Parameters.Add(passParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.ExecuteNonQuery();
                    id = idParameter.Value.ToString();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return id;

        }


    }
}
