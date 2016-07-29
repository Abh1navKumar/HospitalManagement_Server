using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DataServicesLib
{
    public class DoctorDataService : IDoctorDataService
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        string _role = "";
        string _id = "";

        private void CreateConnection()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString;

        }

        public DataSet GetDoctorList()
        {
            SqlDataAdapter adapter;
            DataSet doctorTable = new DataSet();
            //SqlDataReader _reader = null;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Doctor";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(doctorTable);

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
            return doctorTable;
        }
        public void Delete_Doctor(string docId)
        {
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "proc_delete_Doctor";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.Value = docId;
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
        public int Add_Doctor(DataModelLib.Doctor _docUser)
        {
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_insert_Doctor";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = _docUser.Id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;


            SqlParameter roleParameter = new SqlParameter();
            roleParameter.ParameterName = "@role";
            roleParameter.Value = _docUser.Role;
            roleParameter.SqlDbType = SqlDbType.VarChar;
            roleParameter.Size = 50;


            SqlParameter regionParameter = new SqlParameter();
            regionParameter.ParameterName = "@region";
            regionParameter.Value = _docUser.Region;
            regionParameter.SqlDbType = SqlDbType.VarChar;
            regionParameter.Size = 50;


            SqlParameter usernameParameter = new SqlParameter();
            usernameParameter.ParameterName = "@username";
            usernameParameter.Value = _docUser.Name;
            usernameParameter.SqlDbType = SqlDbType.VarChar;
            usernameParameter.Size = 50;

            UserDataService u = new UserDataService();
            SqlParameter passwordParameter = new SqlParameter();
            passwordParameter.ParameterName = "@password";
            passwordParameter.Value = u.MD5Hash(_docUser.Password);
            passwordParameter.SqlDbType = SqlDbType.VarChar;
            passwordParameter.Size = 50;

            _command.Parameters.Add(roleParameter);
            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(regionParameter);
            _command.Parameters.Add(usernameParameter);
            _command.Parameters.Add(passwordParameter);

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
        public int Update_Doctor(DataModelLib.Doctor _docUser)
        {
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;
            idParameter.Value = _docUser.Id;

            SqlParameter regionParameter = new SqlParameter();
            regionParameter.ParameterName = "@region";
            regionParameter.SqlDbType = SqlDbType.VarChar;
            regionParameter.Size = 20;
            regionParameter.Value = _docUser.Region;

            SqlParameter usernameParameter = new SqlParameter();
            usernameParameter.ParameterName = "@username";
            usernameParameter.SqlDbType = SqlDbType.VarChar;
            usernameParameter.Size = 20;
            usernameParameter.Value = _docUser.Name;

            

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(regionParameter);
            _command.Parameters.Add(usernameParameter);
            if (_docUser.Password.Equals(""))
            {
                _command.CommandText = "proc_Update_DoctorWithoutPassword";

            }
            else
            {
                _command.CommandText = "proc_update_DoctorWithPassword";
                UserDataService u = new UserDataService();
                SqlParameter passwordParameter = new SqlParameter();
                passwordParameter.ParameterName = "@password";
                passwordParameter.Value = u.MD5Hash(_docUser.Password);
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
        public List<String> GetRegions()
        {
            List<string> regions = new List<string>();
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_regions";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    SqlDataReader _reader = _command.ExecuteReader();
                    while (_reader.Read())
                    {
                        regions.Add(_reader["region"].ToString());
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

            return regions;
        }
        public string AuthenticateDoctorByRegion(DataModelLib.Doctor _docUser)
        {
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_authenticate_DoctorByRegion";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@_id";
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@_name";
            nameParameter.SqlDbType = SqlDbType.VarChar;
            nameParameter.Size = 20;

            SqlParameter roleParameter = new SqlParameter();
            roleParameter.ParameterName = "@_role";
            roleParameter.SqlDbType = SqlDbType.VarChar;
            roleParameter.Size = 20;
            roleParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(nameParameter);
            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(roleParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.Parameters["@_id"].Value = _docUser.Id;
                    _command.Parameters["@_name"].Value = _docUser.Name;
                     _command.ExecuteNonQuery();
                    _role = roleParameter.Value.ToString();
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
            return _role;
        }
        public string GetDoctorIdByRegion(string data)
        {
            string[] temp = data.Split('%');
             string name = temp[0];
            string region = temp[1];
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_get_DoctorIdByRegion";
            _command.Connection = _connection;

            SqlParameter regionParameter = new SqlParameter();
            regionParameter.ParameterName = "@_region";
            regionParameter.SqlDbType = SqlDbType.VarChar;
            regionParameter.Size = 20;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@_id";
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;
            idParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(regionParameter);
            _command.Parameters.Add(idParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.Parameters["@_region"].Value = region;
                    _command.ExecuteNonQuery();
                    _id = idParameter.Value.ToString();
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
            return _id;
        }
        public int VerifyDoctor(string docId)
        {
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Doctor";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                SqlDataReader rd = _command.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if ((rd["Id"].ToString()).Equals(docId))
                        {
                            _connection.Close();
                            return 1;
                        }
                    }
                }
                _connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }

    }
}
