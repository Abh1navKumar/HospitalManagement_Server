using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataServicesLib
{
    public class AdminDataService : IAdminDataService
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        private void CreateConnection()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString;

        }
        public DataSet GetAdminList()
        {
            SqlDataAdapter adapter;
            DataSet AdminTable = new DataSet();
            //SqlDataReader _reader = null;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_adminTable";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(AdminTable);

                    //_reader = _command.ExecuteReader();

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
            return AdminTable;
        }
        public DataSet GetAdminDisplayed()
        {
            SqlDataAdapter adapter;
            DataSet AdminTable = new DataSet();
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Admin";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(AdminTable);

                    //_reader = _command.ExecuteReader();

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
            return AdminTable;
        }
        public int Add_Admin(DataModelLib.Admin _adminUser)
        {
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_insert_Admin";
            _command.Connection = _connection;
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = _adminUser.Id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;


            SqlParameter roleParameter = new SqlParameter();
            roleParameter.ParameterName = "@role";
            roleParameter.Value = _adminUser.Role;
            roleParameter.SqlDbType = SqlDbType.VarChar;
            roleParameter.Size = 50;

            SqlParameter usernameParameter = new SqlParameter();
            usernameParameter.ParameterName = "@username";
            usernameParameter.Value = _adminUser.Name;
            usernameParameter.SqlDbType = SqlDbType.VarChar;
            usernameParameter.Size = 50;

            UserDataService u = new UserDataService();
            SqlParameter passwordParameter = new SqlParameter();
            passwordParameter.ParameterName = "@password";
            passwordParameter.Value =  u.MD5Hash(_adminUser.Password);
            passwordParameter.SqlDbType = SqlDbType.VarChar;
            passwordParameter.Size = 50;

            SqlParameter priviledgeParameter = new SqlParameter();
            priviledgeParameter.ParameterName = "@priviledge";
            priviledgeParameter.Value = Int32.Parse(_adminUser.Privileges.ToString());
            priviledgeParameter.SqlDbType = SqlDbType.Int;

            _command.Parameters.Add(roleParameter);
            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(usernameParameter);
            _command.Parameters.Add(passwordParameter);
            _command.Parameters.Add(priviledgeParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                     status = _command.ExecuteNonQuery();
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
            return status;
        }
        public int Update_Admin(DataModelLib.Admin _adminUser)
        {
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = _adminUser.Id;

            SqlParameter priviledgeParameter = new SqlParameter();
            priviledgeParameter.ParameterName = "@priviledge";
            priviledgeParameter.Value =_adminUser.Privileges;

            SqlParameter usernameParameter = new SqlParameter();
            usernameParameter.ParameterName = "@username";
            usernameParameter.Value =_adminUser.Name;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(usernameParameter);
            _command.Parameters.Add(priviledgeParameter);
            if (_adminUser.Password.Equals(""))
            {
                _command.CommandText = "proc_update_AdminWithoutPassword";

            }
            else
            {
                _command.CommandText = "proc_update_AdminWithPassword";
                UserDataService u = new UserDataService();
                SqlParameter passwordParameter = new SqlParameter();
                passwordParameter.ParameterName = "@password";
                passwordParameter.Value = u.MD5Hash(_adminUser.Password);
                _command.Parameters.Add(passwordParameter);

            }


           
            
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    status = _command.ExecuteNonQuery();
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
            return status;
        }
        public void Delete_Admin(string adminId)
        {
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "proc_delete_Admin";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.Value = adminId;
                    _command.Parameters.Add(idParameter);
                    _command.ExecuteNonQuery();
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
        }

        public int Get_Priviledge(string adminId)
        {
            int priviledge = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Priviledge";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@_id";
            idParameter.Value = adminId;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;


            SqlParameter p_Parameter = new SqlParameter();
            p_Parameter.ParameterName = "@priviledge";
            p_Parameter.SqlDbType = SqlDbType.Int;
            p_Parameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(p_Parameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.ExecuteNonQuery();
                    priviledge = Int32.Parse(p_Parameter.Value.ToString());

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
            return priviledge;

        }
        public string Get_Id(DataModelLib.Admin user)
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

            UserDataService u = new UserDataService();
            SqlParameter passParameter = new SqlParameter();
            passParameter.ParameterName = "@_password ";
            passParameter.Value = u.MD5Hash(user.Password);
            passParameter.SqlDbType = SqlDbType.VarChar;
            passParameter.Size = 80;

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
