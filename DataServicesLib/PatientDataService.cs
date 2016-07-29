using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataServicesLib
{
   public class PatientDataService : IPatientDataService
    {
        private SqlConnection _connection;
        private SqlCommand _command;
       
        private void CreateConnection()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString;
        }
       
        public DataSet GetPatientListByDocId(string DocId)
        {
            SqlDataAdapter adapter;
            DataSet patientTable = new DataSet();
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_PatientByDoctor";
            _command.Connection = _connection;

            SqlParameter docParameter = new SqlParameter();
            docParameter.ParameterName = "@_docId";
            docParameter.SqlDbType = SqlDbType.VarChar;
            docParameter.Size = 20; 
            docParameter.Value = DocId;

            _command.Parameters.Add(docParameter);
            
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(patientTable);    
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
            return patientTable;
        }
        public DataSet GetPatientList()
        {
            SqlDataAdapter adapter;
            DataSet patientTable = new DataSet();
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Patient";
            _command.Connection = _connection;

            
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(patientTable);
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
            return patientTable;
        }

        public int CheckPatientForDoctor(string DocId)
        {
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "proc_select_Patient";
            _command.Connection = _connection;
            
            try
            {
                _connection.Open();
                SqlDataReader r = _command.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        if (r["docId"].ToString().Equals(DocId))
                        {
                            _connection.Close();
                            return 1;
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
            return 0;
        }
        public int Add_Patient(DataModelLib.Patient _patient)
        {
            string folderName = "C:\\Users\\310246678\\Documents\\Visual Studio 2015\\Projects\\HospitalManagement_Server\\ReportImages\\" + _patient.Id;
            string xrayString = System.IO.Path.Combine(folderName, "Xrays");
            string ecgString = System.IO.Path.Combine(folderName, "Ecgs");
            string mriString = System.IO.Path.Combine(folderName, "Mris");
            System.IO.Directory.CreateDirectory(xrayString);
            System.IO.Directory.CreateDirectory(ecgString);
            System.IO.Directory.CreateDirectory(mriString);
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_insert_Patient";
            _command.Connection = _connection;
            SqlCommand _cmd = new SqlCommand();
            SqlConnection _connection1 = new SqlConnection();
            _connection1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString;

            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = "proc_insert_Reports";
            _cmd.Connection = _connection1;

            SqlParameter xrayLocParameter = new SqlParameter();
            xrayLocParameter.ParameterName = "@xrayLoc";
            xrayLocParameter.Value = xrayString;
            xrayLocParameter.SqlDbType = SqlDbType.VarChar;
            xrayLocParameter.Size = 200;

            SqlParameter ecgLocParameter = new SqlParameter();
            ecgLocParameter.ParameterName = "@ecgLoc";
            ecgLocParameter.Value = ecgString;
            ecgLocParameter.SqlDbType = SqlDbType.VarChar;
            ecgLocParameter.Size = 200;

            SqlParameter mriLocParameter = new SqlParameter();
            mriLocParameter.ParameterName = "@mriLoc";
            mriLocParameter.Value = mriString;
            mriLocParameter.SqlDbType = SqlDbType.VarChar;
            mriLocParameter.Size = 200;

            SqlParameter iddParameter = new SqlParameter();
            iddParameter.ParameterName = "@id";
            iddParameter.Value = _patient.Id;
            iddParameter.SqlDbType = SqlDbType.VarChar;
            iddParameter.Size = 20;


            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = _patient.Id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;


            SqlParameter roleParameter = new SqlParameter();
            roleParameter.ParameterName = "@docId";
            roleParameter.Value = _patient.DocId;
            roleParameter.SqlDbType = SqlDbType.VarChar;
            roleParameter.Size = 50;


            SqlParameter ageParameter = new SqlParameter();
            ageParameter.ParameterName = "@age";
            ageParameter.Value = _patient.Age;
            ageParameter.SqlDbType = SqlDbType.VarChar;
            ageParameter.Size = 50;

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@name";
            nameParameter.Value = _patient.Name;
            nameParameter.SqlDbType = SqlDbType.VarChar;
            nameParameter.Size = 50;
            
            _command.Parameters.Add(roleParameter);
            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(ageParameter);
            _command.Parameters.Add(nameParameter);

            _cmd.Parameters.Add(iddParameter);
            _cmd.Parameters.Add(xrayLocParameter);
            _cmd.Parameters.Add(ecgLocParameter);
            _cmd.Parameters.Add(mriLocParameter);
  
            try
            {
                _connection.Open();
                _connection1.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    status = _command.ExecuteNonQuery();
                    int status2 = _cmd.ExecuteNonQuery();
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
        public int Update_Patient(DataModelLib.Patient _patient)
        {
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_update_Patient";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = _patient.Id;

            SqlParameter docParameter = new SqlParameter();
            docParameter.ParameterName = "@doc";
            docParameter.Value = _patient.DocId;

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@name";
            nameParameter.Value = _patient.Name;


            SqlParameter ageParameter = new SqlParameter();
            ageParameter.ParameterName = "@age";
            ageParameter.Value = _patient.Age;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(docParameter);
            _command.Parameters.Add(nameParameter);
            _command.Parameters.Add(ageParameter);

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
        public int Delete_Patient(string patientId)
        {
            int status = 0;
           // string folderName = "C:\\Users\\310246678\\Documents\\Visual Studio 2015\\Projects\\HospitalManagement_Server\\ReportImages\\" + patientId;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "proc_delete_Patient";
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.Value = patientId;
                    _command.Parameters.Add(idParameter);
                    status = _command.ExecuteNonQuery();
                    //if (System.IO.Directory.Exists(folderName))
                    //{
                    //    System.IO.Directory.Delete(folderName,true);
                    //}
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
        public int Add_Report(DataModelLib.Report _report,string _commandText)
        {
            int status = 0;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = _commandText;
            _command.Connection = _connection;

            SqlParameter ReportIdParameter = new SqlParameter();
            ReportIdParameter.ParameterName = "@reportId";
            ReportIdParameter.Value = _report.ReportId;
            ReportIdParameter.SqlDbType = SqlDbType.VarChar;
            ReportIdParameter.Size = 20;


            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = _report.Id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 50;


            SqlParameter timeParameter = new SqlParameter();
            timeParameter.ParameterName = "@time";
            timeParameter.Value = _report.DateTime;
            timeParameter.SqlDbType = SqlDbType.VarChar;
            timeParameter.Size = 50;

            _command.Parameters.Add(ReportIdParameter);
            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(timeParameter);

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
        public DataSet GetXrayData(string patient_id)
        {
            SqlDataAdapter adapter;
            DataSet xRayTable = new DataSet();
            //SqlDataReader _reader = null;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_XrayTable";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = patient_id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            _command.Parameters.Add(idParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(xRayTable);

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
            return xRayTable;
        }

        public DataSet GetEcgData(string patient_id)
        {
            SqlDataAdapter adapter;
            DataSet EcgTable = new DataSet();
            //SqlDataReader _reader = null;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_EcgTable";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = patient_id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            _command.Parameters.Add(idParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(EcgTable);

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
            return EcgTable;
        }

        public DataSet GetMriData(string patient_id)
        {
            SqlDataAdapter adapter;
            DataSet MriTable = new DataSet();
            //SqlDataReader _reader = null;
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_MriTable";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = patient_id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            _command.Parameters.Add(idParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    adapter = new SqlDataAdapter(_command);
                    adapter.Fill(MriTable);

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
            return MriTable;
        }

        public string getXrayImgLocation(string patient_id)
        {
            string XrayImgLocation = "";
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_XrayImglocation";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = patient_id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            SqlParameter locationParameter = new SqlParameter();
            locationParameter.ParameterName = "@_location";
            locationParameter.SqlDbType = SqlDbType.VarChar;
            locationParameter.Size = 200;
            locationParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(locationParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.ExecuteNonQuery();
                    XrayImgLocation = locationParameter.Value.ToString();

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
            return XrayImgLocation;
        }

        public string getEcgImgLocation(string patient_id)
        {
            string EcgImgLocation = "";
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_EcgImglocation";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = patient_id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            SqlParameter locationParameter = new SqlParameter();
            locationParameter.ParameterName = "@_location";
            locationParameter.SqlDbType = SqlDbType.VarChar;
            locationParameter.Size = 200;
            locationParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(locationParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.ExecuteNonQuery();
                    EcgImgLocation = locationParameter.Value.ToString();

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
            return EcgImgLocation;
        }

        public string getMriImgLocation(string patient_id)
        {
            string MriImgLocation = "";
            CreateConnection();
            _command = new SqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "proc_select_MriImglocation";
            _command.Connection = _connection;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = patient_id;
            idParameter.SqlDbType = SqlDbType.VarChar;
            idParameter.Size = 20;

            SqlParameter locationParameter = new SqlParameter();
            locationParameter.ParameterName = "@_location";
            locationParameter.SqlDbType = SqlDbType.VarChar;
            locationParameter.Size = 200;
            locationParameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(idParameter);
            _command.Parameters.Add(locationParameter);
            try
            {
                _connection.Open();
                if (_connection.State == ConnectionState.Open)
                {
                    _command.ExecuteNonQuery();
                    MriImgLocation = locationParameter.Value.ToString();

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
            return MriImgLocation;
        }

    }
}
